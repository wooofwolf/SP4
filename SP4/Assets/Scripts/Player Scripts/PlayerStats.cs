using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public int DoubleJumps = 1;
    public PlayerDeath playerDeath;
    public bool dead = false;
    public HealthCircle healthbar;
    public bool tripJump = false;
    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            Die();
            dead = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        if (tripJump)
        {
            DoubleJumps = 2;
        }
        else
        {
            DoubleJumps = 1;
        }
    }

    public override void Die()
    {
        playerDeath.ShowDeath();
    }   
}
