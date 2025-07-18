using System;
using UnityEngine;

public class EnemyKillEvent
{
    public event Action<GameObject> onEnemyKilled;
    public void EnemyKilled(GameObject attacker)
    {

        onEnemyKilled?.Invoke(attacker);
    }
}
