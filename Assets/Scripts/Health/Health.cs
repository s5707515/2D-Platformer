using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    public float currentHealth { get; private set; }


    private Animator anim;

    private bool dead; //make sure die animation doesn't play twice


    [Header("iFrames")]

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void TakeDamage(float _damage) //Afflicts damage to a given player
    {

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        if (currentHealth > 0)
        {
            //player hurt

            anim.SetTrigger("hurt");

            //insert iframes here later

            StartCoroutine(Invunerability());
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


    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true); // layer 10 is player, layer 11 is enemy

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0,0.8f);

            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));

            spriteRenderer.color = Color.white;

            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}


