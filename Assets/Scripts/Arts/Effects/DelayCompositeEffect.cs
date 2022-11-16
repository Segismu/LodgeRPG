using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Arts.Effects
{
    [CreateAssetMenu(fileName = "Delay Composite Effect", menuName = "Arts/Effects/Delay Composite")]
    public class DelayCompositeEffect : EffectStrategy
    {
        [SerializeField] float delay = 0;
        [SerializeField] EffectStrategy[] delayedEffects;

        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(DelayedEffect(data, finished));
        }

        private IEnumerator DelayedEffect(AbilityData data, Action finished)
        {
            yield return new WaitForSeconds(delay);

            foreach (var effect in delayedEffects)
            {
                effect.StartEffect(data, finished);
            }
        }
    }
}
