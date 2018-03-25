using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// Player stats: Store data between Sences
/// </summary>
public static class PlayerStats
{
	public static readonly Vector3 startMidgard = new Vector3(27,1,183);

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
