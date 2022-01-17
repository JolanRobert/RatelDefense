using UnityEngine;

public class CactusEnemy : BasicEnemy {

    private bool isAggro;
    private bool isAttacking;
    private float nextAttackCounter;

    void Update() {
        if (isAggro) {
            agent.destination = player.transform.position;
            TryAttack();
        }
        else agent.destination = baseDestination.position;

        if (isAttacking) agent.velocity = Vector3.zero;
        else agent.speed = moveSpeed;
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
        player.GetComponent<IDamageable>().TakeDamage(damage,DamageType.RAW);
        nextAttackCounter = 1/attackSpeed;
    }

    public override void TakeDamage(float amount, DamageType damageType) {
        base.TakeDamage(amount, damageType);
        isAggro = true;
    }
}
