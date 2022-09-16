using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Controls;
using UnityEngine;

namespace RPG.Combat
{

    public class LootPickUp : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 4;



        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Pickup(other.GetComponent<Fight>());
            }
        }

        private void Pickup(Fight fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(RespawnInSeconds(respawnTime));
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

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.GetComponent<Fight>());
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}