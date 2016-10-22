using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //PRIVATE VARIABLES
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private GameObject _camera; 
    private GameObject _spawnPoint; 
    private GameObject _gameControllerObject;
    private GameController _gameController;

    private float _move;
    private float _jump;
    private bool _isFacingRight;
    private bool _isGrounded;


    //PUBLIC VARIABLES
    public float Velocity = 10f;
    public float JumpForce = 100f;

    // Use this for initialization
    void Start () {
       this._initalize();
	}
	
	// Update is called once per frame
	void Update () {
        if (this._isGrounded)
        {
            this._move = Input.GetAxis("Horizontal");
            if (this._move > 0f)
            {
                this._move = 1;
                this._isFacingRight = true;
                this._flip();
            }
            else if (this._move < 0f)
            {
                this._move = -1;
                this._isFacingRight = false;
                this._flip();
            }
            else
            {
                this._move = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this._jump = 1f;
            }

            this._rigidbody.AddForce(new Vector2(
                this._move * this.Velocity,
                this._jump * this.JumpForce),
                ForceMode2D.Force);
        }
        else
        {
			this._move = 0f;
			this._jump = 0f;
		}

        this._camera.transform.position = new Vector3(
            this._transform.position.x,
            this._transform.position.y, -10f);
    }

    private void _initalize()
    {
        this._transform = GetComponent<Transform>();
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._camera = GameObject.FindWithTag("MainCamera");
        this._spawnPoint = GameObject.FindWithTag("SpawnPoint");

        this._gameControllerObject = GameObject.Find("Game Controller");
        this._gameController = this._gameControllerObject.GetComponent<GameController>() as GameController;

        this._move = 0f;
        this._isFacingRight = true;
        this._isGrounded = false;
    }

    private void _flip()
    {
        if (this._isFacingRight)
        {
            this._transform.localScale = new Vector2(3f, 3f);
        }
        else
        {
            this._transform.localScale = new Vector2(-3f, 3f);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeathPlane"))
        {
            this._transform.position = this._spawnPoint.transform.position;
            this._gameController.LivesValue -= 1;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            this._transform.position = this._spawnPoint.transform.position;
            this._gameController.LivesValue -= 1;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        this._isGrounded = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            this._gameController.ScoreValue += 100;
        }
    }



}
