using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Panda;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent (typeof(ThirdPersonNPCCharacter))]
public class BasicAINPC : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent { get; private set; }
	public ThirdPersonNPCCharacter character { get; private set; }
	public float approachSpeed = 1.0f;
	public float strollSpeed = 0.5f;
	public float reachedMinDistance = 2.0f;
	public string wayPointString;


	protected int wayPointIndex;
	protected GameObject[] waypoints;

	//FSM-Variables
	[Task]
	public bool IsStroll = false;



	// Use this for initialization
	public virtual void Start () {
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent> ();
		character = GetComponent<ThirdPersonNPCCharacter> ();
		waypoints = GameObject.FindGameObjectsWithTag(wayPointString);
		RandomizeWayPointIndex ();

		AgentInitialization ();
		FSMIntitialization ();
	}

	#region startup Methods
	/// <summary>
	/// FSM: Set Start state und prepare FSM to kick off
	/// </summary>
	private void FSMIntitialization ()
	{
		IsStroll = true;
	}

	/// <summary>
	/// Agent needs no rotation update. Is done in our script
	/// </summary>
	private void AgentInitialization ()
	{
		agent.updateRotation = false;
		agent.updatePosition = true;
	}

	/// <summary>
	/// Randomizes the index of the way point. Change when reached
	/// </summary>
	protected void RandomizeWayPointIndex(){
		wayPointIndex = Random.Range (0, waypoints.Length);
	}
	#endregion


	/// <summary>
	/// Checks whethe the destination of character is reached
	/// </summary>
	/// <returns><c>true</c>, if destination reached was ised, <c>false</c> otherwise.</returns>
	protected bool isDestinationReached(){
		bool isReached = true;
		if (Vector3.Distance (transform.position, waypoints [wayPointIndex].transform.position) >= reachedMinDistance) {
			isReached = false;
		}
		return isReached;
	}

	/// <summary>
	/// Moves the Character to the desired destination
	/// </summary>
	/// <param name="position">Position.</param>
	protected void MoveToDestination (Vector3 position)
	{
		agent.SetDestination (position);
		character.Move (agent.desiredVelocity);
	}

	/// <summary>
	/// Patrol the waypoints-area according to speed
	/// </summary>
	[Task]
	public void Stroll ()
	{
		agent.speed = strollSpeed;
		if (!isDestinationReached()) {
			MoveToDestination (waypoints [wayPointIndex].transform.position);
		} else if (isDestinationReached()) {
			RandomizeWayPointIndex ();
		} else {
			character.Move (Vector3.zero);
		}
	}

	/// <summary>
	/// Stands moving the character.
	/// </summary>
	[Task]
	public void StandStill()
	{
		character.Move (Vector3.zero);
	}
		
}
