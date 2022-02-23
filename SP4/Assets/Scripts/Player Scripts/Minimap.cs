using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Minimap : MonoBehaviour
{
    public Transform player;
 
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        if (SceneManager.GetActiveScene().name == "Level2")

        {
            newPosition.y = player.position.y + 5;
        }
        else if (SceneManager.GetActiveScene().name == "Level1")
        {
            newPosition.y = player.position.y + 3;
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            newPosition.y = player.position.y + 3;
        }
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
