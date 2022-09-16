using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;
using System.Collections.Generic;
using GameDevTV.Utils;
using System;

namespace RPG.Combat
{
    public class Fight : MonoBehaviour, IAction, ISaveable, IModProvider
    {
        [SerializeField] float timeBetweenHits = 1f;
        [SerializeField] Transform rHandTransform = null;
        [SerializeField] Transform lHandTransform = null;
        [SerializeField] Weapon baseWeapon = null;

        HP target;
        float timeSinceLastHit = Mathf.Infinity;
        LazyValue<Weapon> currentWeapon;

        private void Awake()
        {
            currentWeapon = new LazyValue<Weapon>(SetupBaseWeapon);
        }

        private Weapon SetupBaseWeapon()
        {
            AttachWeapon(baseWeapon);
            return baseWeapon;
        }

        private void Start()
        {
            currentWeapon.ForceInit();
        }

        private void Update()
        {
            timeSinceLastHit += Time.deltaTime;

            if (target == null) return;

            if (target.IsDead()) return;

            if (!GetIsInRage())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
        }

        private void AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rHandTransform, lHandTransform, animator);
        }

        public HP GetTarget()
        {
            return target;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastHit > timeBetweenHits)
            {
                TriggerAttack();
                timeSinceLastHit = 0;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopHit");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation
        void Hit()
        {
            if (target == null) { return; }

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (currentWeapon.value.HasProjectile())
            {
                currentWeapon.value.LaunchProjectile(rHandTransform, lHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);
            }
        }

        void Shoot()
        {
            Hit();
        }

        private bool GetIsInRage()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.GetRange();
        }

        public bool CanAttack(GameObject fightTarget)
        {
            if (fightTarget == null) { return false; }
            HP targetToTest = fightTarget.GetComponent<HP>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject fightTarget)
        {
            GetComponent<Scheduler>().StartAction(this);
            target = fightTarget.GetComponent<HP>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopHit");
        }

        public IEnumerable<float> GetAdditiveMod(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetDamage();
            }
        }

        public IEnumerable<float> GetPercentageMod(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetPercentageBonus();
            }
        }

        public object CaptureState()
        {
            if (baseWeapon == null)
            {
                return "Barehand";
            }
            else
            {
                return currentWeapon.value.name;
            }
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}