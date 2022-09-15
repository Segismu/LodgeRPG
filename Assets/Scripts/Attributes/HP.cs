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
        [SerializeField] float regenPercentage = 80;

        float hpPoints = -1f;

        bool isDead = false;

        private void Awake()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHP;
            if (hpPoints < 0)
            {
                hpPoints = GetComponent<BaseStats>().GetStat(Stat.HP);
            }

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
            return 100 * (hpPoints / GetComponent<BaseStats>().GetStat(Stat.HP));
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

            exp.GainExp(GetComponent<BaseStats>().GetStat(Stat.ExpReward));
        }

        public object CaptureState()
        {
            return hpPoints;
        }

        private void RegenerateHP()
        {
            float regenHPPoints = GetComponent<BaseStats>().GetStat(Stat.HP) * (regenPercentage / 100);
            hpPoints = Mathf.Max(hpPoints, regenHPPoints);
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


