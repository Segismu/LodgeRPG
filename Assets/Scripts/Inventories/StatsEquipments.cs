using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;

namespace RPG.Inventories
{
    public class StatsEquipments : Equipment, IModProvider
    {
        public IEnumerable<float> GetAdditiveMod(Stat stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModProvider;
                if (item == null) continue;

                foreach (float modifier in item.GetAdditiveMod(stat))
                {
                    yield return modifier;
                }
            }
        }

        public IEnumerable<float> GetPercentageMod(Stat stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModProvider;
                if (item == null) continue;

                foreach (float modifier in item.GetPercentageMod(stat))
                {
                    yield return modifier;
                }
            }
        }
    }
}