using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Arts.Effects
{
    [CreateAssetMenu(fileName = "Spawn Target Prefab Effect", menuName = "Arts/Effects/Effect Prefab")]
    public class SpawnTargetPrefabEffect : EffectStrategy
    {
        [SerializeField] Transform prefabToSpawn;
        [SerializeField] float destroyDelay = -1;

        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(Effect(data, finished));
        }

        private IEnumerator Effect(AbilityData data, Action finished)
        {
            Transform instance = Instantiate(prefabToSpawn);
            instance.position = data.GetTargetedPoint();

            if (destroyDelay > 0)
            {
                yield return new WaitForSeconds(destroyDelay);
                Destroy(instance.gameObject);
            }
            finished();
        }
    }
}