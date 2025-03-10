using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//GITHUB CHECK

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    [SerializeField] private float maximumLifetime = 5f;

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(hit) //If fireball hits anything, don't execute the rest
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * direction;

        transform.Translate(movementSpeed, 0, 0);


        lifetime += Time.deltaTime;

        if (lifetime > maximumLifetime)
        {
            gameObject.SetActive(false);    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if(collision.CompareTag("Enemy"))
        {
            //If the enemy has health, damage it (not a trap)
            collision.GetComponent<Health>()?.TakeDamage(1); 
        }
    }

    public void SetDirection(float _direction) //Determines direction to send a fireball in (left or right)
    {
        lifetime = 0;

        direction = _direction;

        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;

        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
