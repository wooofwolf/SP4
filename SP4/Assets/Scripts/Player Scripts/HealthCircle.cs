using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthCircle : MonoBehaviour
{
    public Image fill;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        HPChange((player.GetComponent<PlayerStats>().CurrentHealth/3));
    }

    void HPChange(float cd)
    {
        fill.fillAmount = cd;
    }
}
