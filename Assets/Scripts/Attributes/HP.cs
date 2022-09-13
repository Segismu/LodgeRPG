using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Attributes
{
    public class HP : MonoBehaviour, ISaveable
    {
        [SerializeField] float hpPoints = 100f;

        bool isDead = false;

        private void Start()
        {
            hpPoints = GetComponent<BaseStats>().GetHealth();
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            hpPoints = Mathf.Max(hpPoints - damage, 0);

            if (hpPoints <= 0)
            {
                Die();
                AwardExp(instigator);
            }
        }

        public float GetPercentage()
        {
            return 100 * (hpPoints / GetComponent<BaseStats>().GetHealth());
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Scheduler>().CancelCurrentAction();
        }

        private void AwardExp(GameObject instigator)
        {
            Experience exp = instigator.GetComponent<Experience>();
            if (exp == null) return;

            exp.GainExp(GetComponent<BaseStats>().expReward());
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


