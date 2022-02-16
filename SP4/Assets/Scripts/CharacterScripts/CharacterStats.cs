using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int MaxHealth = 3;
    public float CurrentHealth = 0;
    public int Damage = 1;
    public float AttackSpeed = 1f;
    public float AttackDelay = 0.6f;

    
    private void Awake()
    {
        CurrentHealth = MaxHealth;
        //if (healthbar != null)
        //{
        //    healthbar.SetMaxHealth(MaxHealth);
        //}
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        //healthbar.SetHealth(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int health)
    {
        CurrentHealth += health;
        //healthbar.SetHealth(CurrentHealth);
        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }    
    public virtual void Die()
    {
        // Function should be overwritten
        Debug.Log(transform.name + " died.");
    }
}
