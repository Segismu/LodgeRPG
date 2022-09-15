using System;
using UnityEngine;
using UnityEngine.UI;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHPDisplay : MonoBehaviour
    {
        Fight fight;

        private void Awake()
        {
            fight = GameObject.FindWithTag("Player").GetComponent<Fight>();
        }

        private void Update()
        {
            if (fight.GetTarget() == null)
            {
                GetComponent<Text>().text = "N/A";
                return;
            }

            HP health = fight.GetTarget();
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHpPoints(), health.GetMaxHpPoints());
        }
    }
}

