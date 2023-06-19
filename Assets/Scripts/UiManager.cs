using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{
    public static UiManager instance
    {
        get
        {
            if(myInstance == null)
            {
                myInstance = FindObjectOfType<UiManager>();
            }
            return myInstance;
        }
    }

    private static UiManager myInstance;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTime;

    public GameObject mainUI;
    public GameObject healthBar;
    public GameObject player;
    public float surviveTime;
    private float minutes = 0;
    public float tempSurviveTime = 0;
    private void Start()
    {
        surviveTime = 0;
    }

    private void Update()
    {
        if(!GameManager.instance.isGameover)
        {
            Timer();
        }
    }

    public void AmmoText(int currentAmmo, int currentMagazine)
    {
        ammoText.text = currentAmmo + " / " + currentMagazine;
    }

    public void Timer()
    {
        surviveTime += Time.deltaTime;
        tempSurviveTime += Time.deltaTime;
        if(surviveTime > 59)
        {
            minutes++;
            surviveTime = 0;
        }
        timeText.text = (int)minutes + " : " + (int)surviveTime;
        if(surviveTime < 10)
        {
            timeText.text = (int)minutes + " : 0" + (int)surviveTime;
        }
    }

    public void BestTime(float newBestTime)
    {
        bestTime.text = "BEST TIME: " + newBestTime.ToString("f0");
    }

    public void MainUI(bool active)
    {
        mainUI.SetActive(active);
        healthBar.SetActive(!active);
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
