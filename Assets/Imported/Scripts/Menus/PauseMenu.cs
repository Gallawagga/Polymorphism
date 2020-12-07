using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false; //while this is true, the game is paused and the menu is visible
    public GameObject pauseMenuUI;
    public GameObject blackdrop;
    private int sceneNo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //if escape is pushed...
        {
            Pause();
        }
    }

    public void Awake()
    {
        blackdrop.SetActive(true);
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        sceneNo = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public void Resume() //on resuming game.
    {
        blackdrop.SetActive(false);
        pauseMenuUI.SetActive(false);//setting the UI for the menu to be invisible. 
        Time.timeScale = 1f; //rejoining the game with a timescale of 1 on the binary scale of 0-1.
        gameIsPaused = false;
    }

    public void Pause() //when you pause the game by pressing esc.
    {
        pauseMenuUI.SetActive(true);//setting the UI for the menu to be visible. 
        Time.timeScale = 0f; //freezes the time by setting timescale to null/0.
        gameIsPaused = true;
    }

    public void RestartFromPause()
    {
        SceneManager.LoadScene(sceneNo);
    }

    public void QuitGameFromPause()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }

}

