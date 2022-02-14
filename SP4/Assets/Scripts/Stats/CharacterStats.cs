using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int MaxHealth = 3;
    public int CurrentHealth { get; private set; }
    public int Damage = 1;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage()
    {
        CurrentHealth -= 1;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Function should be overwritten
        Debug.Log(transform.name + " died.");
    }
}
