using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Stat Data")]
    [SerializeField] private StatSO baseStats;

    public int MaxHP => baseStats.maxHP;
    public int Damage => baseStats.damage;
    public int Armor => baseStats.armor;

    public int CurrentHP { get; private set; }
    public bool IsDead { get; private set; }

    public System.Action<GameObject> OnDeath;
    public System.Action<int, int> OnHealthChanged;

    private void Awake()
    {
        ResetHP();
    }

    public void TakeDamage(int rawDamage, GameObject attacker = null)
    {
        if (IsDead) return;

        int finalDamage = Mathf.Max(rawDamage - Armor, 1);
        CurrentHP = Mathf.Max(CurrentHP - finalDamage, 0);
        OnHealthChanged?.Invoke(CurrentHP, MaxHP);

        if (CurrentHP <= 0 && !IsDead)
        {
            IsDead = true;
            OnDeath?.Invoke(attacker);
        }
    }

    public void Heal(int amount)
    {
        CurrentHP = Mathf.Min(CurrentHP + amount, MaxHP);
        OnHealthChanged?.Invoke(CurrentHP, MaxHP);
    }

    public void ResetHP()
    {
        IsDead = false;
        CurrentHP = MaxHP;
        OnHealthChanged?.Invoke(CurrentHP, MaxHP);
    }
}
