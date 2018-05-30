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
	protected bool CheckFertigkeit() {
		bool retVal = false;
		if (StartNode.typeNodeElement == typeof(Mission)) {
			Mission node = StartNode.nodeElement as Mission;
			retVal =  CheckItemFertigkeitCharacter<Mission> (node);
		} else if (StartNode.typeNodeElement == typeof(Optionspaket)) {
			Optionspaket node = StartNode.nodeElement as Optionspaket;
			retVal =  CheckItemFertigkeitCharacter<Optionspaket> (node);
		} else if (StartNode.typeNodeElement == typeof(Option)) {
			Option node = StartNode.nodeElement as Option;
			retVal =  CheckItemFertigkeitCharacter<Option> (node);
		}  else if (StartNode.typeNodeElement == typeof(Infopaket)) {
			Infopaket node = StartNode.nodeElement as Infopaket;
			retVal =  CheckItemFertigkeitCharacter<Infopaket> (node);
		} else if (StartNode.typeNodeElement == typeof(Info)) {
			Info node = StartNode.nodeElement as Info;
			retVal =  CheckItemFertigkeitCharacter<Info> (node);
		}

		return retVal;
	}


	/// <summary>
	/// Checks the item fertigkeit.
	/// </summary>
	private bool CheckItemFertigkeitCharacter<T>(T startNodeElement) where T:IFertigkeitsCheck {
		bool retVal=false;
		if (startNodeElement.id > 0) {
			InventoryItem itemFertigkeitCharacter = midgardCharacterChecker.GetFertigkeit (startNodeElement.id);
			retVal = CheckFertigkeitByMidgardCharacter (itemFertigkeitCharacter, startNodeElement.modifier, startNodeElement.checkType);
		}
		return retVal;
	}

	/// <summary>
	/// Checks the fertigkeit by MidgardCharacter character.
	/// </summary>
	/// <returns><c>true</c>, if fertigkeit by middle character was checked, <c>false</c> otherwise.</returns>
	/// <param name="itemFertigkeitCharacter">Item fertigkeit character.</param>
	/// <param name="modifier">Modifier.</param>
	/// <param name="checkType">Check type.</param>
	private bool CheckFertigkeitByMidgardCharacter(InventoryItem itemFertigkeitCharacter, int modifier, string checkType){
		bool retVal = false;
		if (itemFertigkeitCharacter != null) {
			if (checkType.Equals (CHECK_EW)) {
				retVal = midgardCharacterChecker.CheckFertigkeitEW (itemFertigkeitCharacter, modifier);
			}
			else if (checkType.Equals (CHECK_PW)) {
				retVal = midgardCharacterChecker.CheckFertigkeitPW (itemFertigkeitCharacter, modifier);
			}

			return retVal;
		}

		return false;
	}

}
