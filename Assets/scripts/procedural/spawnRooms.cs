using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRooms : MonoBehaviour
{
    public LayerMask WhatIsRoom;
    public lvlGeneration levelGen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, WhatIsRoom);
        if (roomDetection != null && levelGen.stopGeneration == true)
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
