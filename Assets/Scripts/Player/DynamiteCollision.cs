using UnityEngine;

public class DynamiteCollision : MonoBehaviour
{
    [SerializeField] private float radius;
    private Collider[] hits;

    public static DynamiteCollision instance;

    [SerializeField] private GameObject explosionEffect;

    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter(Collision other)
    {
        CircleRaycast();
        Explosion.instance.ResetThrow();
    }

    public void CircleRaycast()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Debug.Log("ok");
        hits = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in hits )
        {
            if (hit.gameObject.GetComponent<IDamageable>()!=null)
            {
                hit.gameObject.GetComponent<IDamageable>().TakeDamage(PlayerManager.instance.damage*PlayerManager.instance.attackSpeed*PlayerManager.instance.level,DamageType.RAW);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,radius);
    }
}
