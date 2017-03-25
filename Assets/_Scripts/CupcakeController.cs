using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CupcakeController : MonoBehaviour {
	public float playerNo = 1;
	public float speed = 1f;
	public float frictionConstant = .5f;

	private Vector2 _velocity;
	private float protectionTime;
	private string hAxisName, vAxisName;
	private Rigidbody2D _rigidbody;

	void Start()
	{
		hAxisName = "Horizontal" + playerNo;
		vAxisName = "Vertical" + playerNo;
		_rigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update () {

		if (protectionTime > 0)
			protectionTime -= Time.fixedDeltaTime;

		// Character Controls
		Vector2 _direction = GetJoystickValue ();
		_velocity = _direction * speed;

		if (_direction.x == 0f) {
			_velocity.x *= 0.7f;
		}
		if (_direction.y == 0f) {
			_velocity.y *= 0.7f;
		}

	}

	void FixedUpdate() {
		_rigidbody.velocity = _velocity; // Controlling Cupcake with stick
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (protectionTime > 0) {
			// Debug.Log ("Immune");
			GetComponentInChildren<SpriteRenderer> ().color = Color.red;
		}
		else if (other.transform.tag == "CUPCAKE") {
			//protectionTime = 0.5f; // Protect character for continuous collisions
			calculateDamage(other);
		}
		else
		{
			GetComponentInChildren<SpriteRenderer> ().color = Color.white; // Return normal color after protection has gone
		}
	}

	// Returns positive value on damage
	private float calculateDamage (Collision2D other)
	{
		Debug.DrawRay (other.contacts [0].point, other.contacts [0].normal, Color.red);

		Rigidbody2D contactRigidbody = other.gameObject.GetComponent<Rigidbody2D> ();
		/*
		var impactVelocityX = _rigidbody.velocity.x - contactRigidbody.velocity.x;
		impactVelocityX *= Mathf.Sign(impactVelocityX);
		var impactVelocityY = _rigidbody.velocity.y - contactRigidbody.velocity.y;
		impactVelocityY *= Mathf.Sign(impactVelocityY);
		var impactVelocity = impactVelocityX + impactVelocityY;
		var impactForce = impactVelocity * _rigidbody.mass * contactRigidbody.mass;
		impactForce *= Mathf.Sign(impactForce);
		*/

		float damage = 0f;
		Vector3 enemyProjectionVector = Vector3.Project (contactRigidbody.velocity, other.contacts [0].normal);
		Vector3 ourProjectionVector = Vector3.Project (_rigidbody.velocity, other.contacts [0].normal);
		damage = enemyProjectionVector.magnitude - ourProjectionVector.magnitude;
		if (enemyProjectionVector.magnitude > ourProjectionVector.magnitude) {
			Debug.Log ("Enemy Vector: " + enemyProjectionVector.magnitude + " Our Vector: " + ourProjectionVector.magnitude);
			Debug.Log (this.gameObject.name + " received " + damage);
			protectionTime = 0.5f;
		}
		return damage;
	}


	#region ControlFunctions
	private Vector2 GetJoystickValue() {
		return new Vector2 (GetHorizontalValue(), GetVerticalValue());
	}

	private float GetVerticalValue() {
		float r = 0f;
		r += Input.GetAxis (vAxisName);
		return Mathf.Clamp (r, -1.0f, 1.0f);
	}

	private float GetHorizontalValue() {
		float r = 0f;
		r += Input.GetAxis (hAxisName);
		return Mathf.Clamp (r, -1.0f, 1.0f);
	}
	#endregion
}
