using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatforms : MonoBehaviour
{
    Rigidbody2D _myRigidbody;
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myRigidbody.gravityScale = 0;
    }
    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            _myRigidbody.gravityScale = 4;
        }

        if(obj.gameObject.tag == "Spikes")
        {
            Destroy(gameObject);
        }
    }
}
