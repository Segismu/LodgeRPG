using System;
using RPG.Attributes;
using RPG.Combat;
using UnityEngine;

namespace RPG.Arts.Effects
{
    [CreateAssetMenu(fileName = "Spawn Projectile Effect", menuName = "Arts/Effects/SpawnProjectile", order = 0)]
    public class SpawnProjectileEffect : EffectStrategy
    {
        [SerializeField] Projectile projectileToSpawn;
        [SerializeField] float damage;
        [SerializeField] bool isRightHand = true;
        [SerializeField] bool useTargetPoint = true;

        public override void StartEffect(AbilityData data, Action finished)
        {
            Fight fighter = data.GetUser().GetComponent<Fight>();
            Vector3 spawnPos = fighter.GetHandTransform(isRightHand).position;

            if (useTargetPoint)
            {
                SpawnProjectileForTargetPoint(data, spawnPos);
            }
            else
            {
                SpawnProjectileForTargets(data, spawnPos);
            }

            finished();
        }

        private void SpawnProjectileForTargetPoint(AbilityData data, Vector3 spawnPos)
        {
            Projectile projectile = Instantiate(projectileToSpawn);
            projectile.transform.position = spawnPos;
            projectile.SetTarget(data.GetTargetedPoint(), data.GetUser(), damage);
        }

        private void SpawnProjectileForTargets(AbilityData data, Vector3 spawnPos)
        {
            foreach (var target in data.GetTargets())
            {
                HP hp = target.GetComponent<HP>();

                if (hp)
                {
                    Projectile projectile = Instantiate(projectileToSpawn);
                    projectile.transform.position = spawnPos;
                    projectile.SetTarget(hp, data.GetUser(), damage);
                }
            }
        }
    }
}