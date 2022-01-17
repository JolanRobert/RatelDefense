using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Tower : MonoBehaviour
{
    
    public TowerSO towerSO;

    
    [SerializeField] protected float projectilSpeed = 200f;
    
    [SerializeField] protected SphereCollider sc;
    protected GameObject bullet; 
    protected Rigidbody projectilRb;

    [SerializeField] protected GameObject lvl1;
    [SerializeField] protected GameObject lvl2;
    [SerializeField] protected GameObject lvl3;
    private Transform bulletSpawn;
    [SerializeField] protected List<Transform> enemyInRange = new List<Transform>();

    protected bool isShooting ;

    protected void Start()
    {
        sc.radius = towerSO.range;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<BasicEnemy>())
        {
            enemyInRange.Add(other.transform);
        }
    }
    
    protected void OnTriggerExit(Collider other)
    {
        if (enemyInRange.Contains(other.transform))
        {
            enemyInRange.Remove(other.transform);
        }
    }
    
    protected void Update()
    {
        if (enemyInRange.Count > 0)
        {
            if (enemyInRange[0] == null) return;
            if(!isShooting)
                StartCoroutine(Shoot(towerSO.attackSpeed));
        }
        SetMesh();
        
    }
    protected virtual void SetMesh()
    {
        switch (towerSO.level)
        {
            case 1 :
                lvl1.SetActive(true);
                bulletSpawn = lvl1.transform.GetChild(0).transform;
                lvl2.SetActive(false);
                lvl3.SetActive(false);
                break;
            case 2 :
                lvl1.SetActive(false);
                lvl2.SetActive(true);
                bulletSpawn = lvl2.transform.GetChild(0).transform;
                lvl3.SetActive(false);
                break;
            case 3 :
                lvl1.SetActive(false);
                lvl2.SetActive(false);
                lvl3.SetActive(true);
                bulletSpawn = lvl3.transform.GetChild(0).transform;
                break;
        }
    }
    protected virtual IEnumerator Shoot(float t)
    {
        if (enemyInRange[0] != null)
        {
            isShooting = true;
            bullet = Pooler.instance.Pop(towerSO.projectileName);
            bullet.transform.parent = null;
            bullet.transform.position = bulletSpawn.position;
            bullet.GetComponent<BasicBullet>().InitBullet(towerSO.projectileName,Random.Range(towerSO.damageMin,towerSO.damageMax+1),towerSO.damageType);
            bullet.GetComponent<Rigidbody>().AddForce((enemyInRange[0].position-bullet.transform.position).normalized * projectilSpeed,ForceMode.Impulse);
            Pooler.instance.DelayedDepop(3,towerSO.projectileName,bullet);
            yield return new WaitForSeconds(t);
            isShooting = false;
        }

    }
    
    
    
    
    
}
