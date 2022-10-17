using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class AggroGroup : MonoBehaviour
    {
        [SerializeField] Fight[] fighters;
        [SerializeField] bool activateOnStart = false;

        private void Start()
        {
            Activate(activateOnStart);
        }

        public void Activate(bool shouldActivate)
        {
            foreach (Fight fighter in fighters)
            {
                FightTarget target = fighter.GetComponent<FightTarget>();
                if (target != null)
                {
                    target.enabled = shouldActivate;
                }

                fighter.enabled = shouldActivate;
            }
        }
    }
}
