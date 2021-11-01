using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticales;
    [SerializeField] float range = 25f;

    Transform target;


    private void Start()
    {
        projectileParticales = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        StartCoroutine(FindClosestEnemy());
        AimWeapon();
    }

    IEnumerator FindClosestEnemy()
   {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform ClosestEnemy = null;
        float maxDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                ClosestEnemy = enemy.transform;
                maxDistance = targetDistance;
            }           
        }
        target = ClosestEnemy;
        yield return new WaitForSeconds(0.2f);
   }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target.position);
        if (targetDistance <= range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    private void Attack(bool isActive)
    {
        var emissionModule = projectileParticales.emission;
        emissionModule.enabled = isActive;
    }
}
