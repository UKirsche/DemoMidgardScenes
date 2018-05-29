using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOptionManager  {


	/// <summary>
	/// Sets the index of the chosen option.
	/// </summary>
	/// <param name="index">Index.</param>
	public static void SetChosenOptionIndex(int index, DialogParser dialogParser){
		if (index >= 0) {
			dialogParser.SetParentOptionalStartNodeIndex (index);
		}
	}

	/// <summary>
	/// Checks whether Dialog isOption
	/// </summary>
	/// <returns><c>true</c>, if dialog option was nexted, <c>false</c> otherwise.</returns>
	public static bool NextDialogOption(DialogParser dialogParser){
		return dialogParser.IsOption;
	}

	/// <summary>
	/// Liefert die Optionen (Auswahlmöglichkeiten) als Liste von strings
	/// </summary>
	/// <returns>The next options.</returns>
	public static List<string> FormatOptions(DialogParser dialogParser){
		List<string> optionStrings = new List<string> ();
		List<DialogNode<object>> optionNodes = dialogParser.optionalStartNodes;
		foreach (var optionNode in optionNodes) {
			Option nodeElement = optionNode.nodeElement as Option;
			optionStrings.Add (nodeElement.Beschreibung);
		}

		return optionStrings;
	}

}
