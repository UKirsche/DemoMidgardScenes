﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class AIFarmAnimal : AINPC {

	// Use Initialization from BaseClass
	public override void Start () {
		base.Start ();
	}

	//Use Standard Stroll from BaseClass
	[Task]
	public void FarmStroll ()
	{
		base.Stroll ();
	}
		
}