using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParserFertigkeiten : DialogParser {


	private MidgardCharacterFertigkeitenChecker midgardCharacterChecker;



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


	/// Chechkt, ob der aktuelle Startknoten eine Fertigkeitsabfrage macht
	/// </summary>
	/// <param name="nextNode">Next node.</param>
	protected void CheckFertigkeit(){
		if(StartNode.typeNodeElement==typeof(Mission)) {
			Mission mission = StartNode.nodeElement as Mission;
			if (mission.id) {
				InventoryItem itemFertigkeit = midgardCharacterChecker.GetFertigkeit (mission.id);
				if (itemFertigkeit != null) {
					midgardCharacterChecker.CheckFertigkeitEW (itemFertigkeit, 0); //check auf nullmodifier
					
				}
			}
		} else if(StartNode.typeNodeElement==typeof(Optionspaket)) {
		} else if(StartNode.typeNodeElement == typeof(Option)) {
		}
	}

}
