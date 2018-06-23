using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current;

	public Transform spawnPoint;

	private int lives;
	private int gems;
	private int money;

	void Awake() {
		if (current == null)
			current = this;

		if (current != this)
			Destroy(this.gameObject);

		if (spawnPoint == null) Debug.LogError("No spawnPoint Transform attached found attached to the level controller! [LEVEL_CONTROLLER.CS]");
	}
}
