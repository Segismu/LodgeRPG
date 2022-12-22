using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Controls;
using UnityEngine;

// When adding function to Somber, IRaycastable isnt an option,
// we should interact via proximity popups. 

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue = null;
        [SerializeField] string conversantName;

    public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null)
            {
                return false;
            }

            if (GetComponent<HP>().IsDead()) return false;

            // Remove this in Somber
            if (GetComponent<HP>().IsDead())
            {
                return true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue); ;
            }

            return true;
        }

        public string GetName()
        {
            return conversantName;
        }
    }
}
