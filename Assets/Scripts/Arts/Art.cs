using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Arts
{
    [CreateAssetMenu(fileName = "Base Art", menuName = "Arts/Art", order = 0)]

    public class Art : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy;

        public override void Use(GameObject user)
        {
            targetingStrategy.StartTargeting(user, TargetAquired);
        }

        private void TargetAquired(IEnumerable<GameObject> targets)
        {
            Debug.Log("Target Aquired");
            foreach (var target in targets)
            {
                Debug.Log(target);
            }
        }

    }
}