using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] Transform contentRoot;
        [SerializeField] GameObject buttonPrefab;

        private void OnEnable()
        {
            print("Loading save files.");

            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);
            }

            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
            Instantiate(buttonPrefab, contentRoot);
        }
    }
}
