using System;
using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public static Shoot instance;

    private RaycastHit hit;
    private Vector3 origin;
    private Vector3 direction;
    [SerializeField] private Transform player;
    [SerializeField] private bool isCanShoot;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private Transform effectSpawn;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable() {
        isCanShoot = true;
    }

    public void Fire()
    {
        if (isCanShoot)
        {
            StartCoroutine(ShootAnim());
            Instantiate(fireEffect, effectSpawn.position, Quaternion.Euler(0,180+player.eulerAngles.y,0)).transform.parent = transform;
            StartCoroutine(WaitShoot());
            origin = player.position;
            direction = player.transform.forward;
            if (Physics.Raycast(origin,direction,out hit))
            {
                Debug.DrawRay(origin,direction*hit.distance,Color.red);
                if (hit.collider.gameObject.GetComponent<IDamageable>()!=null)
                {
                    hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(PlayerManager.instance.damage,DamageType.RAW);
                }
            }
        }
    }

    IEnumerator WaitShoot()
    {
        isCanShoot = false;
        yield return new WaitForSeconds(1/PlayerManager.instance.attackSpeed);
        isCanShoot = true;
    }

    IEnumerator ShootAnim()
    {
        animator.SetBool("isShoot",true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isShoot",false);
    }
}
