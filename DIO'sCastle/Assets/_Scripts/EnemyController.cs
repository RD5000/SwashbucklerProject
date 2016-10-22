using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	// PRIVATE INSTANCE VARIABLES 
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private bool _isGrounded;
	private bool _isGroundAhead;

	// PUBLIC VARIABLES
	public float Speed = 5f;
	public Transform SightStart;
	public Transform SightEnd;

	// Use this for initialization
	void Start () {
		this._transform = GetComponent<Transform> ();
		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._isGrounded = false;
		this._isGroundAhead = true;
	}
	
	// Update is called once per frame (Physics)
	void FixedUpdate () {
		if (this._isGrounded) {
			this._rigidbody.velocity = new Vector2(this._transform.localScale.x, 0) * this.Speed;

			this._isGroundAhead = Physics2D.Linecast (
				this.SightStart.position,
				this.SightEnd.position,
				1 << LayerMask.NameToLayer ("Solid"));

			if (this._isGroundAhead == false) {
				this._flip();
			}
		}

	}


	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Enemy")) {
			this._flip ();
		}
	}
		

	private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = true;
		}
	}


	private void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Platform")) {
			this._isGrounded = false;
		}
	}


	private void _flip () {
		if (this._transform.localScale.x == 3) {
			this._transform.localScale = new Vector2 (-3f, 3f);
		} else {
			this._transform.localScale = new Vector2 (3f, 3f);
		}
	}
}
