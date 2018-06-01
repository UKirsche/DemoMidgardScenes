using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParserFertigkeiten : DialogParser {

	private MidgardCharacterFertigkeitenChecker midgardCharacterChecker;


	/// <summary>
	/// Initializes a new instance of the <see cref="DialogParserFertigkeiten"/> class.
	/// </summary>
	public DialogParserFertigkeiten(){
		midgardCharacterChecker = new MidgardCharacterFertigkeitenChecker ();
	}


	/// <summary>
	/// Gets the leaves: Holt zu jedem Startknoten die entsprechenden Kinder ab.
	/// Voraussetzung ist immer der aktuelle Startknoten ist gesetzt. 
	/// Mögliche Startknoten sind: Optionspaket, Option, Mission
	/// </summary>
	/// <returns>Infos.</returns>
	public List<Info> GetInfosByFertigkeit(){
		List<Info> returnList=null;	
		if(StartNode!=null && StartNode.nodeElement!=null){
			if (StartNode.typeNodeElement == typeof(Optionspaket)) {
				if (HasNodeTypeFertigkeit<Optionspaket> (StartNode.nodeElement as Optionspaket)) {
					if (midgardCharacterChecker.CheckFertigkeit<Optionspaket> (StartNode.nodeElement as Optionspaket)) {
						SetParentOptionalStartNodes (); //OptionsListe gesetzt, kein Rückgabe
					} else {
						MoveUpward ();
						GetInfosByFertigkeit (); //Hole nächsten Knoten ab
					}
				}
			} else {
				if(StartNode.typeNodeElement==typeof(Mission)) { //Falls Startknoten Mission oder Option (einzige weiteren Knoten mit Lauf nach unten
					if (HasNodeTypeFertigkeit<Mission> (StartNode.nodeElement as Mission)) {
						if (midgardCharacterChecker.CheckFertigkeit<Mission> (StartNode.nodeElement as Mission)) {
							
						}
					}
					return MoveNextForInfoPaket ();
				}
			}
		}

		return returnList;
	}
		


	/// <summary>
	/// Checks the item fertigkeit.
	/// </summary>
	private bool HasNodeTypeFertigkeit<T>(T startNodeElement) where T:IID {
		bool retVal=false;
		if (startNodeElement.id > 0) {
			retVal = true;
		}
		return retVal;
	}

}
