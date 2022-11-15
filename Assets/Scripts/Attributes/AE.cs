using UnityEngine;

namespace RPG.Attributes

{
    //Energy consumed to perform arts actions.

    public class AE : MonoBehaviour
    {
        [SerializeField] float maxAE = 200;
        [SerializeField] float aeRegenRate = 5;

        float ae;

        private void Awake()
        {
            ae = maxAE;
        }

        private void Update()
        {
            if (ae < maxAE)
            {
                ae += aeRegenRate * Time.deltaTime;

                if (ae > maxAE)
                {
                    ae = maxAE;
                }
            }
        }

        public float GetAE()
        {
            return ae;
        }

        public float GetMaxAE()
        {
            return maxAE;
        }

        public bool UseAE(float aeToUse)
        {
            if (aeToUse > ae)
            {
                return false;
            }

            ae -= aeToUse;
            return true;
        }
    }
}