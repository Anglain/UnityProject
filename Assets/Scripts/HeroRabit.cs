using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
	public float speed = 3f;
	public float jumpForce = 5f;
	public Transform deathPoint;
	public AnimationClip deathAnimation;
	public LayerMask whatIsGround;
	public float maxJumpTime = 1f;
	public float jumpSpeed = 4f;

	private bool _isBig = false;
	public bool isBig
	{
		get
		{
			return _isBig;
		}
		set
		{
			if (value) transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
			else transform.localScale = new Vector3(1f, 1f, 1f);
			_isBig = value;
		}
	}

	private float movingAxisValue;
	private float jumpTime = 0f;
	private float currentDeathAnimTime = 0f;
	private bool isGrounded = true;
	private bool isDead = false;
	private bool jumpActive = false;

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
		if (deathAnimation == null) Debug.LogError("No death animation assigned to the player! [HERO_RABIT.CS]");

		this.heroParent = this.transform.parent;
	}

	void FixedUpdate ()
	{

		Vector3 castFrom = transform.position + Vector3.up * 0.3f;
		Vector3 castTo = transform.position + Vector3.down * 0.2f;

		if (!isDead)
		{
			RaycastHit2D hit = Physics2D.Linecast(castFrom, castTo, whatIsGround);
			Debug.DrawLine(castFrom, castTo, Color.red);
			isGrounded = hit;

			Move();
			Jump();
			CheckPlatformCollision(hit);

			anim.SetBool("isMoving", (Mathf.Abs(rb2D.velocity.x) > 0.02f));
			anim.SetBool("isGrounded", isGrounded);
		}
		else
		{
			currentDeathAnimTime -= Time.fixedDeltaTime;

			if (currentDeathAnimTime <= 0)
			{
				isDead = false;
				anim.SetBool("isDead", isDead);
				//transform.position = Vector3.up;
				transform.position = LevelController.current.spawnPoint.position;
			}
		}

		if (transform.position.y < deathPoint.position.y && !isDead)
		{
			RabitDie();
		}
	}

	private void Move()
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
	}

	private void Jump()
	{
		if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
		{
			jumpActive = true;
		}

		if (jumpActive)
		{
			if (Input.GetButton("Jump") || (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
			{
				jumpTime += Time.fixedDeltaTime;
				if (jumpTime < maxJumpTime)
				{
					Vector3 velocity = rb2D.velocity;
					velocity.y = jumpSpeed * (1f - jumpTime / maxJumpTime);
					rb2D.velocity = velocity;
				}
			}
			else
			{
				jumpActive = false;
				jumpTime = 0f;
			}
		}
	}

	private void CheckPlatformCollision(RaycastHit2D hit)
	{
		if(hit) {
			if(hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
			{
				SetNewParent(this.transform, hit.transform);
			}
			else
			{
				SetNewParent(this.transform, this.heroParent);
			}
		}
		else
		{
			SetNewParent(this.transform, this.heroParent);
		}
	}

	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos;
		}
	}

	public void RabitDie()
	{
		isDead = true;
		anim.SetBool("isDead", isDead);
		currentDeathAnimTime = deathAnimation.length;
		LevelController.current.RemoveLife();
		/** TODO do something when lives go zero */
	}
}
