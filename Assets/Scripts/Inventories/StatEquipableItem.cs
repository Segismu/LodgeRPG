using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = ("RPG/Inventory/Equipable Item"))]
    public class StatEquipableItem : EquipableItem, IModProvider
    {
        [SerializeField]
        Modifier[] additiveModifiers;
        [SerializeField]
        Modifier[] percentajeModifiers;

        [System.Serializable]
        struct Modifier
        {
            public Stat stat;
            public float value;
        }

        public IEnumerable<float> GetAdditiveMod(Stat stat)
        {
            foreach (var modifier in additiveModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }

        public IEnumerable<float> GetPercentageMod(Stat stat)
        {
            foreach (var modifier in percentajeModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }

    }
}