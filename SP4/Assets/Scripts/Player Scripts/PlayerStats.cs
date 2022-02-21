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
    PlayerMovement movement;
    
    //private void Awake()
    //{
    //    //CurrentHealth = MaxHealth;
    //    movement = GetComponent<PlayerMovement>();
    //}
    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            Die();
          
        }
        if (tripJump)
        {
            DoubleJumps = 0;
            movement = GetComponent<PlayerMovement>();
            movement.jumpForce = 1000f;
        }
        else
        {
            movement = GetComponent<PlayerMovement>();
            DoubleJumps = 1;
            movement.jumpForce = 550f;
            
        }
    }

    public override void Die()
    {
        playerDeath.ShowDeath();
        dead = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }   
}
