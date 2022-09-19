using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.TextDamage
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] TextDamage damageTextPrefab = null;

        public void Spawn (float damageAmount)
        {
            TextDamage instance = Instantiate<TextDamage>(damageTextPrefab, transform);
            instance.SetValue(damageAmount);
        }
    }

}