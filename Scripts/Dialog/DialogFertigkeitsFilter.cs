using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Filtert die Dialog gemäß den Fertigkeiten
/// </summary>
public class DialogFertigkeitsFilter {

	private DialogParser dialogParser;


	public DialogFertigkeitsFilter(DialogParser _dialogParser){

		dialogParser = _dialogParser;
	}


	public void FilterDialog(){

		if (dialogParser is DialogParser) {
		} else if (dialogParser is DialogParserCharFertigkeiten) {
			
		}
		
	}

}
