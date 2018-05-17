using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateVerticalToggle : PopulateVertical {

	public GameObject prefabDialogToggle;
	public ToggleGroup toggleGroup;
	private List<GameObject> dialogToggleElements;


	// Use Initialization from BaseClass
	public override void Awake () {
		base.Awake ();
		dialogToggleElements = new List<GameObject> ();
		toggleGroup = GetComponent<ToggleGroup> ();
	}


	/// <summary>
	/// Clears the dialg text.
	/// </summary>
	public override void ClearDialogBox(){
		if (dialogToggleElements.Count > 0) {
			foreach (var item in dialogToggleElements) {
				Destroy (item);
			}
		}
	}

	/// <summary>
	/// Adds the dialog option to the Vertical Group
	/// </summary>
	public void addDialogOption(string dialogString){
		GameObject newDialog;
		newDialog = Instantiate (prefabDialogToggle, transform); //ensures that
		newDialog.GetComponent<Toggle>().group = toggleGroup;
		newDialog.GetComponentInChildren<Text> ().text = dialogString;
		dialogToggleElements.Add (newDialog);
	}

}
