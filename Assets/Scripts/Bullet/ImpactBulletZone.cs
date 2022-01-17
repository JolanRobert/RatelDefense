using System.Collections.Generic;
using UnityEngine;

public class ImpactBulletZone : MonoBehaviour {
    
    [SerializeField] private SphereCollider impactZone;
    public List<IDamageable> inImpactZoneGOs = new List<IDamageable>();

    public void SetImpactRadius(float impactRadius) {
        impactZone.radius = impactRadius;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<IDamageable>() == null) return;
        if (other.name == "Player") return;
        inImpactZoneGOs.Add(other.GetComponent<IDamageable>());
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<IDamageable>() == null) return;
        if (other.name == "Player") return;
        inImpactZoneGOs.Remove(other.GetComponent<IDamageable>());
    }
}
