using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0,10)]float spamDelayTime = 2f;
    [SerializeField] int poolSize = 10;

    GameObject[] pool;

    private void Start()
    {
        PopulatePool();
        StartCoroutine("SpamEnemies");
    }   

    private void PopulatePool()
    {
        pool = new GameObject[Mathf.Abs(poolSize)];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    IEnumerator SpamEnemies()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spamDelayTime);           
        }
    }

    private void EnableObjectInPool()
    {
        foreach (var enemy in pool)
        {
            if (enemy.activeInHierarchy != true)
            {
                enemy.SetActive(true);
                return;
            }           
        }
    }
}
