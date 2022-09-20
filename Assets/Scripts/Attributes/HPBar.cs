using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] HP hpComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;


        void Update()
        {
            if (Mathf.Approximately(hpComponent.GetFraction(), 0)
            || Mathf.Approximately(hpComponent.GetFraction(), 1))

            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;
            foreground.localScale = new Vector3(hpComponent.GetFraction(), 1, 1);
        }
    }

}