using UnityEngine;

public class HeroPassive : MonoBehaviour
{
    [SerializeField] private int enemiesToHeal = 5;
    [SerializeField] private int healAmount = 5;

    private int killCount = 0;
    private Stats stats;

    private void OnEnable()
    {
        EventManager.instance.enemyKillEvent.onEnemyKilled += OnEnemyKilled;
    }
    private void OnDisable()
    {
        EventManager.instance.enemyKillEvent.onEnemyKilled -= OnEnemyKilled;
    }
    private void Awake()
    {
        stats = GetComponent<Stats>();
    }

    public void OnEnemyKilled(GameObject attacker)
    {
        if (!(attacker.tag == gameObject.tag)) return;

        killCount++;
        if (killCount >= enemiesToHeal)
        {
            killCount = 0;
            stats.Heal(healAmount);
        }
    }
}
