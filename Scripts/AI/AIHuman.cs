using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Panda;

public class AIHuman : AINPC {


	// Use Initialization from BaseClass
	public override void Start () {
		base.Start ();
	}


	#region PC Talk - Reacts to Player

	/// <summary>
	/// Sets the dialog partner for the hit PC (Player) as this NPC!
	/// </summary>
	/// <returns><c>true</c>, if dialog partner P was set, <c>false</c> otherwise.</returns>
	[Task]
	public bool SetDialogPartnerPC()
	{
		var dialogManager = commPartnerChosen.GetComponent<PlayerDialogManager> ();	
		if (dialogManager != null) {
			dialogManager.SetDialogPartner (gameObject);
		} 
		return true;
	}

	/// <summary>
	/// Remoes this NPC as TalkPartner for PC
	/// </summary>
	/// <returns><c>true</c>, if dialog partner P was set, <c>false</c> otherwise.</returns>
	[Task]
	public bool UnsetDialogPartnerPC()
	{
		if (commPartnerChosen != null) {
			var dialogManager = commPartnerChosen.GetComponent<PlayerDialogManager> ();
			if (dialogManager != null) {
				dialogManager.SetDialogPartner (null);
				commPartnerChosen = null;
			} 
		}

		return true;
	}

	#endregion

	#region Helpers
	/// <summary>
	/// Approachs the destination of gameobject
	/// </summary>
	/// <param name="go">Go.</param>
	private void ApproachDestination(GameObject go){
		this.MoveToDestination (go.transform.position);
	}

	#endregion


}
