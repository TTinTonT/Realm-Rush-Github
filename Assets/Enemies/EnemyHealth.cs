using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoint = 5;
    [SerializeField] int difficultRam = 1;
    int currentHitPoint;
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }


    private void OnEnable()
    {
        currentHitPoint = maxHitPoint;
    }

    private void OnParticleCollision(GameObject other)
    {
        currentHitPoint -= 1;
        if (currentHitPoint <= 0)
        {
            gameObject.SetActive(false);
            enemy.rewardGold();
            maxHitPoint += difficultRam;
        }
    }
}
