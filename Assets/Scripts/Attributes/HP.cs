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

        public bool wasDeadLastFrame = false;

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
            return hpPoints.value <= 0;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            hpPoints.value = Mathf.Max(hpPoints.value - damage, 0);

            if (IsDead())
            {
                onDie.Invoke();
                AwardExp(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
            UpdateState();
        }

        public void Heal(float hpToRestore)
        {
            hpPoints.value = Mathf.Min(hpPoints.value + hpToRestore, GetMaxHpPoints());
            UpdateState();
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

        private void UpdateState()
        {
            Animator animator = GetComponent<Animator>();
            if (!wasDeadLastFrame && IsDead())
            {
                animator.SetTrigger("die");
                GetComponent<Scheduler>().CancelCurrentAction();
            }

            if (wasDeadLastFrame && !IsDead())
            {
                animator.Rebind();
            }

            wasDeadLastFrame = IsDead();
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

                UpdateState();
        }
    }
}


