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
	private Infopaket dialogPackage;



	// Use this for initialization
	void Start () {
		dialogView = GameObject.Find (DialogName);
		dialogView.SetActive (false);
	}


	/// <summary>
	/// Sets the dialog partner for the Player: Always an NPC
	/// </summary>
	/// <param name="talkPartner">Talk partner.</param>
	public void SetDialogPartner(GameObject talkPartner){
		npcTalkPartner = talkPartner;
	}

	/// Holt nächstes Infopaket vom NPC
	/// </summary>
	public Infopaket GetNextDialogPackageFromNPC(){
		Infopaket infopaket = null;;
		if (npcTalkPartner != null) {
			var npcDialogManager = npcTalkPartner.GetComponent<NPCDialogManager> ();
			infopaket = npcDialogManager.GetNextInfoPackage ();

		}

		return infopaket;
	}
		

	#region tasks

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

	#endregion

}
