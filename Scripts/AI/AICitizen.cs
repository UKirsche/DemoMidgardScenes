using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Panda;

public class AICitizen : AINPC {

	public string wayPointStringHome;
	public string wayPointStringBreakSit;

	protected AIVisionNpc vision;
	protected GameObject talkPartner;
	protected GameObject sitOpportunity;


	#region FSM-Variablen
	[Task]
	public bool IsBreak=false;
	[Task]
	public bool IsSitting=false;
	[Task]
	public bool IsDialog = false;
	#endregion

	// Use Initialization from BaseClass
	public override void Start () {
		base.Start ();

		vision = GetComponentInChildren<AIVisionNpc> ();
		IsBreak = true;
		IsStroll = false;
		talkPartner = null;
	}



	#region Sit/Break Tasks

	/// <summary>
	/// Determines whether this instance sees a possibility to make a little break
	/// </summary>
	/// <returns><c>true</c> if this instance is break visible; otherwise, <c>false</c>.</returns>
	[Task]
	public bool IsSitVisible(){
		bool retVal = false;

		if (vision != null) {
			GameObject closestBreakGO = vision.GetClosestVisibleGO ();
			if (closestBreakGO != null) {
				if (closestBreakGO.tag.Equals(wayPointStringBreakSit)) {
					sitOpportunity = closestBreakGO;
					retVal= true;
				} 
			}
		}

		return retVal;
	}

	/// Approachs the talk partner.
	/// </summary>
	/// <returns><c>true</c>, if talk partner was approached, <c>false</c> otherwise.</returns>
	[Task]
	public bool ApproachOpportunity(){
		bool retVal = false;
		if (sitOpportunity != null) {
			ApproachDestination (sitOpportunity);
			retVal = true;
		}
		return retVal;
	}

	/// <summary>
	/// Ises the talk partner reached.
	/// </summary>
	/// <returns><c>true</c>, if talk partner reached was ised, <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool IsSitReached(){
		bool isReached = true;
		if (sitOpportunity != null) {
			isReached = IsDestinationReached (sitOpportunity);
		}

