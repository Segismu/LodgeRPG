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

        public override void StartEffect(AbilityData data, Action finished)
        {
            Fight fighter = data.GetUser().GetComponent<Fight>();
            Vector3 spawnPos = fighter.GetHandTransform(isRightHand).position;

            foreach (var target in data.GetTargets())
            {
                HP hp = target.GetComponent<HP>();

                if (hp)
                {
                    Projectile projectile = Instantiate(projectileToSpawn);
                    projectile.transform.position = spawnPos;
                    projectile.SetTarget(hp, data.GetUser(), damage);
                }
                finished();
            }
        }
    }
}