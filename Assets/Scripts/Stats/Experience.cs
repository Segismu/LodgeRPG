using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float expPoints = 0;

        public event Action onExperienceGained;
        
        public void GainExp(float exp)
        {
            expPoints += exp;
            onExperienceGained();
        }

        public float GetPoints()
        {
            return expPoints;
        }

        public object CaptureState()
        {
            return expPoints;
        }

        public void RestoreState(object state)
        {
            expPoints = (float)state;
        }
    }

}
