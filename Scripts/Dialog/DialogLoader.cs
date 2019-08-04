using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Dialog loader: Lädt die Dialog für die NPCs und die Artifacts (statistische Info-Lieferanten).
/// Zu diesem Zweck werden die gesamten Dialog zu Beginn der Scene in zwei Dictionaries geladen
/// - <see cref="artifactByName"/>
/// - <see cref="npcByName"/>
/// 
/// </summary>
public class DialogLoader : MonoBehaviour {

	NPCS npcDialogs=null;
	Artifacts artifactDialogs=null;

	Dictionary<string,Artifact> artifactByName;
	Dictionary<string,NPC> npcByName;

	// Use this for initialization
	void Awake () {
		npcDialogs = SceneResourceReader.GetMidgardResource<NPCS> (SceneResourceReader.MidgardNPC);
		artifactDialogs = SceneResourceReader.GetMidgardResource<Artifacts> (SceneResourceReader.MidgardArtficats);

		artifactByName = new Dictionary<string, Artifact> ();
		npcByName = new Dictionary<string, NPC> ();

		//Konvertiere
		ConvertListsToDictionary<Artifact> (artifactDialogs.artifactListe);
		ConvertListsToDictionary<NPC> (npcDialogs.npcListe);
	}


	/// <summary>
	/// Converts the lists to dictionary.
	/// </summary>
	private void ConvertListsToDictionary<T>(List<T> dialogList){

		foreach (var listElement in dialogList) {
			if (typeof(T) == typeof(Artifact)) {
				Artifact artifactElement = listElement as Artifact;
				artifactByName.Add (artifactElement.name, artifactElement);	
			} else {
				NPC npcElement = listElement as NPC;
				npcByName.Add (npcElement.name, npcElement);	
			}
		}
		
	}


	/// <summary>
	/// Returns all dialogs for NPC with Name
	/// </summary>
	/// <returns>The dialog by NP.</returns>
	/// <param name="npcName">Npc name.</param>
	public T GetDialog<T>(string name){
		if (typeof(T) == typeof(Artifact)) {
			return (T) Convert.ChangeType(artifactByName [name], typeof(T));
		} else {
			return (T) Convert.ChangeType(npcByName [name], typeof(T));
		}
	}
}
