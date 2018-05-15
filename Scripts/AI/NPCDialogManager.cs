using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogManager : ArtifactDialogManager {

	//StandardInfos
	private NPC npcDialogs;
	private DialogParser dialogParser;
	private StandardNPCInfos standardInfos;


	//Anfangszustand: NPC hat Infos zu vergeben
	private bool wasInformand;


	protected bool HasMissionPakets(){
		bool retVal = (npcDialogs != null && npcDialogs.missionen.Count > 0) ? true : false;
		return retVal;
	}

	// Use this for initialization
	public override void Start () {
		base.Start();
		wasInformand = HasMissionPakets() || HasInfoPakets();
		InitializeDialogParser();
		standardInfos = new StandardNPCInfos (artifactName);
		
	}

	/// <summary>
	/// Überschreibt die Methode
	/// </summary>
	protected override void LoadDialog(){
		GameObject scripts = GameObject.Find ("Scripts");
		DialogLoader dialogLoader = scripts.GetComponent<DialogLoader> ();
		npcDialogs = dialogLoader.GetDialog<NPC> (artifactName) as NPC;
	}


	/// <summary>
	/// Initializes the dialog parser with the first Mission. There is always min 1 Mission
	/// </summary>
	private void InitializeDialogParser(){
		dialogParser = new DialogParser ();
		Mission mission = npcDialogs.missionen [0];
		dialogParser.StartNode.nodeElement = mission;
		dialogParser.StartNode.typeNodeElement = typeof(Mission);
		dialogParser.StartNode.typeParentNodeElement = null;
		dialogParser.StartNode.parentNode = null;
	}

	private bool HasInfoPakets(){
		bool retVal = (npcDialogs != null && npcDialogs.Count > 0) ? true : false;
		return retVal;
	}

	/// <summary>
	/// Gets the next info package from Dialogpack for NPC and removes it from the list
	/// </summary>
	public List<string> GetNextDialog(){

	}

	public bool IsOptionDialog(){
	}


	/// <summary>
	/// Gets the next infos: 
	/// Schnittstelle für die neueren Infos. Ersetzt GetNextInfoPackage, bei dem DialogWindow die einzelnen Infos durchgeht
	/// </summary>
	/// <returns>The next infos.</returns>
	public List<Info> GetNextInfos(){
	}



	/// <summary>
	/// Gets the next options (s.o.
	/// </summary>
	/// <returns>The next options.</returns>
	public List<Option> GetNextOptions(){
	}



	/// <summary>
	/// Get Standard Info for a Character
	/// </summary>
	public Infopaket GetStandardInfo(){
		if (HasInfoPakets ()) {
			return standardInfoTalker;
		} else {
			if (wasInformand) {
				return standardInfoFinish;
			}
			return standardInfoName;
		}
	}

	#region Standardauskünfte
	private void CreateStandardInfoPakets(){
		standardInfoName = new Infopaket ();
		standardInfoName.infos.Add (CreateStandardInfo (INFO_NAME + npcName));
		standardInfoTalker = new Infopaket ();
		standardInfoTalker.infos.Add (CreateStandardInfo (INFO_NAME + npcName));
		standardInfoTalker.infos.Add (CreateStandardInfo (INFO_TALKER));
		standardInfoFinish = new Infopaket ();
		standardInfoFinish.infos.Add (CreateStandardInfo (INFO_FINISH));
	}

	public Info CreateStandardInfo(string content){
		Info info = new Info ();
		info.content = content;
		return info;
	}
	#endregion
}
