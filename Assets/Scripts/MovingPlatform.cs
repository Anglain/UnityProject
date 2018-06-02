using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	//class MovingPlatform
	public Transform pointBTransform;
	public Vector3 MoveBy = Vector3.right;

	public float pointAStandTime = 2f;
	public float pointBStandTime = 2f;

	[Range(0f,25f)]
	public float speed = 3f;

	private Vector3 pointA;
	private Vector3 pointB;

	private bool going_to_A = false;

	//Being tested
	private Vector3 moveVector;

	void Start ()
	{
		this.pointA = this.transform.position;
		// this.pointB = this.pointBTransform.position;
		this.pointB = this.pointA + MoveBy;

		this.moveVector = (this.pointB - this.pointA)  * speed * 0.004f;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector3 my_pos = this.transform.position;
		Vector3 target = (going_to_A) ? this.pointA : this.pointB;

		Vector3 destination = my_pos + moveVector;

		destination.z = 0;
		Debug.Log(destination);

		this.transform.position = destination;

		if (IsArrived(my_pos, target))
		{
			//Debug.Log((going_to_A) ? "pointAStandTime" : "pointBStandTime");
			this.going_to_A = !this.going_to_A;
			moveVector *= -1;
		}
	}

	bool IsArrived(Vector3 pos, Vector3 target)
	{
		pos.z = 0;
		target.z = 0;
		return (Vector3.Distance(pos, target) < 0.2f);
	}
}
