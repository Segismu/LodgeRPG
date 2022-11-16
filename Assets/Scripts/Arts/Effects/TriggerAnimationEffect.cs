using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Arts.Effects
{
    [CreateAssetMenu(fileName = "Trigger Animation Effect", menuName = "Arts/Effects/Trigger Animation")]
    public class TriggerAnimationEffect : EffectStrategy
    {
        [SerializeField] string animationTrigger;

        public override void StartEffect(AbilityData data, Action finished)
        {
            Animator animator = data.GetUser().GetComponent<Animator>();
            animator.SetTrigger(animationTrigger);
            finished();
        }
    }
}
