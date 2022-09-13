
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float expPoints = 0;

        public void GainExp(float exp)
        {
            expPoints += exp;
        }
    }

}
