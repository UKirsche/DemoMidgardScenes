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

	const string CHECK_PW = "PW";
	const string CHECK_EW = "EW";


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
	/// Checks the item fertigkeit.
	/// </summary>
	public  bool CheckFertigkeit<T>(T startNodeElement) where T:IFertigkeitsCheck {
		bool retVal=false;
		if (startNodeElement.id > 0) {
			InventoryItem itemFertigkeitCharacter = GetFertigkeitFromCharacter (startNodeElement.id);
			retVal = MakeSuccessRole (itemFertigkeitCharacter, startNodeElement.modifier, startNodeElement.checkType);
		}
		return retVal;
	}


	/// <summary>
	/// Determines whether this instance has fertigkeit the specified in idString.
	/// </summary>
	/// <returns><c>true</c> if this instance has fertigkeit the specified idString; otherwise, <c>false</c>.</returns>
	/// <param name="idString">Identifier string.</param>
	private InventoryItem GetFertigkeitFromCharacter(int id){
		InventoryItem returnItem = null;
		foreach (var item in MChar.fertigkeiten) {
			if (item.id == id) {
				return item;
			}
		}
		return returnItem;
	}


	/// <summary>
	/// Checks the item fertigkeit.
	/// </summary>
	public  bool CheckFertigkeit<T>(T startNodeElement) where T:IFertigkeitsCheck {
		bool retVal=false;
		if (startNodeElement.id > 0) {
			InventoryItem itemFertigkeitCharacter = GetFertigkeit (startNodeElement.id);
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
	private bool MakeSuccessRole(InventoryItem fertigkeitCharacter, int modifier, string checkType){
		bool retVal = false;
		if (fertigkeitCharacter != null) {
			if (checkType.Equals (CHECK_EW)) {
				retVal = RollEW (fertigkeitCharacter, modifier);
			}
			else if (checkType.Equals (CHECK_PW)) {
				retVal = RollPW (fertigkeitCharacter, modifier);
			}

			return retVal;
		}

		return false;
	}


	/// <summary>
	/// Checks the fertigkeit.
	/// </summary>
	/// <returns><c>true</c>, if fertigkeit was checked, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	/// <param name="modifierString">Modifier string.</param>
	private bool RollEW(InventoryItem item, int modifier){
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
	private bool RollPW(InventoryItem item, int modifier){
		int diceRoll = UnityEngine.Random.Range (1, SUCCESS_VAL100+1);
		int diceRollModified = diceRoll + modifier;
		if (diceRollModified<= Convert.ToInt32(item.val)) {
			return true;
		}
		return false;
	}
}
