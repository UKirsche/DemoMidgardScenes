using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Panda;

[RequireComponent (typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent (typeof(ThirdPersonNPCNormal))]
public class AINPC : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent { get; private set; }
	public ThirdPersonNPCNormal character { get; private set; }
	public float approachSpeed = 1.0f;
	public float strollSpeed = 0.5f;
	public float reachedMinDistance = 2.0f;
	public string wayPointString;
	protected AIVisionNpc vision;
	protected List<GameObject> pcTalkPartners;
	protected GameObject pcTalkChosen;

	protected int wayPointIndex;
	protected GameObject[] waypoints;

	//FSM-Variables
	[Task]
	public bool IsStroll = false;



	// Use this for initialization
	public virtual void Start () {
		// get the components on the object we need ( should not be null due to require component so no need to check )
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		character = GetComponent<ThirdPersonNPCNormal> ();

		waypoints = GameObject.FindGameObjectsWithTag(wayPointString);
		RandomizeWayPointIndex ();
		pcTalkPartners = new List<GameObject> ();

		vision = GetComponentInChildren<AIVisionNpc> ();

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


	#region waypoints
	/// <summary>
	/// Checks whethe the destination of character is reached
	/// </summary>
	/// <returns><c>true</c>, if destination reached was ised, <c>false</c> otherwise.</returns>
	protected bool isWayPointReached(){
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
	#endregion


	#region movement
	/// <summary>
	/// Patrol the waypoints-area according to speed
	/// </summary>
	[Task]
	public bool Stroll ()
	{
		agent.speed = strollSpeed;
		if (!isWayPointReached()) {
			MoveToDestination (waypoints [wayPointIndex].transform.position);
		} else if (isWayPointReached()) {
			RandomizeWayPointIndex ();
		} else {
			character.Move (Vector3.zero);
		}

		return true;
	}

	/// <summary>
	/// Stands moving the character.
	/// </summary>
	[Task]
	public bool StandStill()
	{
		agent.speed = 0.0f;
		character.Move (Vector3.zero);
		return true;

	}
	#endregion

	#region PC Talk

	/// <summary>
	/// Determines whether this instance is PC in colliders.
	/// </summary>
	/// <returns><c>true</c> if this instance is PC in colliders; otherwise, <c>false</c>.</returns>
	[Task]
	public bool IsPCInColliders()
	{
		bool retVal = false;
		pcTalkPartners.Clear ();

		foreach (var collider in vision.colliders) {
			var attachedGameObject = collider.attachedRigidbody != null ? collider.attachedRigidbody.gameObject: null;
			if (attachedGameObject != null && attachedGameObject.tag.Equals(DemoRPGMovement.PLAYER_NAME)) {
				pcTalkPartners.Add (attachedGameObject);
				retVal = true;
				
			}
		}

		return retVal;
	}

	/// <summary>
	/// ISPCs the in talk dist.
	/// </summary>
	/// <returns><c>true</c>, if in talk dist was ISPCed, <c>false</c> otherwise.</returns>
	[Task]
	public bool ISPCInTalkDist()
	{
		bool retVal = false;
		float nearestTalkDistance = reachedMinDistance;

		if (pcTalkPartners.Count > 0) {
			foreach (var pc in pcTalkPartners) {
				float distToPC = Vector3.Distance (transform.position, pc.transform.position);
				if (distToPC <= nearestTalkDistance) {
					nearestTalkDistance = distToPC;
					pcTalkChosen = pc;
					retVal = true;
				} 
			}
		}

		return retVal;
	}


	[Task]
	public bool RotateToPC()
	{
		transform.LookAt(pcTalkChosen.transform.position);
		return true;
	}

	/// <summary>
	/// Sets the dialog partner for the hit PC (Player) as this NPC!
	/// </summary>
	/// <returns><c>true</c>, if dialog partner P was set, <c>false</c> otherwise.</returns>
	[Task]
	public bool SetDialogPartnerPC()
	{
		var dialogManager = pcTalkChosen.GetComponent<PlayerDialogManager> ();
		if (dialogManager != null) {
			dialogManager.SetDialogPartner (gameObject);
		} 
		return true;
	}

	/// <summary>
	/// Remoes this NPC as TalkPartner for PC
	/// </summary>
	/// <returns><c>true</c>, if dialog partner P was set, <c>false</c> otherwise.</returns>
	[Task]
	public bool UnsetDialogPartnerPC()
	{
		if (pcTalkChosen != null) {
			var dialogManager = pcTalkChosen.GetComponent<PlayerDialogManager> ();
			if (dialogManager != null) {
				dialogManager.SetDialogPartner (null);
			} 
		}

		return true;
	}

	#endregion
		
}
