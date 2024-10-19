using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Declarations
    private bool isPaused;

    [SerializeField] GameObject gameOver;

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
        gameOver = GameObject.FindGameObjectWithTag("UILevel").transform.GetChild(3).gameObject;
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
}
