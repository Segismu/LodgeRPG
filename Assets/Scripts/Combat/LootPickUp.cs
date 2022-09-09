using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{

    public class LootPickUp : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 4;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fight>().EquipWeapon(weapon);
                StartCoroutine(RespawnInSeconds(respawnTime));
            }
        }

        private IEnumerator RespawnInSeconds(float seconds)
        {
            SpawnLoot(false);
            yield return new WaitForSeconds(seconds);
            SpawnLoot(true);

        }

        private void SpawnLoot(bool shouldSpawn)
        {
            GetComponent<Collider>().enabled = shouldSpawn;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldSpawn);
            }
        }
    }

}
