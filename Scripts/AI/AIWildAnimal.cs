using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;


public class AIWildAnimal : AINPC {


	//FSM-Variables
	[Task]
	public bool IsAttack;
	[Task]
	public bool IsFlight;
	[Task]
	public bool IsEat;

	// Starte NPC Initialisierung
	public override void Start () {
		base.Start ();
		FSMIntitialization ();

	}


	/// <summary>
	/// FSM: Setze Startwerte für die FSM
	/// </summary>
	private void FSMIntitialization ()
	{
		IsAttack = false;
		IsFlight = false;
		IsEat = false;
	}



}