		return isReached;
	}

	/// <summary>
	/// Rotates the NPC so in sit direction
	/// </summary>
	/// <returns><c>true</c>, if sit was rotated, <c>false</c> otherwise.</returns>
	[Task]
	public bool RotateSit(){
		bool retVal = false;
		if (sitOpportunity != null) {
			var sitDirection = Quaternion.LookRotation(sitOpportunity.transform.right);
			this.transform.rotation = sitDirection;
			retVal = true;
		}
		return retVal;
	}


	[Task]
	public bool Sit(){
		ThirdPersonNPCNormal meThird = GetComponent<ThirdPersonNPCNormal> ();
		meThird.Sit ();
		return true;
	}


	[Task]
	public bool StopSit(){
		ThirdPersonNPCNormal meThird = GetComponent<ThirdPersonNPCNormal> ();
		meThird.StopSit ();
		return true;
	}

	[Task]
	public bool StepBack(){
		ThirdPersonNPCNormal meThird = GetComponent<ThirdPersonNPCNormal> ();
		this.transform.position = sitOpportunity.transform.position;
		return true;
	}
	#endregion


	#region Talk Tasks



	/// <summary>
	/// Determines whether this instance is talk partner visible.
	/// </summary>
	/// <returns><c>true</c> if this instance is talk partner visible; otherwise, <c>false</c>.</returns>
	[Task]
	public bool IsTalkPartnerVisible(){
		bool retVal = false;

		if (vision != null) {
			GameObject closestCitizen = vision.GetClosestVisibleGO ();
			if (closestCitizen != null) {
				AICitizen closestCitAI = closestCitizen.GetComponent<AICitizen> ();
				ThirdPersonNPCNormal closestCitThird = closestCitizen.GetComponent<ThirdPersonNPCNormal> ();
				if (closestCitAI != null && closestCitThird!=null) {
					talkPartner = closestCitizen;
					retVal= true;
				} 
			}
		}

		return retVal;
	}


	/// <summary>
	/// Approachs the talk partner.
	/// </summary>
	/// <returns><c>true</c>, if talk partner was approached, <c>false</c> otherwise.</returns>
	[Task]
	public bool ApproachTalkPartner(){
		bool retVal = false;
		if (talkPartner != null) {
			ApproachDestination (talkPartner);
			retVal = true;
		}
		return retVal;
	}

	/// <summary>
	/// Ises the talk partner reached.
	/// </summary>
	/// <returns><c>true</c>, if talk partner reached was ised, <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool IsTalkPartnerReached(){
		bool isReached = true;
		if (talkPartner != null) {
			isReached = IsDestinationReached (talkPartner);
		}
		return isReached;
	}


	/// <summary>
	/// Citizen approaches visible target; if it is also a Citizen NPC and starts talk animation
	/// </summary>
	[Task]
	public bool Talk(){
		ThirdPersonNPCNormal meThird = GetComponent<ThirdPersonNPCNormal> ();
		meThird.Talk ();
		return true;
	}

	/// <summary>
	/// Citizen approaches visible target; if it is also a Citizen NPC and starts talk animation
	/// </summary>
	[Task]
	public bool StopTalk(){
		ThirdPersonNPCNormal meThird = GetComponent<ThirdPersonNPCNormal> ();
		meThird.StopTalk ();
		return true;
	}


	/// <summary>
	/// Stops the talk partner move.
	/// </summary>
	[Task]
	public bool RotatePartner(){
		bool retVal = false;
		if (talkPartner != null) {
			talkPartner.transform.LookAt(this.transform.position);
			retVal = true;
		}
		Debug.DrawRay(transform.position, talkPartner.transform.position, Color.red);
		return retVal;
	} 

	/// <summary>
	/// Stops the talk partner move.
	/// </summary>
	[Task]
	public bool RemovePartnerFromSight(){
		bool retVal = false;
		if (talkPartner != null) {
			vision.RemoveNPCFromColliders (talkPartner);
			retVal = true;
		}
		return retVal;
	} 
	#endregion

	#region Helpers
	/// <summary>
	/// Approachs the destination of gameobject
	/// </summary>
	/// <param name="go">Go.</param>
	private void ApproachDestination(GameObject go){
		this.MoveToDestination (go.transform.position);
	}

	/// <summary>
	/// Determines whether this instance is destination reached the specified go.
	/// </summary>
	/// <returns><c>true</c> if this instance is destination reached the specified go; otherwise, <c>false</c>.</returns>
	/// <param name="go">Go.</param>
	private bool IsDestinationReached (GameObject go)
	{
		bool isReached=true;
		Vector3 goPosition = go.transform.position;
		if (Vector3.Distance (transform.position, goPosition) > reachedMinDistance) {
			isReached = false;
		}
		return isReached;
	}
	#endregion


	/// <summary>
	/// Stops the myself move.
	/// </summary>
	[Task]
	public bool StopMove(){
		this.StandStill ();
		return true;
	}




	/// <summary>
	/// Citizens walks global points in scene, usually city or village
	/// </summary>
	[Task]
	public void GlobalStroll ()
	{
		waypoints = GameObject.FindGameObjectsWithTag(wayPointString);
		base.Stroll ();
	}

	/// <summary>
	/// Citizen walks around his homebase.
	/// </summary>
	[Task]
	public void HomeStroll()
	{
		waypoints = GameObject.FindGameObjectsWithTag(wayPointStringHome);
		base.Stroll ();
	}

	/// <summary>
	/// Moves the citizen to the break-point
	/// </summary>
	[Task]
	public void BreakSit()
	{
		agent.speed = strollSpeed;
		waypoints = GameObject.FindGameObjectsWithTag(wayPointStringBreakSit);
		RandomizeWayPointIndex ();
		GameObject singleBreakPoint = waypoints [wayPointIndex];
		if (!isWayPointReached ()) {
			base.MoveToDestination (singleBreakPoint.transform.position);
		} else if(isWayPointReached()){
			agent.speed = 0;
			character.gameObject.transform.Rotate(0,-90,0);
			character.Move (Vector3.zero);
			character.updateAnimatorStop ();

			IsBreak = false;
			Debug.Log ("Forward: " + character.m_ForwardAmount);
		}
	}

}
