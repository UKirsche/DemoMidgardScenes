using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDisplayManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Displays a simple Dialog in Rows
	/// </summary>
	/// <param name="infos">Infos.</param>
	public static void DisplayDialogText(List<string> dialogRows, PopulateVertical populateDialog){
		populateDialog.ClearDialogBox ();
		if (dialogRows.Count > 0) {
			foreach (var dialogRow in dialogRows) {
				populateDialog.addDialogText (dialogRow);

			}
		} 
	}

	/// <summary>
	/// Displays a Dialog Page 
	/// </summary>
	/// <param name="infos">Infos.</param>
	public static void DisplayDialogOption(List<string> dialogRows, PopulateVertical populateDialog){
		PopulateVerticalToggle popVertical = populateDialog as PopulateVerticalToggle;
		popVertical.ClearDialogBox ();
		if (dialogRows.Count > 0) {
			foreach (var dialogRow in dialogRows) {
				popVertical.addDialogOption(dialogRow);

			}
		} 
	}
}
