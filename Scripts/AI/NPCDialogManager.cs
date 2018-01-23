using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogManager : MonoBehaviour {

	//npcName isGO name
	public string npcName;
	private List<Infopaket> npcDialogs;


	// Use this for initialization
	void Start () {
		npcName = gameObject.name;
		GameObject scripts = GameObject.Find ("Scripts");
		NPCDialogLoader dialogLoader = scripts.GetComponent<NPCDialogLoader> ();
		npcDialogs = dialogLoader.GetDialogByNPC (npcName).infopakete;
	}
		

	/// <summary>
	/// Gets the next info package from Dialogpack for NPC and removes it from the list
	/// </summary>
	public Infopaket GetNextInfoPackage(){
		Infopaket infPackage=null;
		//get first element of the list
		if (npcDialogs!=null && npcDialogs.Count > 0) {
			infPackage = npcDialogs [0];
		}

		return infPackage;
	}
}
