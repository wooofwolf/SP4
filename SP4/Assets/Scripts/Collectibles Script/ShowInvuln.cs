using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInvuln : MonoBehaviour
{
    public void Showinvuln()
    {
        gameObject.SetActive(true);
    }

    public void Stopinvuln()
    {
        gameObject.SetActive(false);
    }
}
