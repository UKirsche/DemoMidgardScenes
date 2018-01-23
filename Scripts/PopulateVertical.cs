using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateVertical : MonoBehaviour {

	public GameObject prefabDialogText;
	private string test="blalbalbal, ist ja wunderbar was hier so alles abgeht,\n ihr kiddies ";
	public int numCount;

	// Use this for initialization
	void Start () {
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
	/// Adds the dialog text to the Vertical Layout Group
	/// </summary>
	public void addDialogText(string dialogString){
		GameObject newDialog;
		newDialog = Instantiate (prefabDialogText, transform); //ensures that
		newDialog.GetComponent<Text> ().text = dialogString;
	}
}
