using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI Interfaces from Pause")]
    public GameObject PauseOptionsUI;
    public GameObject PauseUI;
    public GameObject GOmenuUI;
    public GameObject GOoptionsUI;
    public GameObject MainMenuUI;
    public GameObject MainOptionsUI;

  public void BackToPause()
    {
        PauseOptionsUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    public void BackToGO()
    {
        GOoptionsUI.SetActive(false);
        GOmenuUI.SetActive(true);
    }

    public void BackToMain()
    {
        MainOptionsUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    //volume control

}
