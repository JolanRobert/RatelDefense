using System;
using UnityEngine;

public class ClassicBullet : BasicBullet {
    
    private void OnTriggerEnter(Collider other) {
        
        if (other.GetComponent<IDamageable>() != null && other.name != "Player") {
            IDamageable hitObject = other.GetComponent<IDamageable>();
            hitObject.TakeDamage(damage,damageType);
        }
        if (!other.GetComponent<Tower>())
            Pooler.instance.Depop(bulletName,gameObject);
    }
}
