using GameDevTV.Saving;
using GameDevTV.Utils;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes

{
    //Energy consumed to perform arts actions.

    public class AE : MonoBehaviour, ISaveable
    {
        LazyValue<float> ae;

        private void Awake()
        {
            ae = new LazyValue<float>(GetMaxAE);
        }

        private void Update()
        {
            if (ae.value < GetMaxAE())
            {
                ae.value += GetAERegenRate() * Time.deltaTime;

                if (ae.value > GetMaxAE())
                {
                    ae.value = GetMaxAE();
                }
            }
        }

        public float GetAE()
        {
            return ae.value;
        }

        public float GetAERegenRate()
        {
            return GetComponent<BaseStats>().GetStat(Stat.aeRegen);
        }

        public float GetMaxAE()
        {
            return GetComponent<BaseStats>().GetStat(Stat.AE);
        }

        public bool UseAE(float aeToUse)
        {
            if (aeToUse > ae.value)
            {
                return false;
            }

            ae.value -= aeToUse;
            return true;
        }

        public object CaptureState()
        {
            return ae.value;
        }

        public void RestoreState(object state)
        {
            ae.value = (float) state;
        }
    }
}