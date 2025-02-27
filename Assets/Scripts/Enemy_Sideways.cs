using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if(movingLeft) //If object is moving to the left
        {
            if(transform.position.x > leftEdge) // keep moving left
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else //Change Direction
            {
                movingLeft = false;
            }
        }
        else //If object is moving to the right
        {
            if (transform.position.x < rightEdge) //keep moving right
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else //Change Direction
            {
                movingLeft = true;
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
