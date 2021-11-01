using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetLocator))]
public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float delayTime= 1f;   

    private void Start()
    {       
        StartCoroutine(Build());
    }

    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayTime);
        }
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null){ return false; }
        if (bank.CurrentBalance >= cost)
        {
            bank.WithDawn(cost);
            Instantiate(tower.gameObject,position,Quaternion.identity);
            return true;
        }
        return false;
    }

   
}
