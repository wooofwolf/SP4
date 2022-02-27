using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public int nextScene;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            switch (nextScene)
            {
                case 0:
                    SceneManager.LoadSceneAsync("TutorialScene");
                    break;
                case 1:
                    SceneManager.LoadSceneAsync("Level1");
                    break;
                case 2:
                    SceneManager.LoadSceneAsync("Level2");
                    break;
                case 3:
                    SceneManager.LoadSceneAsync("Level3");
                    break;
                case 4:
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    SceneManager.LoadSceneAsync("GameEnd");
                    break;
                default:
                    Debug.LogError("Cannot load next scene.");
                    break;
            }
        }
    }
}
