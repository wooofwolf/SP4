using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHealed : MonoBehaviour
{
    public void Showhealed()
    {
        gameObject.SetActive(true);
    }

    public void Stophealed()
    {
        gameObject.SetActive(false);
    }
}
