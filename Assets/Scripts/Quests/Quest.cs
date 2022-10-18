using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG Project/Quest", order = 0)]

    public class Quest : ScriptableObject
    {
        [SerializeField] string[] objetives;

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objetives.Length;
        }

        public IEnumerable<string> GetObjectives()
        {
            return objetives;
        }
    }
}
