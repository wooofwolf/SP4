using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        switch(scene)
        {
            case 1: 
                SceneManager.LoadScene("TestScene");
                break;
            case 2: 
                SceneManager.LoadScene("MainMenu");
                break;
            case 3: 
                SceneManager.LoadScene("EnemyTestScene");
                break;
            case 4: 
                SceneManager.LoadScene("CollectibleTestScene");
                break;
            case 5:
                SceneManager.LoadScene("Interactables_Scene");
                break;
            case 6:
                ExitGame();
                break;
        }

    }
    public void LoadGame()
    {
        SceneManager.LoadScene("CollectibleTestScene");
    }
    public void ExitGame()
    {
        //Application.Quit();
        SceneManager.LoadScene("MainMenu");
    }
}
