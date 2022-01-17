using UnityEngine;

public class ChariotEnemy : BasicEnemy {

    [SerializeField] private int summonCactus = 2;

    public override void Die() {
        base.Die();
        for (int i = 0; i < summonCactus; i++) {
            WaveManager.instance.SpawnEnemy(EnemyName.CACTUS,transform.position);
        }
    }
}
