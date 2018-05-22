using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
	public float speed = 30f;
	public Transform deathPoint = null;

	private float movingAxisValue;
	private Rigidbody2D rb2D;
	private SpriteRenderer sr;
	private Animator anim;

	void Start ()
	{
		rb2D = this.GetComponent<Rigidbody2D>();
		sr = this.GetComponent<SpriteRenderer>();
		anim = this.GetComponent<Animator>();
		deathPoint = GameObject.FindWithTag("DeathPoint").transform;

		if (rb2D == null) Debug.LogError("No Rigidbody2D component found attached to this Player gameObject! [HERO_RABIT.CS]");
		if (sr == null)	Debug.LogError("No SpriteRenderer component found attached to this Player gameObject! [HERO_RABIT.CS]");
		if (anim == null) Debug.LogError("No Animator component found attached to this Player gameObject! [HERO_RABIT.CS]");
		if (deathPoint == null) Debug.LogError("No DeathPoint gameObject found! [HERO_RABIT.CS]");
	}

	void FixedUpdate ()
	{
		movingAxisValue = Input.GetAxis("Horizontal");

		if (Mathf.Abs(movingAxisValue) > 0)
		{
			rb2D.velocity = new Vector2(movingAxisValue * speed * Time.deltaTime * 10f, rb2D.velocity.y);
		}

		if (movingAxisValue != 0)
		{
			sr.flipX = (movingAxisValue < 0 ? true : false);
		}

		anim.SetBool("isMoving", (Mathf.Abs(rb2D.velocity.x) < 0.05f));

		if (transform.position.y < deathPoint.position.y) anim.SetBool("isDead", true);
	}
}
