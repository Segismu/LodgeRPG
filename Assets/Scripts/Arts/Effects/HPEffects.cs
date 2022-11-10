using System;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Arts.Effects
{
    [CreateAssetMenu (fileName = "HP effect", menuName = "Arts/Effects/HP")]
    public class HPEffects : EffectStrategy
    {
        [SerializeField] float healthChange;

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach (var target in data.GetTargets())
            {
                var hp = target.GetComponent<HP>();
                if (hp)
                {
                    if (healthChange < 0)
                    {
                        hp.TakeDamage(data.GetUser(), -healthChange);
                    }
                    else
                    {
                        hp.Heal(healthChange);
                    }
                }
            }
            finished();
        }
    }
}