using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidInputHandler : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Rigidbody2D rb;

    bool moveLeft;
    bool moveRight;
    bool HorizontalSpeed;

    [SerializeField] PlayerMove playerScript;
    [SerializeField] GameObject Playerface;
    [SerializeField] ParticleSystem frictioneffect;

    public bool GetIsMovingStatus()
    {
        return HorizontalSpeed;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = FindObjectOfType<PlayerMove>();
        // Playerface = playerScript.face;
        Playerface = GameObject.FindWithTag("PlayerFace");

    }
    void Update()
    {
        HorizontalSpeed = moveLeft||moveRight;
        PlayerFlip();
    }

    void FixedUpdate() // Use FixedUpdate for physics-based movement
    {
        Vector2 velocity = Vector2.zero;

        if (moveLeft)
        {
            velocity.x = -speed;
        }
        else if (moveRight)
        {
            velocity.x = speed;
        }

        rb.velocity = new Vector2(velocity.x, rb.velocity.y); // Set horizontal velocity
    }

    public void OnLeftButtonDown()
    {
        moveLeft = true;
    }
    public void OnLeftButtonUp()
    {
        moveLeft = false;
    }

    public void OnRightButtonDown()
    {
        moveRight = true;
    }
    public void OnRightButtonUp()
    {
        moveRight = false;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground")
        {
            frictioneffect.Stop();
        }
    }
    void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "Ground" && !frictioneffect.isPlaying)
        {
            if(HorizontalSpeed)
            {
                frictioneffect.Play();
                PlayerFrictionEffectFlip();
            }
        }
    }
    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.tag == "Ground")
        {
            frictioneffect.Stop();
        }
    }

    void PlayerFlip()
    {
        if(HorizontalSpeed)
        {
            if(moveRight)
            {
                Playerface.transform.position = new Vector2(transform.position.x + 0.15f, transform.position.y);
            } 
            else if(moveLeft)
            {
                Playerface.transform.position = new Vector2(transform.position.x - 0.15f, transform.position.y);
            } 
        }
        else
        {
            Playerface.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }


    void PlayerFrictionEffectFlip()
    {
        if(moveRight)
        {
            frictioneffect.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if(moveLeft)
        {
            frictioneffect.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            return;
        }
    }
}
