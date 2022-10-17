using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Controls;

namespace RPG.Combat
{
    [RequireComponent(typeof(HP))]

    public class FightTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (!enabled) return false;

            if (!callingController.GetComponent<Fight>().CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                callingController.GetComponent<Fight>().Attack(gameObject);
            }
            return true;
        }
    }
}