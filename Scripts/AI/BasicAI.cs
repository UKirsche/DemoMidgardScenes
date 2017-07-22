using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent (typeof(ThirdPersonNPCCharacter))]
public class BasicAI : MonoBehaviour
{
	public UnityEngine.AI.NavMeshAgent agent { get; private set; }

	public ThirdPersonNPCCharacter character { get; private set; }

	public enum State
	{
		PATROL,CHASE
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


	void Start ()
	{
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent> ();
		character = GetComponent<ThirdPersonNPCCharacter> ();
		waypoints = GameObject.FindGameObjectsWithTag("waypoint");
		RandomizeWayPointIndex ();

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
	/// Ons the trigger enter: Change state to Chase!!!
	/// </summary>
	/// <param name="otherCollider">Other collider.</param>
	void OnTriggerEnter (Collider otherCollider)
	{
		if (otherCollider.tag == "Player") {
			state = State.CHASE;
			target = otherCollider.gameObject;
		}
	}
}
