using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HPDisplay : MonoBehaviour
    {
        HP hp;

        private void Awake()
        {
            hp = GameObject.FindWithTag("Player").GetComponent<HP>();
        }

        private void Update()
        {
            GetComponent<Text>().text = string.Format("{0:0}%", hp.GetPercentage());
        }
    }
}

