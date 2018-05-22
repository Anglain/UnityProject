using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
	public float speed = 3f;
	public float jumpForce = 5f;
	public Transform deathPoint = null;

	private float movingAxisValue;
	private bool isGrounded = true;
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
			rb2D.velocity = new Vector2(movingAxisValue * speed * Time.deltaTime * 100f, rb2D.velocity.y);
		}

		if (movingAxisValue != 0)
		{
			sr.flipX = (movingAxisValue < 0 ? true : false);
		}

		if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
		{
			rb2D.AddForce(Vector2.up * jumpForce * 100f);
			isGrounded = false;
			Debug.Log("Adding force!");
		}

		if (!isGrounded) isGrounded = Mathf.Abs(rb2D.velocity.y) < 0.1f;

		anim.SetBool("isMoving", (Mathf.Abs(rb2D.velocity.x) > 0.1f));
		anim.SetBool("isGrounded", isGrounded);

		if (transform.position.y < deathPoint.position.y) anim.SetBool("isDead", true);
	}
}
