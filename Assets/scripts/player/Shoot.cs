using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Range(0f, 100f)]
    public float speed;
    PlayerMovement playerMovement;
    bool centinelaFlip = false;
    private int direction;
    
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if(playerMovement.facingRight)
        {
            direction = 1;
        }
        else if(!playerMovement.facingRight)
        {
            direction = -1;    
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);
    }

}
