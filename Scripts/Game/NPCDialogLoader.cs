using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogLoader : MonoBehaviour {

	NPCS npcDialogs;

	// Use this for initialization
	void Start () {
		npcDialogs = SceneResourceReader.GetMidgardResource<NPCS> (SceneResourceReader.MidgardNPC);
	}

	/// <summary>
	/// Returns all dialogs for NPC with Name x
	/// </summary>
	/// <returns>The dialog by NP.</returns>
	/// <param name="npcName">Npc name.</param>
	public NPC GetDialogByNPC(string npcName){
		foreach (var npc in npcDialogs.npcListe) {
			if(npc.name.Equals(npcName)){
				return npc;
			}
		}
		return null;
	}
}
