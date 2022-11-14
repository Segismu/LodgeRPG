using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace RPG.Arts
{
    [CreateAssetMenu(fileName = "Base Art", menuName = "Arts/Art", order = 0)]

    public class Art : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy;
        [SerializeField] FilteringStrategy[] filteringStrategies;
        [SerializeField] EffectStrategy[] effectStrategies;
        [SerializeField] float cooldownTime = 0;

        public override void Use(GameObject user)
        {
            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();

            if(cooldownStore.GetTimeRemaining(this) > 0)
            {
                return;
            }

            AbilityData data = new AbilityData(user);
            targetingStrategy.StartTargeting(data,
                () => {
                    TargetAquired(data  );
                    });
        }

        private void TargetAquired(AbilityData data)
        {
            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>();
            cooldownStore.StartCooldown(this, cooldownTime);

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