using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
	public int value = 10;

	protected override void OnRabitHit(HeroRabit rabit)
	{
		LevelController.current.AddCoins(value);
		CollectedHide();
	}
}
