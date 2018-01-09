using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class BasicAICitizen : BasicAINPC {

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


	/// <summary>
	/// Citizen approaches visible target if it is also an NPC and starts talk animation
	/// </summary>
	[Task]
	public void Talk(){
		//Hole Visibles ab
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
		if (!isDestinationReached ()) {
			base.MoveToDestination (singleBreakPoint.transform.position);
		} else if(isDestinationReached()){
			agent.speed = 0;
			character.gameObject.transform.Rotate(0,-90,0);
			character.Move (Vector3.zero);
			character.updateAnimatorStop ();

			IsBreak = false;
			Debug.Log ("Forward: " + character.m_ForwardAmount);
		}
	}

}
