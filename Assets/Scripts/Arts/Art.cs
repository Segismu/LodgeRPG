using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Arts
{
    [CreateAssetMenu(fileName = "Base Art", menuName = "Arts/Art", order = 0)]

    public class Art : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilteringStrategy[] filteringStrategies;
        [SerializeField] EffectStrategy[] effectStrategies; 

        public override void Use(GameObject user)
        {
            targetingStrategy.StartTargeting(user,
                (IEnumerable<GameObject> targets) => {
                    TargetAquired(user, targets);
                    });
        }

        private void TargetAquired(GameObject user, IEnumerable<GameObject> targets)
        {

            foreach (var filteringStrategy in filteringStrategies)
            {
                targets = filteringStrategy.Filter(targets);
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(user, targets, EffectFinished);
            }

        }

        private void EffectFinished()
        {

        }

    }
}