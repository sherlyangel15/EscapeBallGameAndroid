using UnityEngine;
using UnityEngine.Animations;
public class MoveObject : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float startX = 0f;
    [SerializeField] float endX = 0f;
    [SerializeField] float startY = 0f;
    [SerializeField] float endY = 0f;

    bool moveHorizontally;
    bool moveVertically;

    void Start()
    {
        moveHorizontally = startX != 0 && endX != 0 && startY == 0 && endY == 0;
        moveVertically = startY != 0 && endY != 0 && startX == 0 && endX == 0;

    }
    void Update()
    {
        if(moveHorizontally)
        {
            float x = Mathf.PingPong(Time.time * speed, endX - startX) + startX;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        if(moveVertically)
        {
            float y = Mathf.PingPong(Time.time * speed, endY - startY) + startY;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

    }
}