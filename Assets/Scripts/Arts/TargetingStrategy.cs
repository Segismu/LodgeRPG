using UnityEngine;

namespace RPG.Arts
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        public abstract void StartTargeting();
    }
}
