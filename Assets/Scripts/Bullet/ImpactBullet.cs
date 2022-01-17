using System.Collections.Generic;
using UnityEngine;

public class ImpactBullet : BasicBullet {

    [SerializeField] private ImpactBulletZone impactZone;
    
    public void InitBullet(string bulletName, int damage, DamageType damageType, float impactRadius) {
        base.InitBullet(bulletName,damage,damageType);
        impactZone.SetImpactRadius(impactRadius);
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<IDamageable>() != null && other.name != "Player") {
            IDamageable hitObject = other.GetComponent<IDamageable>();
            hitObject.TakeDamage(damage,damageType);
            
            impactZone.inImpactZoneGOs.Remove(hitObject);
            foreach (IDamageable item in impactZone.inImpactZoneGOs) item.TakeDamage(damage,damageType);
        }
        if (!other.GetComponent<Tower>())
            Pooler.instance.Depop(bulletName,gameObject);
    }
}
