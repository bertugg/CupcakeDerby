using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CupcakeController : MonoBehaviour {
	public int playerNo = 1;
	public float speed = 5f;
	public float acceleration = 2;
	public float frictionConstant = .5f;
	public int maxHitPoint = 100;
	public int maxStamina = 100;

	[HideInInspector]
	public HPBarController _hpBar;
	[HideInInspector]
	public PlayMusic soundManager;

	private Vector2 _velocity;
	private float protectionTime;
	private string hAxisName, vAxisName;
	private Rigidbody2D _rigidbody;
	private int hitPoint;
	private float stamina;
	private bool boostAvailable;
	private bool isAlive;
	private Animator _animator;

	void Start()
	{
		isAlive = true;
		hitPoint = maxHitPoint;
		stamina = maxStamina;
		hAxisName = "Horizontal" + playerNo;
		vAxisName = "Vertical" + playerNo;
		_rigidbody = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();

	}

	void Update () {
		
		if (!isAlive) { //Player is dead
			_hpBar.UpdateHPBar(0);
			GetComponentInChildren<SpriteRenderer> ().color = Color.white; // Return normal color after protection has gone
		}
		else if (protectionTime > 0) {
			protectionTime -= Time.fixedDeltaTime;
			GetComponentInChildren<SpriteRenderer> ().color = Color.red;
		}
		else
		{
			GetComponentInChildren<SpriteRenderer> ().color = Color.white; // Return normal color after protection has gone
		}

		if (stamina > maxStamina )
			stamina = maxStamina;
		// Character Controls
		Vector2 _direction = GetJoystickValue ();
		_velocity += _direction * acceleration;
		if (Input.GetButton ("Fire" + playerNo) // Boost if fire button pressed
			&& boostAvailable // and boost available
			&& stamina > 0) { // till stamina depleted

			stamina -= 100 * Time.deltaTime;
			_velocity += _direction * acceleration * 2;

		} else {
			stamina += 20 * Time.deltaTime;
			if (stamina > maxStamina / 2) {
				boostAvailable = true;
			} else
				boostAvailable = false;
		}
		_velocity -= _velocity * frictionConstant;
		Vector2.ClampMagnitude (_velocity, speed);
		if (stamina < maxStamina)
			Debug.Log (stamina);
	}

	void FixedUpdate() {
		_rigidbody.velocity = _velocity; // Controlling Cupcake with stick
		_animator.SetFloat("SpeedX",_rigidbody.velocity.x);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (protectionTime <= 0 && other.transform.tag == "CUPCAKE") {
			calculateDamage (other);
		}
	}

	// Returns positive value on damage
	private int calculateDamage (Collision2D other)
	{
		if (other.rigidbody.velocity.magnitude > _rigidbody.velocity.magnitude) {
			int hitDamage = 5 + Mathf.FloorToInt(other.rigidbody.velocity.magnitude - _rigidbody.velocity.magnitude) ; // Adjust damage
			protectionTime = 0.5f;
			hitPoint -= hitDamage;
			Debug.Log(string.Format("{0} received {1} damage! {2} remained.", gameObject.name, hitDamage, hitPoint));
			_hpBar.UpdateHPBar(hitPoint);
			if (hitPoint <= 0)
				isAlive = false;
			soundManager.PlaySelectedSound (SoundType.Hit);
			return hitDamage;
		} else {
			return 0;
		}
		/*
		Vector3 enemyProjectionVector = Vector3.Project (contactRigidbody.velocity, other.contacts [0].normal);
		Vector3 ourProjectionVector = Vector3.Project (_rigidbody.velocity, other.contacts [0].normal);
		damage = enemyProjectionVector.magnitude - ourProjectionVector.magnitude;
		if (enemyProjectionVector.magnitude > ourProjectionVector.magnitude) {
			Debug.Log ("Enemy Vector: " + enemyProjectionVector.magnitude + " Our Vector: " + ourProjectionVector.magnitude);
			Debug.Log (this.gameObject.name + " received " + damage);
			protectionTime = 0.5f;
		}
		*/
	}


	#region ControlFunctions
	private Vector2 GetJoystickValue() {
		if(isAlive)
			return new Vector2 (GetHorizontalValue(), GetVerticalValue());
		return Vector2.zero;
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
