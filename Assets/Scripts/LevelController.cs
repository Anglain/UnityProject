using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController lc;

	private int lives;
	private int gems;
	private int money;

	void Awake() {
		if (lc == null)
			lc = this;

		if (lc != this)
			Destroy(this.gameObject);
	}
}
