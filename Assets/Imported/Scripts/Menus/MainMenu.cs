using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Configuration")]
    public GameObject MMUI;
    public GameObject OptUI;

    private void Start()
    {
        MMUI.SetActive(true);
        Time.timeScale = 0;
    }

    #region MAINMENU
    public void PlayGame()
    {
        MMUI.SetActive(false);
        Time.timeScale = 1;

        //any transitions/cinematics?
    }

    public void QuitGame()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }
    #endregion MAINMENU

  #region OPTIONS
    public void Options()
    {
        MMUI.SetActive(false);
        OptUI.SetActive(true);
    }
    public void BackFromOptions()
    {
        MMUI.SetActive(true);
        OptUI.SetActive(false);
    }

    //volumecontrol - music not implemented. 

    #endregion OPTIONS
}

