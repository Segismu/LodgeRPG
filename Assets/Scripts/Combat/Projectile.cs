using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;

    [SerializeField] HP target = null;

    void Update()
    {
        if (target == null) return;

        transform.LookAt(GetAimLoc());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget (HP target)
    {
        this.target = target;
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
}