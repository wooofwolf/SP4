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
            newPosition.y = player.position.y + 10;
        }
        else
        {

            newPosition.y = player.position.y + 5;
        }
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
