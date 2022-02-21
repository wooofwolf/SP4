using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAS : MonoBehaviour
{
    public void showAS()
    {
        gameObject.SetActive(true);
    }

    public void stopAS()
    {
        gameObject.SetActive(false);
    }
}
