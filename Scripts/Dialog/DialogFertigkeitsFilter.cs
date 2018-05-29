using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Schaltet Fertigkeitsfilter für Dialog an und aus
/// </summary>
public class DialogFertigkeitsFilter 
{

	private DialogParser dialogParser;
	private bool isFertigkeitsFilter;
	private NPC npcDialogs;

	/// <summary>
	/// Initializes a new instance of the <see cref="DialogFertigkeitsFilter"/> class.
	/// </summary>
	public DialogFertigkeitsFilter(NPC dialogs){
		npcDialogs = dialogs;
		InitializeDialogParser();
	}


	/// <summary>
	/// Initializes the dialog parser with the first Mission. There is always min 1 Mission
	/// </summary>
	private void InitializeDialogParser(){
		dialogParser = new DialogParserCharFertigkeiten ();
		dialogParser.StartNode = new DialogNode<object> ();
		Mission mission = npcDialogs.missionen [0];
		dialogParser.StartNode.nodeElement = mission;
		dialogParser.StartNode.typeNodeElement = typeof(Mission);
		dialogParser.StartNode.typeParentNodeElement = null;
		dialogParser.StartNode.parentNode = null;
	}

	public bool IsFertigkeitsFilter {
		get { return isFertigkeitsFilter;}
		set {isFertigkeitsFilter = value;}
	}


	public List<string> GetNextDialog(){
		List<string> returnList = null;
		bool isOption = dialogParser.IsOption;
		returnList = GetNextInfos ();
		if (isOption) {
			returnList = FormatOptions ();
		}
		return returnList;
	}

	/// <summary>
	/// Gets the next infos: 
	/// Liefert die nächsten NPC-Infos als Liste von strings
	/// </summary>
	/// <returns>The next infos.</returns>
	public List<string> GetNextInfos(){
		List<string> infoStrings = new List<string> ();
		List<Info> infos = dialogParser.GetInfos ();
		if (infos != null) {
			foreach (var info in infos) {
				infoStrings.Add (info.content);
			}
		}

		return infoStrings;
	}

	/// <summary>
	/// Sets the index of the chosen option.
	/// </summary>
	/// <param name="index">Index.</param>
	public void SetChosenOptionIndex(int index){
		if (index >= 0) {
			dialogParser.SetParentOptionalStartNodeIndex (index);
		}
	}


	public bool NextDialogOption(){
		return dialogParser.IsOption;
	}

	/// <summary>
	/// Liefert die Optionen (Auswahlmöglichkeiten) als Liste von strings
	/// </summary>
	/// <returns>The next options.</returns>
	private List<string> FormatOptions(){
		List<string> optionStrings = new List<string> ();
		List<DialogNode<object>> optionNodes = dialogParser.optionalStartNodes;
		foreach (var optionNode in optionNodes) {
			Option nodeElement = optionNode.nodeElement as Option;
			optionStrings.Add (nodeElement.Beschreibung);
		}

		return optionStrings;
	}

	public List<Info> FilterDialog ()
	{
		if (IsFertigkeitsFilter) {
			DialogParserCharFertigkeiten dialogParserFertigkeiten = dialogParser as DialogParserCharFertigkeiten;
			return dialogParserFertigkeiten.GetInfosByFertigkeit ();
			
		} else {
			return dialogParser.GetInfos();
		}
		
	}

}
