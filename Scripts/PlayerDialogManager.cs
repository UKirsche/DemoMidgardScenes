using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panda;

public class PlayerDialogManager : MonoBehaviour {

	//Name of DialogView
	public const string DialogName = "DialogView";
	private GameObject dialogView;
	private GameObject npcTalkPartner;
	private Infopaket dialogPackage;
	private PopulateVertical populateDialog;



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

	/// Holt nächstes Infopaket vom NPC
	/// </summary>
	public List<string> GetNextDialogPackageFromNPC(){
		List<string> dialogRows = null;
		if (npcTalkPartner != null) {
			var npcDialogManager = npcTalkPartner.GetComponent<NPCDialogManager> ();
			dialogRows = npcDialogManager.GetNextDialog ();

		}

		return dialogRows;
	}

	/// <summary>
	/// If collected dialog is a list of options
	/// </summary>
	/// <returns><c>true</c> if this instance is dialog option from NP; otherwise, <c>false</c>.</returns>
	public bool IsDialogOptionFromNPC(){
		var npcDialogManager = npcTalkPartner.GetComponent<NPCDialogManager> ();
		return npcDialogManager.IsOptionDialog ();
	}
		

	#region tasks

	/// <summary>
	/// Activates the dialog window in GameScene with Standardinfo
	/// </summary>
	/// <param name="activate">If set to <c>true</c> activate.</param>
	[Task]
	public bool SetActiveDialogView(){
		if (dialogView.activeSelf==false) {
			dialogView.SetActive (true);
			var npcDialogManager = npcTalkPartner.GetComponent<NPCDialogManager> ();

			Infopaket standardPaket = npcDialogManager.GetStandardInfo ();
			DialogButtonWrapper.DisplayDialog (populateDialog, standardPaket);
		}

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
