using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int MaxHealth = 3;
    public int CurrentHealth { get; private set; }
    public int Damage = 1;
    public float AttackSpeed = 1f;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
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
