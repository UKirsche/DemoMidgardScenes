using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateVertical : MonoBehaviour {

	public GameObject prefabDialogText;
	public int numCount;
	private List<GameObject> dialogTextElements;

	// Use this for initialization
	public virtual void Awake () {
		dialogTextElements = new List<GameObject> ();
	}
	
	public void Populate(){
		for (int i = 0; i < numCount; i++) {
			addDialogText ("tst");
		}
	}

	/// <summary>
	/// Clears the dialg text.
	/// </summary>
	public void ClearDialogBox(){
		if (dialogTextElements.Count > 0) {
			foreach (var item in dialogTextElements) {
				Destroy (item);
			}
		}
	}

	/// <summary>
	/// Adds the dialog text to the Vertical Layout Group
	/// </summary>
	public void addDialogText(string dialogString){
		GameObject newDialog;
		newDialog = Instantiate (prefabDialogText, transform); //ensures that
		newDialog.GetComponent<Text> ().text = dialogString;
		dialogTextElements.Add (newDialog);
	}


	/// <summary>
	/// Adds the dialog option to the Vertical Group
	/// </summary>
	public void addDialogOption(string dialogString){
		GameObject newDialog;
		newDialog = Instantiate (prefabDialogText, transform); //ensures that
		newDialog.GetComponent<Text> ().text = dialogString;
		dialogTextElements.Add (newDialog);
	}
}
