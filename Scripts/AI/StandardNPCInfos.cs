using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardNPCInfos {

	const string INFO_NAME = "Hallo, mein Name ist ";
	const string INFO_TALKER = "Ich kann dir so manches erzählen, mein Freund ";
	const string INFO_FINISH = "Das war alles...";

	private string npcName="";

	public StandardNPCInfos(string _npcName){
		npcName = _npcName;
		CreateStandardInfos ();
	}

	//Getters
	private List<string> standardInfoName;
	public List<string> StandardInfoName {
		get{ return standardInfoName;}
	}
	private List<string> standardInfoFinish;
	public List<string> StandardInfoFinish {
		get{ return standardInfoFinish;}
	}
	private List<string> standardInfoTalker;
	public List<string> StandardInfoTalker {
		get{ return standardInfoTalker;}
	}

	private void CreateStandardInfos(){
		standardInfoName = new List<string> ();
		standardInfoName.Add (INFO_NAME + npcName);
		standardInfoTalker = new List<string> ();
		standardInfoTalker.Add (INFO_NAME + npcName);
		standardInfoTalker.Add (INFO_TALKER);
		standardInfoFinish = new List<string> ();
		standardInfoFinish.Add (INFO_FINISH);
	}


}
