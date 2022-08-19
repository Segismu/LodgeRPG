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

        Transform target;

        private void Update()
        {
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
            GetComponent<Animator>().SetTrigger("attack");
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

        //Animation
        void Hit()
        {

        }
    }
}