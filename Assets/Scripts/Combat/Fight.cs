using UnityEngine;
using RPG.Movement;
using RPG.Core;
using GameDevTV.Saving;
using RPG.Attributes;
using RPG.Stats;
using System.Collections.Generic;
using GameDevTV.Utils;
using System;
using GameDevTV.Inventories;

namespace RPG.Combat
{
    public class Fight : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenHits = 1f;
        [SerializeField] Transform rHandTransform = null;
        [SerializeField] Transform lHandTransform = null;
        [SerializeField] Weapon baseWeapon = null;

        HP target;
        Equipment equipment;
        float timeSinceLastHit = Mathf.Infinity;
        Weapon currentWeapon;
        LazyValue<WeaponComponents> currentWeaponNow;

        private void Awake()
        {
            currentWeapon = baseWeapon;
            currentWeaponNow = new LazyValue<WeaponComponents>(SetupBaseWeapon);

            equipment = GetComponent<Equipment>();
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
            }
        }

        private WeaponComponents SetupBaseWeapon()
        {
            return AttachWeapon(baseWeapon);
        }

        private void Start()
        {
            currentWeaponNow.ForceInit();
        }

        private void Update()
        {
            timeSinceLastHit += Time.deltaTime;

            if (target == null) return;

            if (target.IsDead()) return;

            if (!GetIsInRage(target.transform))
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
            currentWeapon = weapon;
            currentWeaponNow.value = AttachWeapon(weapon);
        }

        private void UpdateWeapon()
        {
            var weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as Weapon;
            if (weapon == null)
            {
                EquipWeapon(baseWeapon);
            }
            else
            {
                EquipWeapon(weapon);
            }
        }

        private WeaponComponents AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rHandTransform, lHandTransform, animator);
        }

        public HP GetTarget()
        {
            return target;
        }

        public Transform GetHandTransform(bool isRightHand)
        {
            if (isRightHand)
            {
                return rHandTransform;
            }
            else
            {
                return lHandTransform;
            }
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

            if(currentWeaponNow.value != null)
            {
                currentWeaponNow.value.OnHit();
            }

            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rHandTransform, lHandTransform, target, gameObject, damage);
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

        private bool GetIsInRage(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeapon.GetRange();
        }

        public bool CanAttack(GameObject fightTarget)
        {
            if (fightTarget == null) { return false; }
            if (!GetComponent<Mover>().CanMoveTo(fightTarget.transform.position) && !GetIsInRage(fightTarget.transform)) { return false; }
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

        public object CaptureState()
        {
            if (baseWeapon == null)
            {
                return "Barehand";
            }
            else
            {
                return currentWeapon.name;
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