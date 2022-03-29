using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerData : ScriptableObject
{
    public GameObject QuestPrefab;
    public List<IQuest> activeQuestList;
    public List<IQuest> deactiveQuestList;


    public void MoveFromActiveToDeactive(IQuest quest)
    {
        if (activeQuestList.Count != 0)
        {
            deactiveQuestList.Add(quest);
            activeQuestList.Remove(activeQuestList.Find(x => x.GetInstanceID() == quest.GetInstanceID()));
        }
        else
        {
            Debug.LogWarning("ActiveEnemyList is empty");
        }
    }
    public IQuest MoveFormDeactiveToActive()
    {
        if (deactiveQuestList.Count != 0)
        {
            IQuest quest = deactiveQuestList[0];
            activeQuestList.Add(quest);
            deactiveQuestList.Remove(quest);
            return quest;
        }
        else
        {
            Debug.LogWarning("DeactiveEnemyList is empty");
            return null;
        }
    }
}
