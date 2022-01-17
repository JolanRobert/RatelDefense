using UnityEngine;

public class BasicBullet : MonoBehaviour {

    protected string bulletName;
    protected int damage;
    protected DamageType damageType;

    public void InitBullet(string bulletName, int damage, DamageType damageType) {
        this.bulletName = bulletName;
        this.damage = damage;
        this.damageType = damageType;
    }
}
