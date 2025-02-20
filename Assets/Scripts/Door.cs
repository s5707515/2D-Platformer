using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;

    [SerializeField] private Transform nextRoom;

    [SerializeField] private CameraController camControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(collision.transform.position.x < transform.position.x) // if player is coming from the left
            {
                camControl.MoveToNewRoom(nextRoom);
            }
            else
            {
                camControl.MoveToNewRoom(previousRoom);
            }
        }
    }

}
