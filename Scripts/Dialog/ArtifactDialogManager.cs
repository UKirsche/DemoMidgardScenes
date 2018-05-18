using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactDialogManager : MonoBehaviour {

	//Artifact Name to search for in XML
	public string artifactName;
	protected Artifact artifactDialog;
	protected StandardNPCInfos standardInfos;


	// Use this for initialization
	public virtual void Start () {
		artifactName = gameObject.name;
		standardInfos = new StandardNPCInfos (artifactName);
		LoadDialog ();
	}

	protected virtual void LoadDialog(){
		GameObject scripts = GameObject.Find ("Scripts");
		DialogLoader dialogLoader = scripts.GetComponent<DialogLoader> ();
		artifactDialog = dialogLoader.GetDialog<Artifact> (name) as Artifact;
	}
	
	protected bool HasInfos(){
		bool retVal = (artifactDialog != null && artifactDialog.infopakete.Count > 0) ? true : false;
		return retVal;
	}

	/// <summary>
	/// Get Standard Info for a Character
	/// </summary>
	public virtual List<string> GetStandardInfo(){
		return standardInfos.StandardInfoFinish;
	}

	/// <summary>
	/// Gets the next info package from Dialogpack for NPC and removes it from the list.
	/// Altes Interface für einfache Infopakete
	/// </summary>
	public virtual List<string> GetNextInfos(){
		Infopaket infPackage=null;
		List<string> infoStrings = new List<string> ();
		//get first element of the list
		if (HasInfos()) {
			infPackage = artifactDialog.infopakete [0];
			artifactDialog.infopakete.Remove (infPackage);
			List<Info> infos = infPackage.infos;
			if (infos != null) {
				foreach (var info in infos) {
					infoStrings.Add (info.content);
				}
			}
		} 

		return infoStrings;
	}
}
