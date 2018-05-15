using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Generic Node Element of Dialog Tree: Rekursive Struktur
/// </summary>
public class Node<T> where T:Node<T>{
	public T parentNode;
}

/// <summary>
/// Konkreter Node mit generic Element
/// </summary>
public class DialogNode<T>:Node<DialogNode<T>>{
	public object nodeElement;
	public Type typeNodeElement;
	public Type typeParentNodeElement;
}

/// <summary>
/// Dialog parser: Holt für den entsprechenden Knoten, die anzuzeigenden Blätter
/// </summary>
public class DialogParser {
	private DialogNode<object> startNode;
	public DialogNode<object> StartNode { 
		get{ return startNode; }
		set{ startNode = value; }
	}

	/// <summary>
	/// Shows whether next Page has Options to choose from
	/// </summary>
	private bool isOption=false;
	public bool IsOption {
		get { return startNode.typeNodeElement == typeof(Optionspaket);}
	}


	/// <summary>
	///  Werden mit relevanter Option von außen bestimmt
	/// </summary>
	public List<DialogNode<object>> optionalStartNodes;

	/// <summary>
	/// Initializes a new instance of the <see cref="DialogParser"/> class.
	/// </summary>
	public DialogParser(){
		startNode = null;
		optionalStartNodes = new List<DialogNode<object>>();
	}

