using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlGeneration : MonoBehaviour
{

    public Transform[] startingPositions;
    public Transform[] endingPositions;
    public GameObject[] rooms; // index 0 --> closed, index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRBT

    private int direction;
    public bool stopGeneration;

    public float moveIncrement;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public GameObject initialRoom;
    public GameObject finalRoom;

    public LayerMask whatIsRoom;

    public GameObject Player;



    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        int randEndingPos = Random.Range(0, endingPositions.Length);
        transform.position = endingPositions[randEndingPos].position;
        Instantiate(finalRoom, transform.position, Quaternion.identity);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(initialRoom, transform.position, Quaternion.identity);
        Player.transform.position = new Vector2(startingPositions[randStartingPos].transform.position.x, startingPositions[randStartingPos].transform.position.y+0.5f);
        //Instantiate(Player, new Vector2(transform.position.x,transform.position.y+2), Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBtwSpawn <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }

    private void Move()
    {

        if (direction == 1 || direction == 2)
        { // Move right !

            if (transform.position.x < 15)
            {
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(0, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                // Makes sure the level generator doesn't move left !
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        { // Move left !

            if (transform.position.x > -15)
            {
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(0, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }

        }
        else if (direction == 5)
        { // MoveDown
            if (transform.position.y > -15)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = pos;

                // Makes sure the room we drop into has a TOP opening !
                int randRoom = Random.Range(2, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                stopGeneration = true;
            }

        }
    }
}