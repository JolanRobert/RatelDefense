using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject {

    public new string name;
    public float hp;
    public float damage;
    public float moveSpeed;
    public float attackSpeed;
    public float range;
    public float physicResistance;
    public float magicResistance;
    public int lifeCost;
    public int goldReward;
    public int xpReward;
}
