using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject controlsPanel;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Controls()
    {
        controlsPanel.SetActive(true);
    }
    public void CloseControls()
    {
        controlsPanel.SetActive(false);
    }
}
