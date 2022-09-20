using System;
using RPG.Core;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "LodgeRPG/Weapons", order = 0)]

    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] WeaponComponents equippedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float percentageBonus = 0f;
        [SerializeField] float hitRange = 2f;
        [SerializeField] bool isRHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public WeaponComponents Spawn(Transform rHand, Transform lHand, Animator animator)
        {
            DestroyOldWeapon(rHand, lHand);

            WeaponComponents weapon = null;
            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rHand, lHand);
                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }

            var overrifeController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrifeController != null)
            {

                animator.runtimeAnimatorController = overrifeController.runtimeAnimatorController;

            }

            return weapon;
        }

        private void DestroyOldWeapon(Transform rHand, Transform lHand)
        {
            Transform oldWeapon = rHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = lHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "Breaking";
            Destroy(oldWeapon.gameObject);
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

        public void LaunchProjectile(Transform rHand, Transform lHand, HP target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rHand, lHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetPercentageBonus()
        {
            return percentageBonus;
        }

        public float GetRange()
        {
            return hitRange;
        }
    }
}