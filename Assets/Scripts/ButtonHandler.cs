using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    bool moveLeft;
    bool moveRight;
    [SerializeField] float speed = 20f;

    void Update()
    {
        if(moveLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if(moveRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
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
}
