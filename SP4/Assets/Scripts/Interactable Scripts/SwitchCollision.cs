using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCollision : MonoBehaviour
{
    public GameObject displayText;

    public GameObject switchStatusText;
    bool switchStatus;

    bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        switchStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (switchStatus)
        {
            switchStatusText.GetComponent<TextMesh>().text = "ON";
        }
        else
        {
            switchStatusText.GetComponent<TextMesh>().text = "OFF";
        }

        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switchStatus = !switchStatus;
            }
        }    

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            inRange = true;
            displayText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        displayText.SetActive(false);
    }

    public bool GetSwitchStatus()
    {
        return switchStatus;
    }
}
