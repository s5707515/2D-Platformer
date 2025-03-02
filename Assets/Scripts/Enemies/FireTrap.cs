using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    [SerializeField] private float damage;


    [Header("Firetrap Timers")]

    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool triggered; //when the trag has been triggered
    private bool active; //when the trap is active and so can hurt the player

    private Health player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!triggered)
            {
                //active fire trap

                StartCoroutine(ActivateFireTrap());
                
            }

            player = collision.GetComponent<Health>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }


    private void Update()
    {
        if(active && player != null)
        {
            player.TakeDamage(damage);
            player = null;
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        //Turn Sprite Red to signify to the player the trap has been triggered
        triggered = true;
        spriteRenderer.color = Color.red;


        //Wait for activation delay, activate trap, turn on fire animation, revert color back to normal
        yield return new WaitForSeconds(activationDelay);

        spriteRenderer.color = Color.white;
        active = true;
        anim.SetBool("activated",true);


        //Wait until X seconds before deactivating trap and return animation to idle

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);

    }

}
