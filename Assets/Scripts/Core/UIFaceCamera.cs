using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class UIFaceCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
