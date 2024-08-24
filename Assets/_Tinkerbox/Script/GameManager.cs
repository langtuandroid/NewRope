using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int TotalLevelCount;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        if (!PlayerPrefs.HasKey("CurrentLevelIndex"))
        {
            PlayerPrefs.SetInt("CurrentLevelIndex",2);
            SceneManager.LoadScene(2);
        }else
        {
            var a =PlayerPrefs.GetInt("CurrentLevelIndex");
            SceneManager.LoadScene(a);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            LevelSucces();
        }
    }

    public void LevelSucces()
    {
        var index = PlayerPrefs.GetInt("CurrentLevelIndex");
        index++;
        if (index > TotalLevelCount) index = 3;
        PlayerPrefs.SetInt("CurrentLevelIndex",index);

        SceneManager.LoadScene(index);
    }

    public void LevelFailed()
    {
        var index = PlayerPrefs.GetInt("CurrentLevelIndex");

        SceneManager.LoadScene(index);
    }
}
