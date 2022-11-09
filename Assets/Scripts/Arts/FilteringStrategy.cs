using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Arts
{
    public abstract class FilteringStrategy : ScriptableObject
    {
        public abstract IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter);
    }
}

