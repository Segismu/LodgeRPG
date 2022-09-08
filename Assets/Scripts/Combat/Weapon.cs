using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "LodgeRPG/Weapons", order = 0)]

    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float hitRange = 2f;
        [SerializeField] bool isRHanded = true;
        [SerializeField] Projectile projectile = null;

        public void Spawn(Transform rHand, Transform lHand, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rHand, lHand);
                Instantiate(equippedPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetTransform(Transform rHand, Transform lHand)
        {
            Transform handTransform;
            if (isRHanded) { handTransform = rHand; }
            else { handTransform = lHand; }

            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rHand, Transform lHand, HP target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rHand, lHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return hitRange;
        }
    }
}