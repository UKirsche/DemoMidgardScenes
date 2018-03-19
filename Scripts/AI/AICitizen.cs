using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Panda;

[RequireComponent (typeof(ThirdPersonNPCNormal))]
public class AICitizen : AIHuman {

	protected GameObject talkPartner;
	protected GameObject sitOpportunity;

	public string wayPointStringBreakSit;

	// Use Initialization from BaseClass
	public override void Start () {
		base.Start ();
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
	/// Is sit opportunity reached reached.
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
		ThirdPersonNPCNormal meThird = (ThirdPersonNPCNormal)character;
		meThird.Sit ();
		return true;
	}


	[Task]
	public bool StopSit(){
		ThirdPersonNPCNormal meThird = (ThirdPersonNPCNormal)character;
		meThird.StopSit ();
		return true;
	}

	#endregion


	#region NPC Talk Tasks



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
		ThirdPersonNPCNormal meThird = (ThirdPersonNPCNormal)character;
		meThird.Talk ();
		return true;
	}

	/// <summary>
	/// Citizen approaches visible target; if it is also a Citizen NPC and starts talk animation
	/// </summary>
	[Task]
	public bool StopTalk(){
		ThirdPersonNPCNormal meThird = (ThirdPersonNPCNormal)character;
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


}
