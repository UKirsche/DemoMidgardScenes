using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent (typeof(ThirdPersonNPCCharacter))]
public class EnemySightAI : MonoBehaviour
{
	public UnityEngine.AI.NavMeshAgent agent { get; private set; }
	public ThirdPersonNPCCharacter character { get; private set; }

	public enum State
	{
		PATROL,CHASE, INVESTIGATE
	}

	private GameObject target;
	private int wayPointIndex;
	bool coRoutineStarted = false;

	//FSM-Variables
	public State state;
	private bool alive;

	//Variables for Patrolling
	private GameObject[] waypoints;
	public float patrolSpeed = 0.5f;

	//Variables for Chasing
	public float chaseSpeed = 1.0f;


	//Variables for Investigating
	private Vector3 investigateSpot;
	private float timer=0.0f;
	public float investigateWait=10.0f;

	//Variables for Sight
	public float heigtMultiplier;
	public float sightDist = 20.0f;


	void Start ()
	{
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		character = GetComponent<ThirdPersonNPCCharacter> ();
		waypoints = GameObject.FindGameObjectsWithTag("waypoint");
		RandomizeWayPointIndex ();

		heigtMultiplier = 1.36f;

		AgentInitialization ();
		FSMIntitialization ();
	}

	/// <summary>
	/// FSM: Set Start state und prepare FSM to kick off
	/// </summary>
	private void FSMIntitialization ()
	{
		state = State.PATROL;
		alive = true;
	}

	/// <summary>
	/// Agent needs no rotation update. Is done in our script
	/// </summary>
	private void AgentInitialization ()
	{
		agent.updateRotation = false;
		agent.updatePosition = true;
	}


	void Update ()
	{
		if (!coRoutineStarted) {
			coRoutineStarted = true;
			StartCoroutine ("FSM");
		}
	}

	/// <summary>
	/// Finite State Machine
	/// </summary>
	IEnumerator FSM ()
	{
		while (alive) {
			switch (state) {
			case State.PATROL:
				Patrol ();
				break;
			case State.CHASE:
				Chase ();
				break;
			case State.INVESTIGATE:
				Investigate ();
				break;
			default:
				break;
			}	
			yield return null;
		}
	}


	/// <summary>
	/// Sets the waypoint index to randomized value
	/// </summary>
	private void RandomizeWayPointIndex(){
		wayPointIndex = Random.Range (0, waypoints.Length);
	}

	/// <summary>
	/// Patrol the waypoints-area
	/// </summary>
	void Patrol ()
	{
		agent.speed = patrolSpeed;
		if (Vector3.Distance (transform.position, waypoints [wayPointIndex].transform.position) >= 2.0f) {
			agent.SetDestination (waypoints [wayPointIndex].transform.position);
			character.Move (agent.desiredVelocity);
		} else if (Vector3.Distance (transform.position, waypoints [wayPointIndex].transform.position) < 2.0f) {
			RandomizeWayPointIndex ();
		} else {
			character.Move (Vector3.zero);
		}
	}

	/// <summary>
	/// Chase this instance.
	/// </summary>
	void Chase ()
	{
		agent.speed = chaseSpeed;
		agent.SetDestination (target.transform.position);
		character.Move (agent.desiredVelocity);
	}

	/// <summary>
	/// Investigate this instance.
	/// </summary>
	void Investigate(){

		//StopMOving
		agent.SetDestination(this.transform.position);
		character.Move (Vector3.zero);
		transform.LookAt (investigateSpot);
		if (timer >= investigateWait) {
			state = State.PATROL;
			timer = 0;
		}
	}

	private bool CheckLOSHit (Ray LOS, out RaycastHit rcHit)
	{
		bool isSeen = false;
		if (Physics.Raycast (LOS, out rcHit, sightDist)) {
			if (rcHit.collider.gameObject.CompareTag ("Player")) {
				isSeen = true;
			}
		}
		return isSeen;
	}

	/// <summary>
	/// Ons the trigger enter: Change state to Chase!!!
	/// </summary>
	/// <param name="otherCollider">Other collider.</param>
	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.tag == "Player") {
			state = State.INVESTIGATE;
			investigateSpot = otherCollider.transform.position;
		}
	}

	void FixedUpdate(){
		RaycastHit hit = new RaycastHit ();;
		timer += Time.deltaTime;
		Ray LOSCenter 	= new Ray (transform.position + Vector3.up * heigtMultiplier, transform.forward * sightDist);
		Ray LOSLeft		= new Ray (transform.position + Vector3.up * heigtMultiplier, (transform.forward - transform.right).normalized * sightDist);
		Ray LOSRight 	= new Ray (transform.position + Vector3.up * heigtMultiplier, (transform.forward + transform.right).normalized * sightDist);
		Ray LOSUp		= new Ray (transform.position + Vector3.up * heigtMultiplier, (transform.forward + transform.up).normalized * sightDist);
		Ray LOSDown 	= new Ray (transform.position + Vector3.up * heigtMultiplier, (transform.forward - transform.up).normalized * sightDist);

		List<Ray> losList = new List<Ray> ();
		losList.Add (LOSCenter);
		losList.Add (LOSLeft);
		losList.Add (LOSRight);
		losList.Add (LOSUp);
		losList.Add (LOSDown);

		bool isSeen = false;
		for (int i = 0; i < losList.Count; i++) {
			Ray ray = losList[i];
			Debug.DrawRay (ray.origin, ray.direction*10, Color.green);
			if (!isSeen) {
				isSeen= CheckLOSHit (ray, out hit);
			}
		}

		if (isSeen) {
			state = State.CHASE;
			target = hit.collider.gameObject;
		}

	}

}
