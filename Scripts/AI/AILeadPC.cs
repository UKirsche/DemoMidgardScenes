using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Panda;

public class AILeadPC : AIHuman {

	public string wayPointStringHome;

	protected GameObject talkPartner;
	public GameObject goalPoint;
	private bool isGoalPointActive = false;
	public GameObject dialogView;
	private PopulateVertical populateDialog;
	private bool setDialogOnce=false;



	// Use Initialization from BaseClass
	public override void Start () {
		base.Start ();

		populateDialog = dialogView.GetComponentsInChildren<PopulateVertical> ()[0];

		IsStroll = false;
		talkPartner = null;
		isGoalPointActive = false;
	}

	[Task]
	public bool SetGoalPointActive(){
		isGoalPointActive = true;
		return isGoalPointActive;
	}

	/// <summary>
	/// Moves to info, where NPC leads PC
	/// </summary>
	/// <returns><c>true</c>, if to info point was moved, <c>false</c> otherwise.</returns>
	[Task]
	public bool MoveToInfoPoint(){
		if (goalPoint != null && isGoalPointActive==true) {
			MoveToDestination (goalPoint.transform.position);
			return true;
		}
		return false;
	}


	/// <summary>
	/// Ises the talk partner reached.
	/// </summary>
	/// <returns><c>true</c>, if talk partner reached was ised, <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool IsInfoPointReached(){
		bool isReached = true;
		if (goalPoint != null) {
			isReached = IsDestinationReached (goalPoint);
		}

		return isReached;
	}

	/// Activates the dialog window in GameScene
	/// </summary>
	/// <param name="activate">If set to <c>true</c> activate.</param>
	[Task]
	public bool SetInActiveDialogView(){
		dialogView.SetActive (false);
		return true;
	}

}