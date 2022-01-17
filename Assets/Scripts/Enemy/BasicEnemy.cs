using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicEnemy : MonoBehaviour, IDamageable {

    private MeshRenderer meshRenderer;
    
    protected NavMeshAgent agent;
    protected Transform baseDestination;
    protected GameObject player;

    [SerializeField] private EnemySO enemySO;
    protected float hp;
    protected float damage;
    protected float moveSpeed;
    protected float attackSpeed;
    protected float range;
    protected float physicResistance;
    protected float magicResistance;
    public int lifeCost { get; private set; }
    protected int goldReward;
    protected int xpReward;
    
    void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        
        baseDestination = GameObject.Find("Base").transform;
        player = GameObject.Find("Player");
    }

    void Start() {
        InitEnemy();
        
        agent.destination = baseDestination.position;
        agent.speed = moveSpeed;
    }

    private void InitEnemy() {
        hp = enemySO.hp;
        damage = enemySO.damage;
        moveSpeed = enemySO.moveSpeed;
        attackSpeed = enemySO.attackSpeed;
        range = enemySO.range;
        physicResistance = enemySO.physicResistance;
        magicResistance = enemySO.magicResistance;
        lifeCost = enemySO.lifeCost;
        goldReward = enemySO.goldReward;
        xpReward = enemySO.xpReward;
    }

    public virtual void TakeDamage(float amount, DamageType damageType) {
        float amountWithModifiers = damageType switch {
            DamageType.PHYSICAL => amount / physicResistance,
            DamageType.MAGICAL => amount / magicResistance,
            _ => amount
        };

        hp -= amountWithModifiers;
        if (hp <= 0) Die();
        else StartCoroutine(DamageTick());
    }

    private IEnumerator DamageTick() {
        yield return null;
        /*Color tmpColor = meshRenderer.material.color;
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        meshRenderer.material.color = tmpColor;*/
    }

    public virtual void Die() {
        GameManager.instance.gold += goldReward;
        PlayerManager.instance.GainXP(xpReward);
        Destroy(gameObject);
    }
}
