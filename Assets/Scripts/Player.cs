using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float jumpForce = 600f;
	public float initialJumpVelocity = 0.4f;
	public float jumpDampening = 0.02f;
	public float gravityDampeningFactor = 4;
	public float finalGravityFactor = 2.5f;


	private Rigidbody2D rb2D;
	private float initialGravity;
	private float jumpVelocity;
	private float inverseMoveTime;


	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		initialGravity = rb2D.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			dampeningJump();
//			simpleJump();
		}
	}

	void FixedUpdate() {
		calculateDampeningJump();
	}

	private void calculateDampeningJump() {
		Vector3 pos = transform.position;
		if (jumpVelocity != 0) {
			pos.y += jumpVelocity;
			jumpVelocity -= jumpDampening;
			if (jumpVelocity <= 0) {
				rb2D.gravityScale = initialGravity / gravityDampeningFactor;
				jumpVelocity = 0;
			}
		} else if (rb2D.velocity.y < 0 && rb2D.gravityScale <= initialGravity * finalGravityFactor) {
			rb2D.gravityScale += initialGravity / gravityDampeningFactor;
		}

		transform.position = pos;
	}

	private void dampeningJump() {
		jumpVelocity = initialJumpVelocity;
		rb2D.gravityScale = 0;
	}

	private void simpleJump() {
		rb2D.AddForce(Vector2.up * jumpForce);
	}


	private IEnumerator DoJump() {
		Vector3 endPosition = transform.position + Vector3.up;
		float remainingDistance = endPosition.y - transform.position.y;

		while (remainingDistance > float.Epsilon) {
			transform.position = Vector3.Lerp(transform.position, endPosition, 0.2f);
			remainingDistance = endPosition.y - transform.position.y;
			yield return null;
		}
	}

	private IEnumerator DoSmoothJump() {
		Vector3 endPosition = Vector3.up;
		float sqrRemainingDistance = (transform.position - endPosition).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards(rb2D.position, endPosition, inverseMoveTime * Time.deltaTime);
			rb2D.MovePosition(newPosition);
			sqrRemainingDistance = (transform.position - endPosition).sqrMagnitude;
			yield return null;
		}

	}
}
