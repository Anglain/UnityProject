﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable
{
	protected override void OnRabitHit(HeroRabit rabit)
	{
		LevelController.current.AddFruit();
		CollectedHide();
	}
}
