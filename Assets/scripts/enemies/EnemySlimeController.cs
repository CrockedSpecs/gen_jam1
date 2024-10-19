using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySlimeController : MonoBehaviour
{
    private Vector3 startingPosision;
    // Start is called before the first frame update
    void Start()
    {
        startingPosision = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Te veo");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Te veo");
        }
    }
}
