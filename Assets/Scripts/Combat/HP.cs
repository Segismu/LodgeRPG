using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class HP : MonoBehaviour
    {
        [SerializeField] float hp = 100f;

        public void TakeDamage(float damage)
        {
            hp = Mathf.Max(hp - damage, 0);
            print(hp);
        }
    }
}


