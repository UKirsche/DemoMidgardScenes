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


    /// <summary>
    /// Gets the next dialog für den PC vom NPC.
    /// </summary>
    public void GetNextDialog()
    {
		GameObject scripts = GameObject.Find ("Scripts");
		DialogDisplayManager dialogDisplay = scripts.GetComponent<DialogDisplayManager> ();
		dialogDisplay.DisplayNextDialog (populateDialog as PopulateVerticalToggle);
	}

}
