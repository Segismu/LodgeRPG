using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;

namespace RPG.Combat
{
    public class Fight : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenHits = 1f;
        [SerializeField] Transform rHandTransform = null;
        [SerializeField] Transform lHandTransform = null;
        [SerializeField] Weapon baseWeapon = null;

        HP target;
        float timeSinceLastHit = Mathf.Infinity;
        Weapon currentWeapon;

        private void Awake()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(baseWeapon);
            }
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
            currentWeapon = weapon;
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

        private bool GetIsInRage()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
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