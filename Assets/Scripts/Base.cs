using UnityEngine;

public class Base : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other) {
        if (!other.GetComponent<BasicEnemy>()) return;
        GameManager.instance.baseLife -= other.GetComponent<BasicEnemy>().lifeCost;
        Destroy(other.gameObject);
    }
}
