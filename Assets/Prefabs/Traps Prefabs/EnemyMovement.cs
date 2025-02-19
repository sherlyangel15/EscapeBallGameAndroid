using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject ObjectToMove;
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] float speed = 5f;

    Vector3 target;
    void Start()
    {
        ObjectToMove.transform.position = pointA.transform.position;
        target = pointB.transform.position;
    }
    void Update()
    {
        ObjectToMove.transform.position = Vector3.MoveTowards(ObjectToMove.transform.position, target, speed *Time.deltaTime);
        if(Vector3.Distance(ObjectToMove.transform.position, target) < 0.01f)
        {
            target = (target == pointA.transform.position) ? pointB.transform.position : pointA.transform.position;
        }
    }
}
