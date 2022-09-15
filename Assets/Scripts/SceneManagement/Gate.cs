using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Gate : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }   
        }

        private IEnumerator Transition()
        {

            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load is not set.");
                yield break;
            }


            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            wrapper.Load();

            Gate otherGate = GetOtherGate();
            UpadatePlayer(otherGate);

            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpadatePlayer(Gate otherGate)
        {
            GameObject player = GameObject.FindWithTag("Player");

            player.transform.position = otherGate.spawnPoint.position;
            player.transform.rotation = otherGate.spawnPoint.rotation;

        }

        private Gate GetOtherGate()
        {
            foreach (Gate gate in FindObjectsOfType<Gate>())
            {
                if (gate == this) continue;
                if (gate.destination != destination) continue;

                return gate;
            }

            return null;
        }
    }
}
