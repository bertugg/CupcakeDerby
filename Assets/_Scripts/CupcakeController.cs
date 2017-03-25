using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupcakeController : MonoBehaviour {
	public float speed = 1f;
	public float frictionConstant = .5f;
	private Vector2 _velocity;
	void Update () {
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
		GetComponent<Rigidbody2D> ().velocity = _velocity; // Controlling Cupcake with stick
	}
	#region Helper Functions
	private Vector2 GetJoystickValue() {
		return new Vector2 (GetHorizontalValue(), GetVerticalValue());
	}

	private float GetVerticalValue() {
		float r = 0f;
		r += Input.GetAxis ("Vertical");
		return Mathf.Clamp (r, -1.0f, 1.0f);
	}

	private float GetHorizontalValue() {
		float r = 0f;
		r += Input.GetAxis ("Horizontal");
		return Mathf.Clamp (r, -1.0f, 1.0f);
	}
	#endregion
}
