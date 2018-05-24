using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{
	private HeroRabit player;

	void Start ()
	{
		player = GameObject.FindWithTag("Player").GetComponent<HeroRabit>();

		if (player == null)	Debug.LogError("No gameObject with the HeroRabit component found in the scene! [HERO_FOLLOW.CS]");
	}

	void Update ()
	{
		Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}
}
