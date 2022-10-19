using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG Project/Quest", order = 0)]

    public class Quest : ScriptableObject
    {
        [SerializeField] List<string> objetives = new List<string>();

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objetives.Count;
        }

        public IEnumerable<string> GetObjectives()
        {
            return objetives;
        }

        public bool HasObjective(string objective)
        {
            return objetives.Contains(objective);
        }
    }
}
