using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateVertical : MonoBehaviour {

	public GameObject prefabDialogText;
	private string test="Ich möchte gerade nicht reden";
	public int numCount;
	private List<GameObject> dialogTextElements;

	// Use this for initialization
	void Start () {
		dialogTextElements = new List<GameObject> ();
		Populate ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Populate(){
		for (int i = 0; i < numCount; i++) {
			addDialogText (test);
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
}
