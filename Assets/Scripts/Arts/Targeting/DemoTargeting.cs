using UnityEngine;

namespace RPG.Arts.Targeting
{
    [CreateAssetMenu(fileName = "Demo Targeting", menuName = "Arts/Targeting/Demo", order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting(GameObject user)
        {
            Debug.Log("Demo targeting Starts");
        }
    }
}

