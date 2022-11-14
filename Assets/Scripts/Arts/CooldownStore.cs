using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Arts
{
    public class CooldownStore : MonoBehaviour
    {

        Dictionary<InventoryItem, float> cooldownTimers = new Dictionary<InventoryItem, float>();
        Dictionary<InventoryItem, float> initialCooldownTimes = new Dictionary<InventoryItem, float>();

        void Update()
        {
            var keys = new List<InventoryItem>(cooldownTimers.Keys);

            foreach (InventoryItem art in keys)
            {
                cooldownTimers[art] -= Time.deltaTime;

                if (cooldownTimers[art] < 0)
                {
                    cooldownTimers.Remove(art);
                    initialCooldownTimes.Remove(art);
                }
            }
        }

        public void StartCooldown(InventoryItem art, float cooldownTime)
        {
            cooldownTimers[art] = cooldownTime;
            initialCooldownTimes[art] = cooldownTime;
        }

        public float GetTimeRemaining(InventoryItem art)
        {
            if(!cooldownTimers.ContainsKey(art))
            {
                return 0;
            }

            return cooldownTimers[art];
        }

        public float GetFractionRemaining(InventoryItem art)
        {
            if (art == null)
            {
                return 0;
            }

            if (!cooldownTimers.ContainsKey(art))
            {
                return 0;
            }

            return cooldownTimers[art] / initialCooldownTimes[art];
        }
    }
}

