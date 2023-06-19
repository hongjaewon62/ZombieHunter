using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<GameManager>();
            }

            return myInstance;
        }
    }

    private static GameManager myInstance;

    public bool isGameover { get; private set; }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void EndGame()
    {
        isGameover = true;
        float bestTime = PlayerPrefs.GetFloat("BestTime");
        if(UiManager.instance.tempSurviveTime > bestTime)
        {
            bestTime = UiManager.instance.tempSurviveTime;
            print(bestTime);
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        UiManager.instance.BestTime(bestTime);
        UiManager.instance.MainUI(true);
    }

    public void Restart()
    {
        isGameover = false;
        UiManager.instance.GameRestart();
    }
}
