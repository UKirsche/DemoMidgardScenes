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
	/// Determines whether this instance is PC in colliders.
	/// </summary>
	/// <returns><c>true</c> if this instance is PC in colliders; otherwise, <c>false</c>.</returns>
	[Task]
	public bool IsPCInColliders()
	{
		bool retVal = false;
		pcTalkPartners.Clear ();

		foreach (var collider in vision.colliders) {
			var attachedGameObject = collider.attachedRigidbody != null ? collider.attachedRigidbody.gameObject: null;
			if (attachedGameObject != null && attachedGameObject.tag.Equals(DemoRPGMovement.PLAYER_NAME)) {
				pcTalkPartners.Add (attachedGameObject);
				retVal = true;

			}
		}

		return retVal;
	}

	/// <summary>
	/// ISPCs the in talk dist.
	/// </summary>
	/// <returns><c>true</c>, if in talk dist was ISPCed, <c>false</c> otherwise.</returns>
	[Task]
	public bool ISPCInTalkDist()
	{
		bool retVal = false;
		float nearestTalkDistance = reachedMinDistance;

		if (pcTalkPartners.Count > 0) {
			foreach (var pc in pcTalkPartners) {
				float distToPC = Vector3.Distance (transform.position, pc.transform.position);
				if (distToPC <= nearestTalkDistance) {
					nearestTalkDistance = distToPC;
					pcTalkChosen = pc;
					retVal = true;
				} 
			}
		}

		return retVal;
	}


	[Task]
	public bool RotateToPC()
	{
		transform.LookAt(pcTalkChosen.transform.position);
		return true;
	}

	/// <summary>
	/// Sets the dialog partner for the hit PC (Player) as this NPC!
	/// </summary>
	/// <returns><c>true</c>, if dialog partner P was set, <c>false</c> otherwise.</returns>
	[Task]
	public bool SetDialogPartnerPC()
	{
		var dialogManager = pcTalkChosen.GetComponent<PlayerDialogManager> ();
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
		if (pcTalkChosen != null) {
			var dialogManager = pcTalkChosen.GetComponent<PlayerDialogManager> ();
			if (dialogManager != null) {
				dialogManager.SetDialogPartner (null);
				pcTalkChosen = null;
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
