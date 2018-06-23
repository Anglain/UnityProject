using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
	public float slowdown = 1f;
	Vector3 lastPosition;

	void Awake()
	{
		lastPosition = new Vector3(Camera.main.transform.position.x, transform.position.y, transform.position.z);
	}

	void LateUpdate()
	{
		Vector3 new_position = new Vector3(Camera.main.transform.position.x, transform.position.y, transform.position.z);
		Vector3 diff = new_position - lastPosition;
		lastPosition = new_position;
		Vector3 my_pos = this.transform.position;

		my_pos += slowdown * diff;
		this.transform.position = my_pos;
	}
}
