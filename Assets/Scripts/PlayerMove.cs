using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Vector2 rawValue;

    [SerializeField] Rigidbody2D _myRigidBody;
    [SerializeField] SpriteRenderer _mySpriteRenderer;
    [SerializeField] BoxCollider2D _groundDetector;

    [Header("Player Face")]
    public GameObject face;

    [Header("Effects")]
    [SerializeField] ParticleSystem frictioneffect;

    [Header("Player")]
    PlayerAttackMode attackMode;
    PlayerColor playerColor;

    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpHeight = 15f;
    [SerializeField] bool playerHasHorizontalSpeed;
    public bool isMoving { get; private set; }


    void Start()
    {
        face = GameObject.FindWithTag("PlayerFace");
        face.transform.localScale = Vector3.one;

        _myRigidBody = GetComponent<Rigidbody2D>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _groundDetector = GetComponentInChildren<BoxCollider2D>();

        attackMode = FindObjectOfType<PlayerAttackMode>();
        playerColor = FindObjectOfType<PlayerColor>();
    }
    void Update()
    {
        Move();
        PlayerFlip();
        playerHasHorizontalSpeed = Mathf.Abs(_myRigidBody.velocity.x) > Mathf.Epsilon;  //Not Necessary for Android. 
        isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;  //Not Necessary for Android. 
        _mySpriteRenderer.sprite = playerColor.GetPlayerSprite();
    }

    public void setRawValueZero() //Not Necessary for Android. 
    {
        rawValue = new Vector2(0, 0);
    }

    public bool PlayerIsMovingStatus() => playerHasHorizontalSpeed;
    public void OnMove(InputValue value) => rawValue = value.Get<Vector2>();  //Not Necessary for Android. 
    public void OnJump(InputValue value)  //Not Necessary for Android. 
    {
        if(!_groundDetector.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            return;
        }
        if(value.isPressed)
        {
            Jump();
        }
    }

    public void jumpButton()
    {
        if(!_groundDetector.IsTouchingLayers(LayerMask.GetMask("Ground")))
        { return; }
        
        Jump();
    }
    void Move()  //Not Necessary for Android. 
    {
        _myRigidBody.velocity = new Vector2(rawValue.x * _speed, _myRigidBody.velocity.y);
    }
    public void SetRawValueZero() //Not Necessary for Android. 
    {
        rawValue = Vector2.zero;
    }
    
    void Jump()
    {
        _myRigidBody.velocity = new Vector2(_myRigidBody.velocity.x, _jumpHeight);
    }

    void OnCollisionEnter2D(Collision2D other) {  //Not Necessary for Android. 
        if(other.gameObject.tag == "Ground")
        {
            frictioneffect.Stop();
        }
    }
    void OnCollisionStay2D(Collision2D other) {  //Not Necessary for Android. 
        if(other.gameObject.tag == "Ground" && !frictioneffect.isPlaying)
        {
            if(playerHasHorizontalSpeed)
            {
                frictioneffect.Play();
                PlayerFrictionEffectFlip();
            }
        }
    }
    void OnCollisionExit2D(Collision2D other){ //Not Necessary for Android. 
        if(other.gameObject.tag == "Ground")
        {
            frictioneffect.Stop();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy")
        {
            if(attackMode.GetAttackModeStatus() == true)
            {
                attackMode.StartCorou(other.gameObject);
            }
        }
    }
    void PlayerFlip() //Not Necessary for Android. 
    {
        if(playerHasHorizontalSpeed)
        {
            if(Mathf.Sign(_myRigidBody.velocity.x) > 0)
            {
                face.transform.position = new Vector2(transform.position.x + 0.15f, transform.position.y);
            } 
            else if(Mathf.Sign(_myRigidBody.velocity.x) < 0)
            {
                face.transform.position = new Vector2(transform.position.x - 0.15f, transform.position.y);
            } 
        }
        else
        {
            face.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }

    void PlayerFrictionEffectFlip() //Not Necessary for Android.
    {
        if(Mathf.Sign(_myRigidBody.velocity.x) > 0)
        {
            frictioneffect.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if(Mathf.Sign(_myRigidBody.velocity.x) < 0)
        {
            frictioneffect.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            return;
        }
    }

}
