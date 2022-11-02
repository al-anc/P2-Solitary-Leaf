using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]GameObject Player, gameOverFirstButton;
    [SerializeField]Text menuText, deliveriesText;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(gameOverFirstButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerMovement>().gameOver == true)
        {
            if (Player.GetComponent<PlayerMovement>().finalDeliveries <= 4)
            {
                menuText.text = "Game Over! Get more than four deliveries next time!";
            }
            if (Player.GetComponent<PlayerMovement>().finalDeliveries >= 5)
            {
                menuText.text = "Game Over! Congrats on working above minimum wage!.";
            }
            deliveriesText.text = $"Deliveries: {Player.GetComponent<PlayerMovement>().finalDeliveries}";
        }
    }
}
