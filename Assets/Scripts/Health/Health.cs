using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }


    private Animator anim;

    private bool dead; //make sure die animation doesn't play twice

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();    
    }


    public void TakeDamage(float _damage) //Afflicts damage to a given player
    {

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        if (currentHealth > 0)
        {
            //player hurt

            anim.SetTrigger("hurt");

            //insert iframes here later
        }
        else
        {

            if(!dead)
            {

                anim.SetTrigger("die");

                GetComponent<PlayerMovement>().enabled = false;

                dead = true;
            }
            //player dead

        }

    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}


