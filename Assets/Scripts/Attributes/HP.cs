using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;
using RPG.Stats;
using RPG.Core;
using System;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class HP : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenPercentage = 80;
        [SerializeField] TakeDamageEvent takeDamage;
        public UnityEvent onDie;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {}

        LazyValue<float> hpPoints;

        public bool isDead = false;

        private void Awake()
        {
            hpPoints = new LazyValue<float>(GetInitialHP);
        }

        private float GetInitialHP()
        {
            return GetComponent<BaseStats>().GetStat(Stat.HP);
        }

        private void Start()
        {
            hpPoints.ForceInit();
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHP;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHP;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            hpPoints.value = Mathf.Max(hpPoints.value - damage, 0);

            if (hpPoints.value <= 0)
            {
                onDie.Invoke();
                Die();
                AwardExp(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public void Heal(float hpToRestore)
        {
            hpPoints.value = Mathf.Min(hpPoints.value + hpToRestore, GetMaxHpPoints());
        }

        public float GetHpPoints()
        {
            return hpPoints.value;
        }

        public float GetMaxHpPoints()
        {
           return GetComponent<BaseStats>().GetStat(Stat.HP);
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return hpPoints.value / GetComponent<BaseStats>().GetStat(Stat.HP);
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
            return hpPoints.value;
        }

        private void RegenerateHP()
        {
            float regenHPPoints = GetComponent<BaseStats>().GetStat(Stat.HP) * (regenPercentage / 100);
            hpPoints.value = Mathf.Max(hpPoints.value, regenHPPoints);
        }

        public void RestoreState(object state)
        {
            hpPoints.value = (float) state;

            if (hpPoints.value <= 0)
            {
                Die();
            }
        }
    }
}


