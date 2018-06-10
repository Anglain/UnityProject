using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Transform pointBTransform;

	[Range(0f,2000f)]
	public float speed = 3f;
	[Range(0f, 10f)]
	public float waitTime;

	private bool going_to_A = false;
	private bool isMoving = true;
	private float actualWaitTime;

	private Vector3 pointA;
	private Vector3 pointB;

	private Vector3 moveVector;

	void Start ()
	{
		if (pointBTransform == null) Debug.LogError("No point B attached to the moving platform! [MOVING_PLATFORM.CS]");

		this.pointA = this.transform.position;
		this.pointB = this.pointBTransform.position;

		this.moveVector = (this.pointB - this.pointA)  * speed * 0.004f;
		this.actualWaitTime = waitTime;
	}

	void FixedUpdate ()
	{
		if (isMoving)
		{
			Vector3 my_pos = this.transform.position;
			Vector3 target = (going_to_A) ? this.pointA : this.pointB;

			Vector3 destination = my_pos + moveVector;
			destination.z = 0;
			//this.transform.position = destination;
			transform.position = Vector3.MoveTowards(transform.position, target, speed * 0.004f);

			if (IsArrived(my_pos, target))
			{
				this.going_to_A = !this.going_to_A;
				moveVector *= -1;
				isMoving = false;
			}
		}
		else
		{
			actualWaitTime -= Time.fixedDeltaTime;
			if (actualWaitTime <= 0)
			{
				isMoving = true;
				actualWaitTime = waitTime;
			}
		}
	}

	bool IsArrived(Vector3 pos, Vector3 target)
	{
		pos.z = 0;
		target.z = 0;
		return (Vector3.Distance(pos, target) < 0.02f);
	}
}
