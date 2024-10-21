using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Declarations
    [SerializeField] private float speed;
    private float followSpeed;
    private Rigidbody2D rigidbody2;
    private GameObject player;
    private Vector2 initPos;
    public int randDir;
    public float idleRange;
    public bool findPlayer;

    private float distanceWithPlayer;

    // Start is called before the first frame update
    void Start()
    {
        followSpeed = speed * 1.5f;
        findPlayer = false;
        rigidbody2 = GetComponent<Rigidbody2D>();
        initPos = transform.position;
        randDir = Random.Range(0, 2);
        player = GameObject.Find("Player");
    }

    void Update()
    {
        isPlayerNear();
        if (!findPlayer)
        {
            idleMove();
        }
        else
        {
            attackPlayer();
        }

    }
    void isPlayerNear()
    {
        distanceWithPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        if (distanceWithPlayer <= 4f)
        {
            findPlayer = true;
            initPos = transform.position;
        }
        else
        {
            findPlayer = false;
        }
    }
    void idleMove()
    {
        float currentPosX = transform.position.x;

        if (currentPosX >= initPos.x + idleRange)
        {
            randDir = 1; // Change direction to left
        }
        else if (currentPosX <= initPos.x - idleRange)
        {
            randDir = 0; // Change direction to right
        }

        // Apply movement based on direction
        if (randDir == 0) // Move right
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime,Space.World);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (randDir == 1) // Move left
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime,Space.World);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void attackPlayer()
    {
        transform.position = Vector2.MoveTowards(this.transform.position,player.transform.position,speed*1.5f * Time.deltaTime);
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the collided object has the tag "floor"
        if (other.gameObject.CompareTag("Floor"))
        {
            if (randDir == 1)
            {
                randDir = 0;
            }
            else
            {
                randDir = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            findPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            findPlayer = false;
            initPos = transform.position;
        }
    }
}