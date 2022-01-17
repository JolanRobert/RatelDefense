using UnityEngine;

public class MageEnemy : BasicEnemy {

    [SerializeField] private float distortionEffectTime;
    public float nextAttackCounter;
    private bool isAttacking;
    
    void Update() {
        agent.destination = player.transform.position;
        TryAttack();
        
        if (isAttacking) agent.velocity = Vector3.zero;
        else agent.speed = moveSpeed;
        
        nextAttackCounter -= Time.deltaTime;
    }
    
    private void TryAttack() {
        if (Vector3.Distance(transform.position, agent.destination) <= range+1) {
            isAttacking = true;
            if (nextAttackCounter <= 0) Attack();
            nextAttackCounter -= Time.deltaTime;
        }
        else isAttacking = false;
    }

    private void Attack() {

        //Damage
        if (Random.Range(0f, 1f) <= 0.7f) player.GetComponent<IDamageable>().TakeDamage(damage,DamageType.RAW);
        //Distortion
        else player.GetComponent<PlayerCameraEffects>().AddDistortion(distortionEffectTime);
        nextAttackCounter = 1 / attackSpeed;
    }
}
