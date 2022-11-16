using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Arts.Effects
{
    [CreateAssetMenu(fileName = "Orient To Target Effect", menuName = "Arts/Targeting/Orient To Target")]
    public class OrientToTargetEffect : EffectStrategy
    {
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.GetUser().transform.LookAt(data.GetTargetedPoint());
            finished();
        }
    }
}
