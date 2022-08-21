using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using static UnityEngine.GraphicsBuffer;

namespace RPG.Combat
{
    public class Fight : MonoBehaviour, IAction
    {
        [SerializeField] float hitRange = 2f;
        [SerializeField] float timeBetweenHits = 1f;
        [SerializeField] float weaponDamage = 5f;

        Transform target;
        float timeSinceLastHit = 0;

        private void Update()
        {
            timeSinceLastHit += Time.deltaTime;

            if (target == null) return;

            if (!GetIsInRage())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastHit > timeBetweenHits)
            {
                //This will trigger hit event.
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastHit = 0;
            }

        }

        //Animation
        void Hit()
        {
            HP hpComponent = target.GetComponent<HP>();
            hpComponent.TakeDamage(weaponDamage);
        }

        private bool GetIsInRage()
        {
            return Vector3.Distance(transform.position, target.position) < hitRange;
        }


        public void Attack(FightTarget fightTarget)
        {
            GetComponent<Scheduler>().StartAction(this);
            target = fightTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}