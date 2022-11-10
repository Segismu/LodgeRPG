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
            AbilityData data = new AbilityData(user);
            targetingStrategy.StartTargeting(data,
                () => {
                    TargetAquired(data  );
                    });
        }

        private void TargetAquired(AbilityData data)
        {

            foreach (var filteringStrategy in filteringStrategies)
            {
                data.SetTargets(filteringStrategy.Filter(data.GetTargets()));
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(data, EffectFinished);
            }

        }

        private void EffectFinished()
        {

        }

    }
}