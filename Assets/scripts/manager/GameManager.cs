using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Declarations
    private bool isPaused;

    [SerializeField] GameObject gameOver, life, energy;

    //Create instance
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ChanceScene
    public void ChanceScene(string seneName)
    {
        SceneManager.LoadScene(seneName, LoadSceneMode.Additive);
    }

    //PauseGame
    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    //GameOver
    public void GameOver()
    {
        gameOver = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(3).gameObject;
        gameOver.SetActive(true);
    }

    //ChangeLife
    public void ChangeLife(int index, bool mood)
    {
        if (!mood && index >= 0 && index <= 2)
        {
            life = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(4).gameObject.transform.GetChild(index).gameObject;
            life.SetActive(mood);
        }
        else if (mood && index >= 1 && index <= 3)
        {
            life = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(4).gameObject.transform.GetChild(index-1).gameObject;
            life.SetActive(mood);
        }
    }

    //ChangeEnergy
    public void ChangeEnergy(int index, bool mood)
    {
        if (!mood && index >= 0 && index <= 4)
        {
            energy = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(5).gameObject.transform.GetChild(index).gameObject;
            energy.SetActive(mood);
        }

        else if (mood && index >= 1 && index <= 5)
        {
            energy = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(5).gameObject.transform.GetChild(index - 1).gameObject;
            energy.SetActive(mood);
        }
    }
}