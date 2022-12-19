using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] Transform respawnLocation;
        [SerializeField] float respawnDelay = 2;
        [SerializeField] float fadeTime = 0.2f;
        [SerializeField] float hpRegenPercentaje = 30;

        private void Awake()
        {
            GetComponent<HP>().onDie.AddListener(Respawn);
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(respawnDelay);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);
            GetComponent<NavMeshAgent>().Warp(respawnLocation.position);
            HP hp = GetComponent<HP>();
            hp.Heal(hp.GetMaxHpPoints() * hpRegenPercentaje / 100);
            yield return fader.FadeIn(fadeTime);

        }
    }

}