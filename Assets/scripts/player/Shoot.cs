using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Range(0f, 100f)]
    public float speed;
    PlayerMovement playerMovement;
    //bool centinelaFlip = false;
    private int direction;

    [SerializeField] private AudioClip collisonAudioClip;
    [SerializeField] private int usefulLife;

    void Start()
    {
        /*
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if(playerMovement.facingRight)
        {
            direction = 1;
        }
        else if(!playerMovement.facingRight)
        {
            direction = 1;    
        }
        */

        direction = 1;
        usefulLife = 3;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);
        if(usefulLife <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(collision.gameObject);
            AudioManager.instance.PlaySFX(collisonAudioClip);
            usefulLife--;
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
