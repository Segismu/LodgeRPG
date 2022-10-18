using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    [SerializeField] Quest[] tempQuests;
    [SerializeField] QuestItemUI questPrefab;

    void Start()
    {
        //foreach (Transform kiddo in GetComponentInChildren<Transform>())
        //{
        //    Destroy(kiddo.gameObject);
        //}

        transform.DetachChildren();


        foreach (Quest quest in tempQuests)
        {
            QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
            uiInstance.Setup(quest);
        }
    }
}
