using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panda;

public class PlayerDialogManager : MonoBehaviour {

	//Name of DialogView
	const string DialogName = "DialogView";
	private GameObject dialogView;
	private GameObject npcTalkPartner;
	private PopulateVertical populateDialog;
	private Infopaket dialogPackage;



	// Use this for initialization
	void Start () {
		dialogView = GameObject.Find (DialogName);
		populateDialog = dialogView.GetComponentsInChildren<PopulateVertical> ()[0];
		dialogView.SetActive (false);


	}


	/// <summary>
	/// Sets the dialog partner for the Player: Always an NPC
	/// </summary>
	/// <param name="talkPartner">Talk partner.</param>
	public void SetDialogPartner(GameObject talkPartner){
		npcTalkPartner = talkPartner;
	}




	/// <summary>
	/// Activates the dialog window in GameScene
	/// </summary>
	/// <param name="activate">If set to <c>true</c> activate.</param>
	[Task]
	public bool SetActiveDialogView(){
		dialogView.SetActive (true);
		return true;
	}

	/// <summary>
	/// Activates the dialog window in GameScene
	/// </summary>
	/// <param name="activate">If set to <c>true</c> activate.</param>
	[Task]
	public bool SetInActiveDialogView(){
		dialogView.SetActive (false);
		return true;
	}

	/// <summary>
	/// Holt nächstes Infopaket vom NPC
	/// </summary>
	[Task]
	public void GetNextDialogPackageFromNPC(){
		if (npcTalkPartner != null) {
			
		}
	}

	/// <summary>
	/// Determines whether this instance sees a possibility to make a little break
	/// </summary>
	/// <returns><c>true</c> if this instance is break visible; otherwise, <c>false</c>.</returns>
	[Task]
	public bool HasDialogPartner(){
		bool retVal = false;
		if (npcTalkPartner != null) {
			retVal = true;
		}

		return retVal;
	}
}
