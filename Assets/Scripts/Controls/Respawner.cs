using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using RPG.Attributes;
using RPG.Controls;
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
        [SerializeField] float enemyHPRegenPercentage = 30;

        private void Awake()
        {
            GetComponent<HP>().onDie.AddListener(Respawn);
        }

        void Start()
        {
            if (GetComponent<HP>().IsDead())
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
            yield return new WaitForSeconds(respawnDelay);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);
            RespawnPlayer();
            ResetEnemies();
            savingWrapper.Save();
            yield return fader.FadeIn(fadeTime);

        }

        private void ResetEnemies()
        {
            foreach (AIController enemyControllers  in FindObjectsOfType<AIController>())
            {
                enemyControllers.Reset();
                HP hp = enemyControllers.GetComponent<HP>();

                if (hp && !hp.IsDead())
                {
                    hp.Heal(hp.GetMaxHpPoints() * enemyHPRegenPercentage / 100);
                }
            }
        }

        private void RespawnPlayer()
        {
            Vector3 positionDelta = respawnLocation.position - transform.position;
            GetComponent<NavMeshAgent>().Warp(respawnLocation.position);
            HP hp = GetComponent<HP>();
            hp.Heal(hp.GetMaxHpPoints() * hpRegenPercentaje / 100);
            ICinemachineCamera activeVirtualCamera = FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;
            if (activeVirtualCamera.Follow == transform)
            {
                activeVirtualCamera.OnTargetObjectWarped(transform, positionDelta);
            }
        }
    }

}