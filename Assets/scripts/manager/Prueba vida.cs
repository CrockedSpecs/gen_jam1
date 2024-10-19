using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Pruebavida : MonoBehaviour
{
    public int energy;

    // Start is called before the first frame update
    void Start()
    {
        energy = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            energy --;
            GameManager.instance.ChangeEnergy(energy, false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            energy ++;
            GameManager.instance.ChangeEnergy(energy, true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            energy--;
            GameManager.instance.ChangeLife(energy, false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            energy++;
            GameManager.instance.ChangeLife(energy, true);
        }
    }
}
