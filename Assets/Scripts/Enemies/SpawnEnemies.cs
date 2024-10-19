using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject bat;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnBat");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnBat()
    {
        yield return new WaitForSeconds(1);
        Instantiate(bat, new Vector3(24, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(10);
        Instantiate(bat, new Vector3(-24, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(10);
        Instantiate(bat, new Vector3(0, 24, 0), Quaternion.identity);
        yield return new WaitForSeconds(10);
        Instantiate(bat, new Vector3(0, -24, 0), Quaternion.identity);
        yield return new WaitForSeconds(10);
        Instantiate(bat, new Vector3(24, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(10);
        Instantiate(bat, new Vector3(-24, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(10);
        Instantiate(bat, new Vector3(0, 24, 0), Quaternion.identity);
        yield return new WaitForSeconds(10);
        Instantiate(bat, new Vector3(0, -24, 0), Quaternion.identity);
    }
}
