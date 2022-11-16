using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Attributes;
using RPG.Core;
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
        [SerializeField] float aeCost = 0;

        public override void Use(GameObject user)
        {
            AE ae = user.GetComponent<AE>();

            if (ae.GetAE() < aeCost)
            {
                return;
            }

            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();

            if(cooldownStore.GetTimeRemaining(this) > 0)
            {
                return;
            }

            AbilityData data = new AbilityData(user);

            Scheduler scheduler = user.GetComponent<Scheduler>();
            scheduler.StartAction(data);

            targetingStrategy.StartTargeting(data,
                () => {
                    TargetAquired(data  );
                    });
        }

        private void TargetAquired(AbilityData data)
        {
            if (data.IsCancelled()) return;

            AE ae = data.GetUser().GetComponent<AE>();
            if (!ae.UseAE(aeCost)) return;

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