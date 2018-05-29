using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParserFertigkeiten : DialogParser {



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
				SetParentOptionalStartNodes (); //OptionsListe gesetzt, kein Rückgabe

			} else if(StartNode.typeNodeElement==typeof(Mission)|| StartNode.typeNodeElement==typeof(Option)) { //Falls Startknoten Mission oder Option (einzige weiteren Knoten mit Lauf nach unten
				DialogNode<object> nextNode = new DialogNode<object> ();
				SetNextNodeParentType (nextNode);
				nextNode.typeNodeElement = typeof(Infopaket);
				Infopaket infopaket = GetNextInfoPaket();
				SetParent (infopaket, nextNode);
				SetStartNode(nextNode);
				returnList = infopaket.infos;
			}
		}

		return returnList;
	}
}
