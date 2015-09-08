using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    //returns to the main menu
    public void backToMainMenu()
    {
        GameObject.Find("MainMenu").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("MainMenu").GetComponent<CanvasGroup>().interactable = true;
        GameObject.Find("MainMenu").GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //quits the game
    public void quit()
    {
        Application.Quit();
    }
}
