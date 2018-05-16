using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateVerticalToggle : PopulateVertical {

	private ToggleGroup toggleGroup;
	private List<Toggle> toggleButtons;

	// Use Initialization from BaseClass
	public override void Awake () {
		base.Awake ();
		toggleGroup = GetComponent<ToggleGroup> ();

	}



}
