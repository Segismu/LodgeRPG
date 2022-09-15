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
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", hp.GetHpPoints(), hp.GetMaxHpPoints());
        }
    }
}

