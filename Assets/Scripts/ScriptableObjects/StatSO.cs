using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BaseStat", fileName = "BaseStat")]
public class StatSO : ScriptableObject {
    public int maxHP = 100;
    public int damage = 10;
    public int armor = 0;
}