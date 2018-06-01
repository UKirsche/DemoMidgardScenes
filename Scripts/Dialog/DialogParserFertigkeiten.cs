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
						returnList = GetInfosByFertigkeit (); //Hole nächsten Knoten ab
					}
				}
			} else {
				if(StartNode.typeNodeElement==typeof(Mission)) { //Falls Startknoten Mission oder Option (einzige weiteren Knoten mit Lauf nach unten
					if (HasNodeTypeFertigkeit<Mission> (StartNode.nodeElement as Mission)) {
						if (midgardCharacterChecker.CheckFertigkeit<Mission> (StartNode.nodeElement as Mission)) {
							return ManageInfopaket ();
						} else {
							MoveNextForMission ();
						}
					}
					if (ManageInfopaket () == null) {
						MoveUpward ();
						returnList = GetInfosByFertigkeit (); //Hole nächsten Knoten ab
					}
				}
				else if(StartNode.typeNodeElement==typeof(Option)) { //Falls Startknoten Mission oder Option (einzige weiteren Knoten mit Lauf nach unten
					if (ManageInfopaket () == null) {
						MoveUpward ();
						returnList = GetInfosByFertigkeit (); //Hole nächsten Knoten ab
					}
				}
			}
		}

		return returnList;
	}


	/// <summary>
	/// Sets the parent optional start node: 
	/// Methode wird nur bei Optionspaketen gebraucht, welche die optionalen Punkte in einer Liste festlegt
	/// </summary>
	protected override void SetParentOptionalStartNodes(){
		base.SetParentOptionalStartNodes ();
		FilterOptionalStartNodes ();
	}

	/// <summary>
	/// Filters the optional start nodes
	/// Falls der Knoten einen Fertigkeitsverweise hat
	/// </summary>
	private void FilterOptionalStartNodes ()
	{
		foreach (var option in optionalStartNodes) {
			Option optionElement = option.nodeElement as Option;
			if (HasNodeTypeFertigkeit<Option> (optionElement)) {
				if (!midgardCharacterChecker.CheckFertigkeit<Option> (StartNode.nodeElement as Option)) {
					optionalStartNodes.Remove (option);
				}
			}
		}
	}


	/// <summary>
	/// Gets the next info paket:
	/// Für jeden Startknoten muss das nächste Infopaket geholt werden (bis auf Optionspaket, das Optionen hat)
	/// </summary>
	/// <returns>The next info paket.</returns>
	protected override Infopaket GetInfopaket(){
		Infopaket returnPaket = base.GetInfopaket ();
		return FilterInfopaket (returnPaket);

	}

	/// <summary>
	/// Filters the optional start nodes
	/// Falls der Knoten einen Fertigkeitsverweise hat
	/// </summary>
	private Infopaket FilterInfopaket (Infopaket infpaket)
	{
		Infopaket retPaket = infpaket;
		if (HasNodeTypeFertigkeit<Infopaket> (infpaket)) {
			if (!midgardCharacterChecker.CheckFertigkeit<Infopaket> (infpaket)) {
				List<Infopaket> infopakete = null;
				if(StartNode.typeNodeElement == typeof(Option)){
					infopakete = (StartNode.nodeElement as Option).infopakete;
				} else if(StartNode.typeNodeElement== typeof(Mission)){
					infopakete = (StartNode.nodeElement as Mission).infopakete;
				}
				infopakete.Remove (infpaket);
				if (infopakete != null && infopakete.Count > 0) {
					retPaket = infopakete [0];
				} else {
					retPaket = null;
				}
			}
		}
		return retPaket;
	}

	private void MoveNextForMission(){
		//TODO Implementation MissionCurator
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
