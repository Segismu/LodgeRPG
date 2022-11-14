using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Arts
{
    public class CooldownStore : MonoBehaviour
    {

        Dictionary<Art, float> cooldownTimers = new Dictionary<Art, float>();

        void Update()
        {
            var keys = new List<Art>(cooldownTimers.Keys);

            foreach (Art art in keys)
            {
                cooldownTimers[art] -= Time.deltaTime;

                if (cooldownTimers[art] < 0)
                {
                    cooldownTimers.Remove(art);
                }
            }
        }

        public void StartCooldown(Art art, float cooldownTime)
        {
            cooldownTimers[art] = cooldownTime;
        }

        public float GetTimeRemaining(Art art)
        {
            if(!cooldownTimers.ContainsKey(art))
            {
                return 0;
            }

            return cooldownTimers[art];
        }
    }
}

