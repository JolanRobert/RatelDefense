using System;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public float currentHealth;
    [SerializeField] private float maxHealth;
    public float damage;
    [SerializeField] private float xp;
    public float attackSpeed;
    public int level;
    public static PlayerManager instance;
    [SerializeField] private float[] healths;
    [SerializeField] private float[] damages;
    [SerializeField] private float[] attackSpeeds;
    [SerializeField] private float[] xps;
    [SerializeField] private int levelMax;
    [SerializeField] private int cooldown = 15;
    public bool isDeath;
    [SerializeField] private MeshRenderer playerMesh;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    void Update() {
        GameManager.instance.uiManager.UpdatePlayerHealth(currentHealth);
    }

    void Init()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        damage = 5;
        attackSpeed = 1;
        xp = 100;
        level = 1;
    }

    public void GainXP(int xpReward)
    {
        if (level != levelMax)
        {
            xp -= xpReward;
            if (xp<=0 )
            {
                LevelUp();
            }
        }
    }

    public void LevelUp()
    {
        level++;
        maxHealth = healths[level - 1];
        currentHealth = maxHealth;
        damage = damages[level - 1];
        attackSpeed = attackSpeeds[level - 1];
        if (level<levelMax)
        {
            xp += xps[level]; 
        }
    }

    public void TakeDamage(float amount, DamageType damageType)
    {
        currentHealth -= amount;
        if (currentHealth<=0)
        {
            currentHealth = maxHealth;
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isDeath = true;
        playerMesh.enabled = false;
        //top down + !isPLay + anim cam
        yield return new WaitForSeconds(cooldown);
        //FPS + isPLay + animCam
        isDeath = false;
        playerMesh.enabled = true;
    }
    
}
