using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]

public class TowerSO : ScriptableObject {

    public new string name;
    public TowerSO nextTowerSO;
    public int level;
    public string projectileName;
    public int goldCost;
    public int damageMin;
    public int damageMax;
    public float range;
    public float attackSpeed;
    public DamageType damageType;
}
