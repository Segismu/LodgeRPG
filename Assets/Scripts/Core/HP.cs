using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class HP : MonoBehaviour, ISaveable
    {
        [SerializeField] float hpPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            hpPoints = Mathf.Max(hpPoints - damage, 0);

            if (hpPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Scheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return hpPoints;
        }

        public void RestoreState(object state)
        {
            hpPoints = (float) state;

            if (hpPoints <= 0)
            {
                Die();
            }
        }
    }
}


