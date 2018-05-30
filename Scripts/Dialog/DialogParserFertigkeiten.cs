using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParserFertigkeiten : DialogParser {

	const string CHECK_PW = "PW";
	const string CHECK_EW = "EW";

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
			if (mission.id > 0) {
				InventoryItem itemFertigkeit = midgardCharacterChecker.GetFertigkeit (mission.id);
				CheckItemFertigkeitCharacterEW (itemFertigkeit, mission.modifier);
			}
		} else if(StartNode.typeNodeElement==typeof(Optionspaket)) {
			Optionspaket opaket = StartNode.nodeElement as Optionspaket;
			if (opaket.id > 0) {
				InventoryItem itemFertigkeit = midgardCharacterChecker.GetFertigkeit (opaket.id);
				CheckItemFertigkeitCharacterEW (itemFertigkeit, opaket.modifier);
			}
		} else if(StartNode.typeNodeElement == typeof(Option)) {
			Option opt = StartNode.nodeElement as Option;
			if (opt.id > 0) {
				InventoryItem itemFertigkeit = midgardCharacterChecker.GetFertigkeit (opt.id);
				CheckItemFertigkeitCharacterEW (itemFertigkeit, opt.modifier);
			}
		}
	}


	/// <summary>
	/// Checks the item fertigkeit.
	/// </summary>
	private bool CheckItemFertigkeitCharacterEW(InventoryItem item, int modifier=0) {
		InventoryItem itemFertigkeit = midgardCharacterChecker.GetFertigkeit (item.id);
		if (itemFertigkeit != null) {
			bool retVal = midgardCharacterChecker.CheckFertigkeitEW (itemFertigkeit, modifier);
			return retVal;
		}

		return false;
	}

}
