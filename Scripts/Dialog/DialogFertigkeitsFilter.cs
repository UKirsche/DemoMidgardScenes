using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Filtert die Dialog gemäß den Fertigkeiten
/// </summary>
public class DialogFertigkeitsFilter {

	private DialogParser dialogParser;
	public bool IsFilterDialog { get; set;}


	public DialogFertigkeitsFilter(DialogParser _dialogParser){

		dialogParser = _dialogParser;
		IsFilterDialog = false;
	}


	public List<string> FilterDialog(){
		if (IsFilterDialog) {
			

		} else {
			return dialogParser.GetInfos ();
		}
	}

}
