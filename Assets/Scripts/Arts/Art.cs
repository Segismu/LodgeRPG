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
            targetingStrategy.StartTargeting(user);
        }

    }
}