using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactDialogManager : MonoBehaviour {

	//Artifact Name to search for in XML
	public string artifactName;
	private Artifact artifactDialog;


	// Use this for initialization
	public virtual void Start () {
		artifactName = gameObject.name;
		LoadDialog ();
	}

	protected virtual void LoadDialog(){
		GameObject scripts = GameObject.Find ("Scripts");
		DialogLoader dialogLoader = scripts.GetComponent<DialogLoader> ();
		artifactDialog = dialogLoader.GetDialog<Artifact> (name) as Artifact;
	}
	
	protected bool HasInfoPakets(){
		bool retVal = (artifactDialog != null && artifactDialog.infopakete.Count > 0) ? true : false;
		return retVal;
	}

	/// <summary>
	/// Gets the next info package from Dialogpack for NPC and removes it from the list.
	/// Altes Interface für einfache Infopakete
	/// </summary>
	public Infopaket GetNextInfoPackage(){
		Infopaket infPackage=null;
		//get first element of the list
		if (HasInfoPakets()) {
			infPackage = artifactDialog.infopakete [0];
			artifactDialog.infopakete.Remove (infPackage);
		} 

		return infPackage;
	}
}
