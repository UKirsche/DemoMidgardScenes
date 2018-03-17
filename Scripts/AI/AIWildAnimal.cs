using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;


public class AIWildAnimal : AINPC {


	//AttackDistance: Sobald so nahe, greift Wolf an. Entsprich min Distance
	private float attackDistance;
	//AggressiveDistance: Hier wird der Wolf aggressiv
	private float aggressiveDistance;
	//FlightDistance: Distance bei der ein Wolf sich verdrückt
	private float flightDistance;

	protected List<GameObject> possiblePreyEnemy;

	//FSM-Variables
	[Task]
	public bool IsAttack;
	[Task]
	public bool IsAggressive;
	[Task]
	public bool IsFlight;
	[Task]
	public bool IsEat;


	// Starte NPC Initialisierung
	public override void Start () {
		base.Start ();

		possiblePreyEnemy = new List<GameObject> ();
		CalculateDistances ();
		FSMIntitialization ();

	}

	void CalculateDistances ()
	{
		attackDistance = reachedMinDistance;
		flightDistance = 4 * attackDistance;
		aggressiveDistance = 2 * attackDistance;
	}

	/// <summary>
	/// FSM: Setze Startwerte für die FSM
	/// </summary>
	private void FSMIntitialization ()
	{
		IsAttack = false;
		IsAggressive = false;
		IsFlight = false;
		IsEat = false;
	}


	#region Attak tasks
	/// <summary>
	/// Checks whether pcs oder npcs are in attack distance
	/// </summary>
	/// <returns><c>true</c>, if in attack distance, <c>false</c> otherwise.</returns>
	[Task]
	public bool IsAttackDistance()
	{
		bool retVal = false;
		possiblePreyEnemy.AddRange (pcCommunicationPartners);
		possiblePreyEnemy.AddRange (npcCommunicationPartners);
		retVal =  IsGoInCommunicationDist (possiblePreyEnemy, attackDistance);

	}

	/// <summary>
	/// Sets the attack goal, depending on prey, enemy lists chosen attack partner
	/// </summary>
	/// <returns><c>true</c>, if attack goal was set, <c>false</c> otherwise.</returns>
	[Task]
	public bool ApproachPrey(){
		strollSpeed = approachSpeed;
		ApproachDestination (commPartnerChosen);
		return true;
	}

	/// <summary>
	/// Ises the talk partner reached.
	/// </summary>
	/// <returns><c>true</c>, if talk partner reached was ised, <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool IsPreyReached(){
		bool isReached = true;
		isReached = IsDestinationReached (commPartnerChosen);
		return isReached;
	}

	#endregion

}
