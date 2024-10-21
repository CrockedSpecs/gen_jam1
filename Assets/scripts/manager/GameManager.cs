using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Declarations
    private bool isPaused;

    [SerializeField] private GameObject gameOver, life, energy, bomb, gameOverText;
    [SerializeField] private TextMeshProUGUI gameOverTextText;
    [SerializeField] private int levelIndex;

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
        levelIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ChanceScene
    public void ChangeScene(string seneName)
    {
        levelIndex = 1;
        SceneManager.LoadScene(seneName);
    }

    //PauseGame
    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        isPaused = !isPaused;
    }

    //Level++
    public void PassLevel()
    {
        levelIndex ++;
    }

    //GameOver
    public void GameOver()
    {
        gameOver = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(3).gameObject;
        gameOverText = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(3).gameObject.transform.GetChild(1).gameObject;
        gameOverTextText = gameOverText.GetComponent<TextMeshProUGUI>();
        gameOverTextText.text = "You died at\n" + "level " + levelIndex.ToString();
        levelIndex = 1;
        gameOver.SetActive(true);
        AudioManager.instance.GameOver();
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

        else if (mood && index == 0)
        {
            StartCoroutine("GetEnergy");
        }
    }

    //ChangeBombState
    public void ChangeBombState(bool mood)
    {
            bomb = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(6).gameObject;
            bomb.SetActive(mood);
    }

    //ReloadEnergy
    IEnumerator GetEnergy()
    {
        int index = 0;
        while (index < 5)
        {
            yield return new WaitForSeconds(1);
            energy = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(5).gameObject.transform.GetChild(index).gameObject;
            energy.SetActive(true);
            index++;         
        }
    }
}