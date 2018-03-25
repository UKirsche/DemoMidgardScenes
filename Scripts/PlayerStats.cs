using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// Player stats: Store data between Sences
/// </summary>
public static class PlayerStats
{
	public enum Scenes{
		Cambryg,
		CaveOsrick,
		CaveGirls,
		Library
	}

	public static readonly Vector3 toCaveOsrick = new Vector3(92,10,170);
	public static readonly Vector3 fromCaveOsrick = new Vector3(29,7,123);
	public static readonly Vector3 startMidgard = new Vector3(29,7,123);


	private static Vector3 beamPosition;
	public static Vector3 BeamPosition 
	{
		get 
		{
			return beamPosition;
		}
		set 
		{
			beamPosition = value;
		}
	}




}
