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
	protected bool HasNodeFertigkeit() {
		bool retVal = false;
		if (StartNode.typeNodeElement == typeof(Mission)) {
			Mission node = StartNode.nodeElement as Mission;
			retVal = HasNodeTypeFertigkeit<Mission> (node);
		} else if (StartNode.typeNodeElement == typeof(Optionspaket)) {
			Optionspaket node = StartNode.nodeElement as Optionspaket;
			retVal = HasNodeTypeFertigkeit<Optionspaket> (node);
		} else if (StartNode.typeNodeElement == typeof(Option)) {
			Option node = StartNode.nodeElement as Option;
			retVal = HasNodeTypeFertigkeit<Option> (node);
		}  else if (StartNode.typeNodeElement == typeof(Infopaket)) {
			Infopaket node = StartNode.nodeElement as Infopaket;
			retVal = HasNodeTypeFertigkeit<Infopaket> (node);
		} else if (StartNode.typeNodeElement == typeof(Info)) {
			Info node = StartNode.nodeElement as Info;
			retVal = HasNodeTypeFertigkeit<Info> (node);
		}

		return retVal;
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

	/// Chechkt, ob der aktuelle Knoten eine Fertigkeitsabfrage macht
	/// </summary>
	/// <param name="nextNode">Next node.</param>
	protected bool CheckFertigkeit(DialogNode<object> actNode) {
		bool retVal = false;
		if (actNode.typeNodeElement == typeof(Mission)) {
			Mission node = actNode.nodeElement as Mission;
			retVal =  midgardCharacterChecker.CheckFertigkeit<Mission> (node);
		} else if (actNode.typeNodeElement == typeof(Optionspaket)) {
			Optionspaket node = actNode.nodeElement as Optionspaket;
			retVal =  midgardCharacterChecker.CheckFertigkeit<Optionspaket> (node);
		} else if (actNode.typeNodeElement == typeof(Option)) {
			Option node = actNode.nodeElement as Option;
			retVal =  midgardCharacterChecker.CheckFertigkeit<Option> (node);
		}  else if (actNode.typeNodeElement == typeof(Infopaket)) {
			Infopaket node = actNode.nodeElement as Infopaket;
			retVal =  midgardCharacterChecker.CheckFertigkeit<Infopaket> (node);
		} else if (actNode.typeNodeElement == typeof(Info)) {
			Info node = actNode.nodeElement as Info;
			retVal =  midgardCharacterChecker.CheckFertigkeit<Info> (node);
		}

		return retVal;
	}

}
