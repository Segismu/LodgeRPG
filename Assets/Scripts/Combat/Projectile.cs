using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    HP target = null;
    float damage = 0;

    void Update()
    {   
        if (target == null) return;

        transform.LookAt(GetAimLoc());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget (HP target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private Vector3 GetAimLoc()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

        if (targetCapsule == null)
        {
            return target.transform.position;
        }

        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HP>() != target) return;
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}