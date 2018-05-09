using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Dialog loader: Allows loading Dialogs for NPC and Artifacts
/// </summary>
public class DialogLoader : MonoBehaviour {

	NPCS npcDialogs=null;
	Artifacts artifactDialogs=null;

	// Use this for initialization
	void Awake () {
		npcDialogs = SceneResourceReader.GetMidgardResource<NPCS> (SceneResourceReader.MidgardNPC);
		artifactDialogs = SceneResourceReader.GetMidgardResource<Artifacts> (SceneResourceReader.MidgardArtficats);
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

	/// <summary>
	/// Returns all dialogs for an Artifact with Name
	/// </summary>
	/// <returns>The dialog by NP.</returns>
	/// <param name="npcName">Npc name.</param>
	public Artifact GetDialogByArtifact(string artifactName){
		foreach (var artifact in artifactDialogs.artifactListe) {
			if(artifact.name.Equals(artifactName)){
				return artifact;
			}
		}
		return null;
	}
}
