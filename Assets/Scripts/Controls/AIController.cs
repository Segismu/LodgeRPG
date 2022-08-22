using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Controls
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fight fighter;
        GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fight>();
            player = GameObject.FindWithTag("Player");
        }


        private void Update()
        {
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }
}