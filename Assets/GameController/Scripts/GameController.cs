using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject PauseScreen;
    public GameObject DeathScreen;
    public void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (GrowthManager.Instance.m_GrowthLevel <= 0)
        {
            DeathScreen.SetActive(true);
        }
    }
    public void OnQuit()
    {
        SceneManager.LoadScene(0);
    }
    public void OnPlay()
    {
        PauseScreen.SetActive(false);
    }
    public void OnRetry()
    {
        SceneManager.LoadScene(1);
    }
    public void Pause()
    {
        PauseScreen.SetActive(true);
    }
}
