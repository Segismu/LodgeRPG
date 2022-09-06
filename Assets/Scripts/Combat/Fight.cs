using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fight : MonoBehaviour, IAction
    {
        [SerializeField] float hitRange = 2f;
        [SerializeField] float timeBetweenHits = 1f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;

        HP target;
        float timeSinceLastHit = Mathf.Infinity;

        private void Start()
        {
            SpawnWeapon();
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

        private void SpawnWeapon()
        {
            if (weapon == null) return;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastHit > timeBetweenHits)
            {
                //This will trigger hit event
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
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRage()
        {
            return Vector3.Distance(transform.position, target.transform.position) < hitRange;
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
    }
}