	/// <summary>
	/// Gets the leaves: Holt zu jedem Startknoten die entsprechenden Kinder ab.
	/// Voraussetzung ist immer der aktuelle Startknoten ist gesetzt. 
	/// Mögliche Startknoten sind: Optionspaket, Option, Mission
	/// </summary>
	/// <returns>Infos.</returns>
	public List<Info> GetInfos(){
		List<Info> returnList=null;	
		if(startNode.nodeElement!=null){
			//Hier wird der tmpNode nicht direkt neu gesetzt, sondern eine Liste möglicher tmpNodes ermittelt
			if (startNode.typeNodeElement == typeof(Optionspaket)) {
				SetParentOptionalStartNodes (); //OptionsListe gesetzt, kein Rückgabe

			} else if(startNode.typeNodeElement==typeof(Mission)|| startNode.typeNodeElement==typeof(Option)) { //Falls Startknoten Mission oder Option (einzige weiteren Knoten mit Lauf nach unten
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


	/// <summary>
	/// Sets the type of the next node parent, abhängig vom aktuellen Startknoten
	/// </summary>
	/// <param name="nextNode">Next node.</param>
	private void SetNextNodeParentType(DialogNode<object> nextNode){
		if (startNode.typeNodeElement == typeof(Option)) {
			nextNode.typeParentNodeElement = typeof(Option);
		} else if(startNode.typeNodeElement==typeof(Mission)) {
			nextNode.typeParentNodeElement = typeof(Mission);
		}
	}

	/// <summary>
	/// Legt den Parent für das Infopaket fest. Der kann entweder Mission oder Option sein
	/// </summary>
	/// <param name="infopaket">Infopaket.</param>
	/// <param name="newNode">New node.</param>
	private void SetParent(Infopaket infopaket, DialogNode<object> newNode){
		newNode.nodeElement = infopaket;
		newNode.parentNode = startNode;
	}


	/// <summary>
	/// Gets the next info paket:
	/// Für jeden Startknoten muss das nächste Infopaket geholt werden (bis auf Optionspaket, das Optionen hat)
	/// </summary>
	/// <returns>The next info paket.</returns>
	private Infopaket GetNextInfoPaket(){
		Infopaket returnPaket = null;
		if(startNode.typeNodeElement == typeof(Option)){
			returnPaket = (startNode.nodeElement as Option).infopakete[0];
		} else if(startNode.typeNodeElement== typeof(Mission)){
			returnPaket = (startNode.nodeElement as Mission).infopakete[0];
		}
		return returnPaket;

	}

	/// <summary>
	/// Sets the parent optional start node: 
	/// Methode wird nur bei Optionspaketen gebraucht, welche die optionalen Punkte in einer Liste festlegt
	/// </summary>
	private void SetParentOptionalStartNodes(){
		optionalStartNodes.Clear(); //leere die alte Liste;
		Optionspaket opaket = startNode.nodeElement as Optionspaket;
		List<Option> optionen = opaket.optionen;
		foreach (var option in optionen) {
			DialogNode<object> optionsNode = new DialogNode<object>();
			optionsNode.nodeElement = option;
			optionsNode.typeNodeElement = typeof(Option);
			optionsNode.typeParentNodeElement = typeof(Optionspaket);
			optionsNode.parentNode = startNode;
			optionalStartNodes.Add(optionsNode);
		}
	}

	//Setzt neuen Startpunkt mit Parent. -> Upward-Jumping
	//Beispiele: 
	// * Infopaket enthält weiteres Optionspaket (letzter Leave auf einer Ebene)
	//   ->Optionspaket auch letztes Element im Parent, damit wird parent NICHT das Infopaket darüber, sondern das nächste
	//   ->Infopaket eine Ebene weiter oben.

	/// <summary>
	/// Sets the start node:
	/// Setzt den Startknoten für neue Iteration. Falls kein Unterknoten, springe hoch.
	/// Beispiele MoveUpward:
	/// Infopaket enthält weiteres Optionspaket (letzter Leave auf einer Ebene)
	///  ->Optionspaket auch letztes Element im Parent, damit wird parent NICHT das Infopaket darüber, sondern das nächste
	///  ->Infopaket eine Ebene weiter oben.
	/// </summary>
	/// <param name="newNode">New node.</param>
	private void SetStartNode(DialogNode<object> nextNode){
		Infopaket infopaket = nextNode.nodeElement as Infopaket;
		if(infopaket.optionspaket!=null){//Neuer Startknoten
			DialogNode<object> newStartNode = new DialogNode<object> ();
			startNode = newStartNode;
			startNode.nodeElement = infopaket.optionspaket;
			startNode.typeNodeElement = typeof(Optionspaket);
			startNode.typeParentNodeElement = typeof(Infopaket);
			startNode.parentNode = nextNode;
		} else {
			//Neuer Knoten:moveUpward
			MoveUpward();
		}
	}

	/// <summary>
	/// Moves the tree upward.
	/// </summary>
	private void MoveUpward(){
		DialogNode<object> tmpNodeParent =  startNode.parentNode;
		DialogNode<object> tmpNode = startNode;
		List<Infopaket> infopakete= new List<Infopaket>();

		//Hier refactoring
		if(tmpNode.typeNodeElement==typeof(Infopaket)){
			if (tmpNode.typeParentNodeElement == typeof(Option)) {
				Option option = tmpNodeParent.nodeElement as Option;
				infopakete = option.infopakete;
			}
			else if (tmpNode.typeParentNodeElement==typeof(Mission)){
				Mission mission = tmpNodeParent.nodeElement as Mission;
				infopakete = mission.infopakete;	
			}
			Infopaket infopaket = tmpNode.nodeElement as Infopaket;
			infopakete.Remove(infopaket);

			if (infopakete.Count > 0) {
				startNode = startNode.parentNode;
				return;
			} 
		}

		//Move Node up
		startNode = startNode.parentNode;
		infopakete.Clear ();

		if (startNode != null) {
			if(startNode.typeNodeElement==typeof(Option)){
				Option option = tmpNodeParent.nodeElement as Option;
				infopakete = option.infopakete;
			} else if (startNode.typeNodeElement==typeof(Mission)){
				Mission mission = tmpNodeParent.nodeElement as Mission;
				infopakete = mission.infopakete;	
			}
			if (infopakete.Count > 0) {
				return;
			} else {
				MoveUpward ();
			}
		}

	}

}