using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Die an die Capsule (Infoobjekt für Spieler) gebundene ColliderInfo ist das Bindegleid zur der statischen DialogView <see cref="dialogView"/>
/// </summary>
[RequireComponent(typeof(CapsuleCollider))]
public class CapsuleColiderInfo : MonoBehaviour {
	public GameObject dialogView;
	private Infopaket dialogPackage;
	private PopulateVertical populateDialog;


	// Use this for initializatio
	void Start () {
		populateDialog = dialogView.GetComponentsInChildren<PopulateVertical> ()[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/// <summary>
	/// Activates the dialog window in GameScene with Standardinfo
	/// </summary>
	/// <param name="activate">If set to <c>true</c> activate.</param>
	public bool SetActiveDialogView(){
		dialogView.SetActive (true);
		var artifactDialogManager = GetComponent<ArtifactDialogManager> ();
		var dialogString = artifactDialogManager.GetNextInfos ();

		if (dialogString == null || dialogString.Count==0) {
			dialogString = artifactDialogManager.GetStandardInfo ();

		}
		DialogDisplayManager.DisplayDialogText (dialogString, populateDialog);

		return true;
	}



    /// <summary>
    /// Raises the trigger enter event. Only valid for Gameobjects with Rigidbody and that include a trigger type.
    /// Ruft <see cref="SetActiveDialogView"/> wenn ein "Player" den Trigger auslöst.
    /// </summary> 
    /// <param name="other">Other.</param>
    void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody.gameObject.tag == "Player") {
			SetActiveDialogView ();
		}
	}

    /// <summary>
    /// Sobald der Trigger-Bereich verlassen wird, wird das Fenster wieder geschlossen.
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerExit(Collider other)
	{
		if (other.attachedRigidbody.gameObject.tag == "Player") {
			dialogView.SetActive (false);	
		}
	}

}
