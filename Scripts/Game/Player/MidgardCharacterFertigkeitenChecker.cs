using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasse führt Checks auf Fertigkeiten aus:
/// </summary>
public class MidgardCharacterFertigkeitenChecker  {

	const int SUCCESS_VAL20=20;
	const int SUCCESS_VAL100=100;

	MidgardCharakter mChar;
	MidgardCharakter MChar { 
		get { 
			return mChar;
		}
		set {
			mChar = value;
		}
	}


	/// <summary>
	/// Initializes a new instance of the <see cref="MidgardCharacterFertigkeitenChecker"/> class.
	/// Hier muss die Referenz zu mChar aufgefüllt werden.
	/// </summary>
	public MidgardCharacterFertigkeitenChecker(){
		//TODO : hier muss Verweis auf das UMARPG-Objekt, bzw. Player erfolgen, der den Charakter ausliest 
	}

	/// <summary>
	/// Determines whether this instance has fertigkeit the specified in idString.
	/// </summary>
	/// <returns><c>true</c> if this instance has fertigkeit the specified idString; otherwise, <c>false</c>.</returns>
	/// <param name="idString">Identifier string.</param>
	public InventoryItem GetFertigkeit(int id){
		InventoryItem returnItem = null;
		foreach (var item in MChar.fertigkeiten) {
			if (item.id == id) {
				return item;
			}
		}
		return returnItem;
	}

	/// <summary>
	/// Checks the fertigkeit.
	/// </summary>
	/// <returns><c>true</c>, if fertigkeit was checked, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	/// <param name="modifierString">Modifier string.</param>
	public bool CheckFertigkeitEW(InventoryItem item, int modifier){
		int diceRoll = UnityEngine.Random.Range (1, SUCCESS_VAL20+1);
		int diceRollModified = diceRoll + Convert.ToInt32(item.val) + modifier;
		if (diceRollModified >= SUCCESS_VAL20) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// Checks the fertigkeit.
	/// </summary>
	/// <returns><c>true</c>, if fertigkeit was checked, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	/// <param name="modifierString">Modifier string.</param>
	public bool CheckFertigkeitPW(InventoryItem item, int modifier){
		int diceRoll = UnityEngine.Random.Range (1, SUCCESS_VAL100+1);
		int diceRollModified = diceRoll + modifier;
		if (diceRollModified<= Convert.ToInt32(item.val)) {
			return true;
		}
		return false;
	}
}
