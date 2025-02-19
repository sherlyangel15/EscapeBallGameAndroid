using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumbHeight = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void JumpContinuously()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumbHeight);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            JumpContinuously();
        }
    }
}
