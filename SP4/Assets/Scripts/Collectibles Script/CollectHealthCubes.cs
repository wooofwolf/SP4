using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHealthCubes : MonoBehaviour
{
    public ShowHealed showhealed;
    private float showhealedtimer = 2.0f;
    public bool showheal = false;
    public AudioSource collectionSound;
    private PlayerStats player;
    Healthbar healthbar;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (showheal == true)
        {
            Debug.Log("show screen");
            showhealedtimer -= Time.deltaTime;
            if (showhealedtimer <= 0)
            {
                showheal = false;
                Destroy(gameObject);
            }
        }

        if(showheal == true) Showheal();
        else if (showheal == false) Stopheal();
    }
    private void OnTriggerEnter(Collider other)
    {
        //showhealed.Showhealed();
        showheal = true;
        collectionSound.Play();
        player.CurrentHealth += 1;
        player.Heal(1);
        transform.position = new Vector3(0, -10, 0);
        //Destroy(gameObject);
        //Debug.Log("Health+1");
    }
    public void Showheal()
    {
        showhealed.Showhealed();
    }

    public void Stopheal()
    {
        showhealed.Stophealed();
    }
}
