using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;
using Panda;

[RequireComponent (typeof(ThirdPersonNPCWildAnimal))]
public class AIWildAnimal : AINPC {

	public float attackDistance;
	public float aggressiveDistance;
	public float flightDistance;

	private float standardSpeed;


	// Starte NPC Initialisierung
	public override void Start () {
		standardSpeed = strollSpeed;
		character = GetComponent<ThirdPersonNPCWildAnimal> ();
		base.Start ();

		//possiblePreyEnemy = new List<GameObject> ();
		FSMIntitialization ();

	}

	/// <summary>
	/// FSM: Setze Startwerte für die FSM
	/// </summary>
	private void FSMIntitialization ()
	{
	}



	#region Attak tasks
	/// <summary>
	/// Checks whether pcs oder npcs are in attack distance
	/// </summary>
	/// <returns><c>true</c>, if in attack distance, <c>false</c> otherwise.</returns>
	[Task]
	public bool IsAttackDistance()
	{
		return IsGoInCommunicationDist (pcCommunicationPartners, attackDistance);

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

	/// <summary>
	/// Ises the talk partner reached.
	/// </summary>
	/// <returns><c>true</c>, if talk partner reached was ised, <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool StartAttack(){
		ThirdPersonNPCWildAnimal meThird = (ThirdPersonNPCWildAnimal)character;
		meThird.Attack ();
		return true;
	}

	/// <summary>
	/// Ises the talk partner reached.
	/// </summary>
	/// <returns><c>true</c>, if talk partner reached was ised, <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool StopAttack(){
		ThirdPersonNPCWildAnimal meThird = (ThirdPersonNPCWildAnimal)character;
		meThird.StopAttack ();
		return true;
	}

	#endregion


	#region aggressive
	/// <summary>
	/// Checks whether pcs oder npcs are in attack distance
	/// </summary>
	/// <returns><c>true</c>, if in attack distance, <c>false</c> otherwise.</returns>
	[Task]
	public bool IsAggressiveDistance()
	{
		return IsGoInCommunicationDist (pcCommunicationPartners, aggressiveDistance);
	}


	/// <summary>
	/// Ises the talk partner reached.
	/// </summary>
	/// <returns><c>true</c>, if talk partner reached was ised, <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool StartAggressive(){
		ThirdPersonNPCWildAnimal meThird = (ThirdPersonNPCWildAnimal)character;
		meThird.Aggression ();
		return true;
	}

	/// <summary>
	/// Ises the talk partner reached.
	/// </summary>
	/// <returns><c>true</c>, if talk partner reached was ised, <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool StopAggressive(){
		ThirdPersonNPCWildAnimal meThird = (ThirdPersonNPCWildAnimal)character;
		meThird.StopAggression ();
		return true;
	}
	#endregion

	#region flight reactions
	/// <summary>
	/// Checks whether pcs are in flight distance
	/// </summary>
	/// <returns><c>true</c>, if in attack distance, <c>false</c> otherwise.</returns>
	[Task]
	public bool IsFlightDistance()
	{
		return IsGoInCommunicationDist (pcCommunicationPartners, flightDistance);
	}

	/// <summary>
	/// Rotates the animal away from possible attacker.
	/// </summary>
	/// <returns><c>true</c>, if away was rotated, <c>false</c> otherwise.</returns>
	[Task]
	public bool RotateAway(){
		Vector3 newForward = (transform.position - commPartnerChosen.transform.position);
		newForward.Normalize ();
		Vector3 lookAtPosition = this.transform.position + newForward;
		transform.LookAt (lookAtPosition);
		return true;
	}

	/// <summary>
	/// Flees from pc. Assumes that forward vector of NPC already looks in opposite direction from Hunter
	/// </summary>
	/// <returns><c>true</c>, if out of fleedistance <c>false</c> otherwise.</returns>
	/// <param name="talkPartnerPosition">Talk partner position.</param>
	[Task]
	public bool Flee(){
		strollSpeed = approachSpeed;
		Vector3 newForward = (transform.position - commPartnerChosen.transform.position);
		newForward.Normalize ();
		Vector3 fleeDestination = transform.position+(newForward*3);
		Debug.DrawLine (transform.position,transform.position+newForward);
		MoveToDestination (fleeDestination);
		return true;
	}
	#endregion

	#region
	[Task]
	public bool WildStroll(){
		strollSpeed = standardSpeed;
		base.Stroll ();
		return true;

	}
	#endregion
}
