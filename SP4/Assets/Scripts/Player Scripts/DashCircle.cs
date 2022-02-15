using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DashCircle : MonoBehaviour
{
    public Image fill;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        CDChange(player.GetComponent<PlayerMovement>().dashCDUI);
    }

    void CDChange(float cd)
    {
        fill.fillAmount = cd; 
    }
}
