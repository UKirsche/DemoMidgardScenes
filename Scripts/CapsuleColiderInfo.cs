using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleColiderInfo : MonoBehaviour {
	public GameObject dialogView;
	private Infopaket dialogPackage;
	private PopulateVertical populateDialog;

	// Use this for initializatio
	void Start () {
		populateDialog = dialogView.GetComponentsInChildren<PopulateVertical> ()[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/// <summary>
	/// Activates the dialog window in GameScene with Standardinfo
	/// </summary>
	/// <param name="activate">If set to <c>true</c> activate.</param>
	public bool SetActiveDialogView(){
		dialogView.SetActive (true);
		var npcDialogManager = GetComponent<NPCDialogManager> ();
		var infopaket = npcDialogManager.GetNextInfoPackage ();
		DialogButtonWrapper.DisplayInfoPackage (populateDialog, infopaket);

		return true;
	}



	/// <summary>
	/// Raises the trigger enter event. Only valid for Gameobjects with Rigidbody and that include a trigger type.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter( Collider other )
	{
		SetActiveDialogView ();
	}

	void OnTriggerExit(Collider other)
	{
		dialogView.SetActive (false);
	}

}
