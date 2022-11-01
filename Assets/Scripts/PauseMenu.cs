using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject Player;
    public GameObject pauseMenu;
    [SerializeField]GameObject ResumeButton;
    [SerializeField]Text menuText, deliveriesText;

    void Start()
    {
        menuText.text = "Game is Paused";
        deliveriesText.text = "";
    }
    
    void Update()
    {
        if (Player.GetComponent<PlayerMovement>().gameOver == true)
        {
            ResumeButton.SetActive(false);
            if (Player.GetComponent<PlayerMovement>().finalDeliveries <= 3)
            {
                menuText.text = "Game Over! Get more than three deliveries next time peon!";
            }
            if (Player.GetComponent<PlayerMovement>().finalDeliveries >= 4)
            {
                menuText.text = "Game Over! Congrats on working above minimum wage!.";
            }
            deliveriesText.text = $"Deliveries: {Player.GetComponent<PlayerMovement>().finalDeliveries}";
        }
        else
        {
            ResumeButton.SetActive(true);
            menuText.text = "Game is Paused";
        }
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Player.GetComponent<PauseControls>().paused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("Level");
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
