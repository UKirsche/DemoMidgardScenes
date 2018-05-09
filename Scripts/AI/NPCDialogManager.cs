using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogManager : MonoBehaviour {


	const string INFO_NAME = "Hallo, mein Name ist ";
	const string INFO_TALKER = "Ich kann dir so manches erzählen, mein Freund ";
	const string INFO_FINISH = "Das war alles...";




	//npcName isGO name
	public string npcName;


	//StandardInfos
	private NPC npcDialogs;
	private DialogParser dialogParser;
	private Infopaket standardInfoName;
	private Infopaket standardInfoFinish;
	private Infopaket standardInfoTalker;

	//Anfangszustand: NPC ist beladen
	private bool wasInformand;


	// Use this for initialization
	void Start () {
		
		npcName = gameObject.name;
		GameObject scripts = GameObject.Find ("Scripts");
		DialogLoader dialogLoader = scripts.GetComponent<DialogLoader> ();
		npcDialogs = dialogLoader.GetDialog<NPC> (npcName);
		wasInformand = HasInfoPakets();

		//DialogParser
		InitializeDialogParser();

		//Erzeugt Standard-Infopakete
		CreateStandardInfoPakets ();
		
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
