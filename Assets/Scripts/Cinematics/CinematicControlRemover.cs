using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Controls;

namespace RPG.Cinematics
{

    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableController;
            GetComponent<PlayableDirector>().stopped += EnableController;
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableController;
            GetComponent<PlayableDirector>().stopped -= EnableController;
        }

        void DisableController(PlayableDirector pd)
        {
            player.GetComponent<Scheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableController(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }

}