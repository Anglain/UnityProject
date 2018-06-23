using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{
	protected override void OnRabitHit(HeroRabit rabit)
	{
		if (rabit.isBig)
		{
			rabit.isBig = false;
			CollectedHide();
		}
		else
		{
			rabit.RabitDie();
			CollectedHide();
		}
	}
}
