using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Arts
{
    public abstract class EffectStrategy : ScriptableObject
    {
        public abstract void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action finished);
    }
}
