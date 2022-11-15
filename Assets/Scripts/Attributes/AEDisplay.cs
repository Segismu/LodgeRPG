using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class AEDisplay : MonoBehaviour
    {
        AE ae;

        private void Awake()
        {
            ae = GameObject.FindWithTag("Player").GetComponent<AE>();
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", ae.GetAE(), ae.GetMaxAE());
        }
    }
}

