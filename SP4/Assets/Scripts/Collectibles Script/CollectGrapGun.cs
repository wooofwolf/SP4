using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGrapGun : MonoBehaviour
{
    public GrapGunPickup pickup;

    private void OnTriggerEnter(Collider other)
    {
        pickup.activeGrappling();
        Destroy(gameObject);
    }
}
