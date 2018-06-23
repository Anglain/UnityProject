using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
	public static LevelController current;

	public Transform spawnPoint;
	public int maxLives = 3;

	public Text coinsText;
	public Text gemsText;
	public Text livesText;
	public Text fruitsText;

	private int maxGems = 3;
	private int _gems;
	private int Gems
	{
		get
		{
			return _gems;
		}
		set
		{
			if (value >= 0 && value <= maxGems) _gems = value;
		}
	}
	private int money;
	private int _lives = 3;
	private int Lives
	{
		get
		{
			return _lives;
		}
		set
		{
			if (value >= 0 && value <= maxLives) _lives = value;
		}
	}
	private int fruits;


	void Awake ()
	{
		if (current == null)
			current = this;

		if (current != this)
			Destroy(this.gameObject);

		if (spawnPoint == null) Debug.LogError("No spawnPoint Transform attached found attached to the level controller! [LEVEL_CONTROLLER.CS]");
	}

	void Update ()
	{
		coinsText.text = "Coins: " + money;
		gemsText.text = "Gems: " + Gems;
		livesText.text = "Lives: " + Lives;
		fruitsText.text = "Fruits: " + fruits;
	}

	public void AddCoins(int coinsToAdd)
	{
		money += coinsToAdd;
	}

	public void AddFruit()
	{
		fruits++;
	}

	public void AddLife()
	{
		Lives++;
	}

	public void RemoveLife()
	{
		Lives--;
	}

	public void AddGem()
	{
		Gems++;
	}
}
