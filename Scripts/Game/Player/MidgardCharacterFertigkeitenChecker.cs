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
	/// Determines whether this instance has fertigkeit the specified in idString.
	/// </summary>
	/// <returns><c>true</c> if this instance has fertigkeit the specified idString; otherwise, <c>false</c>.</returns>
	/// <param name="idString">Identifier string.</param>
	public InventoryItem HasFertigkeit(string idString){
		InventoryItem returnItem = null;
		int id =  Convert.ToInt32 (idString);
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
	public bool CheckFertigkeitEW(InventoryItem item, string modifierString){
		int modifier = Convert.ToInt32(modifierString);
		int diceRoll = UnityEngine.Random.Range (1, SUCCESS_VAL20+1);
		int diceRollModified = diceRoll + item.val + modifier;
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
	public bool CheckFertigkeitPW(InventoryItem item, string modifierString){
		int modifier = Convert.ToInt32(modifierString);
		int diceRoll = UnityEngine.Random.Range (1, SUCCESS_VAL100+1);
		int diceRollModified = diceRoll + modifier;
		if (diceRollModified<= item.val) {
			return true;
		}
		return false;
	}
}
