using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyGameObject();
    }

    void DestroyGameObject()
    {
        if (transform.position.x > 25)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -25)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y > 25)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < -25)
        {
            Destroy(gameObject);
        }
    }
}
