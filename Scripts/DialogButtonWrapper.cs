using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogButtonWrapper : MonoBehaviour {

	public GameObject dialogView;
	private PopulateVertical populateDialog;


	// Hole populateDialog-Skript
	void Start () {
		populateDialog = dialogView.GetComponentsInChildren<PopulateVertical> ()[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/// <summary>
	/// Gets the next dialog für den PC vom NPC.
	/// </summary>
	public void GetNextDialog(){
		GameObject playerObject = GameObject.FindGameObjectWithTag (DemoRPGMovement.PLAYER_NAME);
		PlayerDialogManager playerDialogManager = playerObject.GetComponent<PlayerDialogManager> ();
		Infopaket infopaket = playerDialogManager.GetNextDialogPackageFromNPC ();
		if (infopaket != null) {
			DisplayInfoPackage (populateDialog, infopaket);
		}
	}

	/// <summary>
	/// Displays the info package on the dialog
	/// </summary>
	/// <param name="infos">Infos.</param>
	public static void DisplayInfoPackage(PopulateVertical popDialog, Infopaket infoPaket){
		popDialog.ClearDialogBox ();
		if (infoPaket.infos != null && infoPaket.infos.Count > 0) {
			foreach (var info in infoPaket.infos) {
				popDialog.addDialogText (info.content);
			}
		} 
	}

}
