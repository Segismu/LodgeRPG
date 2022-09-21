using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using RPG.Attributes;
using GameDevTV.Utils;

namespace RPG.Controls
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float lookingForPlayerTime = 3f;
        [SerializeField] float aggroCooldown = 5f;
        [SerializeField] PatrolRoute patrolRoute;
        [SerializeField] float wayPointTolerance = 1f;
        [SerializeField] float wayPointDwellTime = 3f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] float shoutDistance = 4f;

        Fight fighter;
        HP hppoints;
        GameObject player;
        Mover mover;

        LazyValue<Vector3> guardPost;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWayPoint = Mathf.Infinity;
        float timeSinceAggro = Mathf.Infinity;
        int currentWayPointIndex = 0;

        private void Awake()
        {
            fighter = GetComponent<Fight>();
            hppoints = GetComponent<HP>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardPost = new LazyValue<Vector3>(GetGuardPosition);
        }

        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Start()
        {
            guardPost.ForceInit();
        }


        private void Update()
        {
            if (hppoints.IsDead()) return;

            if (isAggrevated() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < lookingForPlayerTime)
            {
                LookForPlayerBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        public void Aggro()
        {
            timeSinceAggro = 0;
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWayPoint += Time.deltaTime;
            timeSinceAggro += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPost = guardPost.value;

            if(patrolRoute != null)
            {
                if (AtWayPoint())
                {
                    timeSinceArrivedAtWayPoint = 0;
                    CycleWayPoint();
                }
                nextPost = GetCurrentWayPoint();
            }

            if(timeSinceArrivedAtWayPoint > wayPointDwellTime)
            {
                mover.StartMoveAction(nextPost, patrolSpeedFraction);
            }

        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointTolerance;
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolRoute.GetNextIndex(currentWayPointIndex);
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolRoute.GetWayPoint(currentWayPointIndex);
        }

        private void LookForPlayerBehaviour()
        {
            GetComponent<Scheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);

            AggroNearbyEnemies();
        }

        private void AggroNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);

            foreach (RaycastHit hit in hits)
            {
                AIController ai = hit.collider.GetComponent<AIController>();
                if (ai == null) continue;

                ai.Aggro();
            }
        }

        private bool isAggrevated()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance || timeSinceAggro < aggroCooldown;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}