using System.Collections.Generic;
using UnityEngine;

namespace RPG.Arts.Filters
{
    [CreateAssetMenu(fileName = "Tag Filter", menuName = "Arts/Filters/Tag", order = 0)]

    public class TagFilter : FilteringStrategy
    {
        [SerializeField] string tagToFilter = "";

        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (var gameObject in objectsToFilter)
            {
                if (gameObject.CompareTag(tagToFilter))
                {
                    yield return gameObject;
                }
            }
        }
    }
}