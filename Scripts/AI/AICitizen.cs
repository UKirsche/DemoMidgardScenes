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
	}


	private bool isTalkPartnerReached(Vector3 talkPartnerPosition){
		bool isReached = true;
		if (Vector3.Distance (transform.position, talkPartnerPosition) >= reachedMinDistance) {
			isReached = false;
		}
		return isReached;
	}

	/// <summary>
	/// Citizen approaches visible target; if it is also a Citizen NPC and starts talk animation
	/// </summary>
	[Task]
	public bool Talk(){

		bool retVal = false;

		if (vision != null) {
			GameObject closestCitizen = vision.GetClosestVisibleGO ();

			if (closestCitizen != null) {
				AICitizen closestCitAI = closestCitizen.GetComponent<AICitizen> ();
				if (closestCitAI != null) {
					closestCitAI.StandStill ();
					retVal = true;
				}

			}
		}


		return retVal;

	}


	[Task]
	public bool Testtalk(){
		int randInt = UnityEngine.Random.Range (1, 101);
		bool retVal = (randInt > 50) ? true : false;
		return retVal;
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
