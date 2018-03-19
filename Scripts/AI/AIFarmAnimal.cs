using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Panda;

[RequireComponent (typeof(ThirdPersonNPC))]
public class AIFarmAnimal : AINPC {

	// Use Initialization from BaseClass
	public override void Start () {
		character = GetComponent<ThirdPersonNPC> ();
		base.Start ();
	}
		
}
