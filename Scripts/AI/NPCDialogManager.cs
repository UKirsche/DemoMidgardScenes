using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogManager : MonoBehaviour {

	//npcName isGO name
	public string npcName;
	private NPC npcDialogs;

	// Use this for initialization
	void Start () {
		npcName = gameObject.name;
		GameObject scripts = GameObject.Find ("Scripts");
		NPCDialogLoader dialogLoader = scripts.GetComponent<NPCDialogLoader> ();
		npcDialogs = dialogLoader.GetDialogByNPC (npcName);
	}
		

	public void GetNPCDialogs(){
	}
}
