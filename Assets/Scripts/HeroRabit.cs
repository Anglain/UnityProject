using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
	public float speed = 3f;
	public float jumpForce = 5f;
	public Transform deathPoint;
	public LayerMask whatIsGround;
	public Transform ground;

	private float movingAxisValue;
	private bool isGrounded = true;
	private Rigidbody2D rb2D;
	private SpriteRenderer sr;
	private Animator anim;
	private Transform heroParent;

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
		if (ground == null) Debug.LogError("No ground assigned to player! [HERO_RABIT.CS]");

		this.heroParent = this.transform.parent;
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
			sr.flipX = (movingAxisValue < 0);
		}

		if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
		{
			rb2D.AddForce(Vector2.up * jumpForce * 100f);
			isGrounded = false;
		}

		if (!isGrounded) isGrounded = Mathf.Abs(rb2D.velocity.y) < 0.1f;

		anim.SetBool("isMoving", (Mathf.Abs(rb2D.velocity.x) > 0.1f));
		anim.SetBool("isGrounded", isGrounded);

		RaycastHit2D hit = Physics2D.Linecast(transform.position, ground.position, whatIsGround);

		if(hit) {
			if(hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
			{
				SetNewParent(this.transform, hit.transform);
			}
		}
		else
		{
			SetNewParent(this.transform, this.heroParent);
		}

		if (transform.position.y < deathPoint.position.y) anim.SetBool("isDead", true);
	}

	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos;
		}
	}
}
