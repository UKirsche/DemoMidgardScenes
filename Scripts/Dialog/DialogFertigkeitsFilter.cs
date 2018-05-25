using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Schaltet Fertigkeitsfilter für Dialog an und aus
/// </summary>
public class DialogFertigkeitsFilter : MonoBehaviour
{

	private DialogParser dialogParser;
	private bool isFertigkeitsFilter;

	public bool IsFertigkeitsFilter {
		get { return isFertigkeitsFilter;}
		set { 
			if (value == false) {
				dialogParser = new DialogParser ();	

			} else {
				dialogParser = new DialogParserCharFertigkeiten ();
			}
			isFertigkeitsFilter = value;
		}
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
