using System;
using System.Collections;

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
	private static Scenes lastScene;

	public static Scenes LastScene 
	{
		get 
		{
			return lastScene;
		}
		set 
		{
			lastScene = value;
		}
	}

}
