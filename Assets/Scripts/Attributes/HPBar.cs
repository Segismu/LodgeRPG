using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] HP hpComponent = null;
        [SerializeField] RectTransform foreground = null;


        void Update()
        {
            foreground.localScale = new Vector3(hpComponent.GetFraction(), 1, 1);
        }
    }

}