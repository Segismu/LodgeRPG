using UnityEngine;

namespace RPG.Attributes

{
    //Energy consumed to perform arts actions.

    public class AE : MonoBehaviour
    {
        [SerializeField] float maxAE = 200;

        float ae;

        private void Awake()
        {
            ae = maxAE;
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