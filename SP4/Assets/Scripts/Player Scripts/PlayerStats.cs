using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    
    public int DoubleJumps = 1;

    public bool dead = false;
    public bool invul = false;
    public bool tallJump = false;
    public bool tjeffectscheck = false;
    public bool invulneffectcheck = false;

    public float tallJumpTimer = 15f;
    public float maxTallJumpTimer;
    public float tallJumpForce = 1300f;
    public float tjeffectTime = 3.0f;
    public float InvulnTimer = 3.0f;
    public float maxInvulnTimer;
    public float invulneffectTimer = 3f;

    public ShowTJ showTj;

    public PlayerDeath playerDeath;
    public HealthCircle healthbar;
    public HealthFadeScript healthFade;
    PlayerMovement movement;
    public GameObject tallJumpTimerBar;
    public ShowInvuln showinvuln;
    public ShieldFadeScript shieldFade;
    public GameObject invulnTimerbar;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        //movement = GetComponent<PlayerMovement>();
        maxTallJumpTimer = tallJumpTimer;
        maxInvulnTimer = InvulnTimer;
    }
    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            Die();
            healthFade.DisableOnePulse();
            healthFade.DisableContinuousPulse();
        }

        if (tallJump)
        {
            DoubleJumps = 0;
            movement = GetComponent<PlayerMovement>();
            movement.jumpForce = tallJumpForce;

            tjeffectscheck = true;
            tallJumpTimerBar.SetActive(true);
            TimerBar jumpSlider = tallJumpTimerBar.GetComponent<TimerBar>();
            jumpSlider.SetMaxBar(maxTallJumpTimer);
            tallJumpTimer -= Time.deltaTime;
            jumpSlider.SetBar(tallJumpTimer);
            if (tallJumpTimer <= 0)
            {
                tallJumpTimerBar.SetActive(false);
                //tallJumpTimer = maxTallJumpTimer;
                tallJump = false;
            }
            if (tallJumpTimer <= maxTallJumpTimer - tjeffectTime)
            {
                tjeffectscheck = false;
            }
        }
        else if (!tallJump)
        {
            movement = GetComponent<PlayerMovement>();
            DoubleJumps = 1;
            movement.jumpForce = 550f;
            
        }

        if (invul)
        {
            invulneffectcheck = true;

            shieldFade.ShowEffect();
            invulnTimerbar.SetActive(true);
            TimerBar invulnSlider = invulnTimerbar.GetComponent<TimerBar>();
            invulnSlider.SetMaxBar(maxInvulnTimer);
            InvulnTimer -= Time.deltaTime;
            invulnSlider.SetBar(InvulnTimer);

            if (InvulnTimer <= 0)
            {
                shieldFade.HideEffect();
                invulnTimerbar.SetActive(false);
                invul = false;
            }
            if (InvulnTimer <= maxInvulnTimer - invulneffectTimer)
            {
                Debug.Log("stop showing");
                invulneffectcheck = false;
            }
        }

        if (invulneffectcheck)
        {
            showinvuln.Showinvuln();
        }
        else if (!invulneffectcheck)
        {
            showinvuln.Stopinvuln();
        }

        if (tjeffectscheck)
        {
            showTj.showtj();
        }
        else if(!tjeffectscheck)
        {
            showTj.stoptj();
        }
    }

    public override void Heal(int health)
    {
        Debug.Log("Healed");
        if (CurrentHealth <= 1)
        {
            healthFade.DisableContinuousPulse();
            Debug.Log("Pulse disabled");    
        }

        CurrentHealth += health;
        //healthbar.SetHealth(CurrentHealth);
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public override void Die()
    {
        playerDeath.ShowDeath();
        dead = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Showtj()
    {
        showTj.showtj();
    }

    public void Stoptj()
    {
        showTj.stoptj();
    }
    public override void TakeDamage(int damage)
    {
        //Debug.Log("imhere");
        if (!invul || (invul && CurrentHealth > 1))
        {
            base.TakeDamage(damage);
            if (CurrentHealth == 1)
            {
                healthFade.PulseEffectContinuous();
            }
            else
            {

                healthFade.PulseEffectOnce();
            }
        }
        else if (invul && CurrentHealth == 1)
        {
            base.TakeDamage(0);
        }
    }
    public void ShowInv()
    {
        showinvuln.Showinvuln();
    }

    public void StopInv()
    {
        showinvuln.Stopinvuln();
    }
}
