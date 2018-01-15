using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Panda;

public class AICitizen : AINPC {

	public string wayPointStringHome;
	public string wayPointStringBreakSit;
	public string wayPointStringBreakStand;
	public string wayPointSingleTarget;

	protected AIVisionNpc vision;
	protected GameObject talkPartner;

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
	/// Ises the talk partner reached.
	/// </summary>
	/// <returns><c>true</c>, if talk partner reached was ised, <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool IsTalkPartnerReached(){
		bool isReached = true;
		if (talkPartner != null) {
			Vector3 talkPartnerPosition = talkPartner.transform.position;
			if (Vector3.Distance (transform.position, talkPartnerPosition) > reachedMinDistance) {
				isReached = false;
			}
		}

		return isReached;
	}

	/// <summary>
	/// Approachs the talk partner.
	/// </summary>
	/// <returns><c>true</c>, if talk partner was approached, <c>false</c> otherwise.</returns>
	[Task]
	public bool ApproachTalkPartner(){
		bool retVal = false;
		if (talkPartner != null) {
			this.MoveToDestination (talkPartner.transform.position);
			retVal = true;
		}
		return retVal;
	}

	/// <summary>
	/// Stops the talk partner move.
	/// </summary>
	[Task]
	public bool StopTalkPartnerMove(){
		if (talkPartner != null) {
			AICitizen talkPartnerAI = talkPartner.GetComponent<AICitizen> ();
			talkPartnerAI.StandStill ();
		}
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

	/// <summary>
	/// Stops the myself move.
	/// </summary>
	[Task]
	public bool StopMove(){
		this.StandStill ();
		return true;
	}


	/// <summary>
	/// Rotates the talk partner.
	/// </summary>
	[Task]
	public void RotateTalkPartner(){
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
