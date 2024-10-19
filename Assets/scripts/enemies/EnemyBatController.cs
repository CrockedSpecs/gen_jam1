using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Declarations
    [SerializeField] private float spped;
    private Rigidbody2D rigidbody2;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        rigidbody2.AddForce((player.transform.position - transform.position).normalized * spped);
    }
}
