using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Controls
{
    public class PlayerController : MonoBehaviour
    {
        HP hppoints;

        private void Start()
        {
            hppoints = GetComponent<HP>();
        }

        private void Update()
        {
            if (hppoints.IsDead()) return;

            if (CombatInteractor()) return;
            if (MoverInteractor()) return;
        }

        private bool CombatInteractor()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetCursorRay());
            foreach (RaycastHit hit in hits)
            {
                FightTarget target = hit.transform.GetComponent<FightTarget>();
                if (target == null) continue;

                if (!GetComponent<Fight>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fight>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool MoverInteractor()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetCursorRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetCursorRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
