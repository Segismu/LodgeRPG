using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Controls
{
    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
            if (CombatInteractor()) return;
            if (MoverInteractor()) return;
        }

        private bool CombatInteractor()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetCursorRay());
            foreach (RaycastHit hit in hits)
            {
                FightTarget target = hit.transform.GetComponent<FightTarget>();
                if (!GetComponent<Fight>().CanAttack(target))
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fight>().Attack(target);
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
                    GetComponent<Mover>().StartMoveAction(hit.point);
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
