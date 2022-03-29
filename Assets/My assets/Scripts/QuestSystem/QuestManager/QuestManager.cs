using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    public static QuestManager Instance { get { return _instance; } }

    public List<IQuest> activeQuestList; //Zaakceptowane zadania przez gracza
    public List<IQuest> onBoardQuestList; //Zadania dodane do tablic z zadaniami
    public List<AQuestData> availableQuestList; //Dostêpne zadania które nie znajduj¹ siê jeszcze na tablicach 
    public List<IQuest> finishQuestList; // Ukoñczone zadania przez gracza
    public List<GameObject> boardList; // Lista tablic z zadaniami wystêpujacych w grze

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void ActiveQuest(KillEnemyQuest quest)
    {
        activeQuestList.Add(quest);
        onBoardQuestList.Remove(quest);
        foreach (var item in quest.killEnemyQuestData.enemyToKillList)
        {
            EnemyManager.Instance.SetEnemyDieEvent(item.key.GetComponent<EnemyController>(),quest);
        }
    }
    public void FinishQuest(KillEnemyQuest quest)
    {
        finishQuestList.Add(quest);
        activeQuestList.Remove(quest);
        foreach (var item in quest.killEnemyQuestData.enemyToKillList)
        {
            EnemyManager.Instance.ClearEnemyDieEvent(item.key.GetComponent<EnemyController>(), quest);
        }
        quest.gameObject.SetActive(false);
    }

    public void AddQuestToAvailableQuestList(AQuestData data)
    {
        availableQuestList.Add(data);
    }

    public void FillQuestBoard()
    {
        foreach (GameObject board in boardList)
        {
            IQuest[] questList = board.GetComponentsInChildren<IQuest>();
            for(int i=0;i<questList.Length;i++)
            {
                if(questList[i].questData==null)
                {
                    questList[i].questData = availableQuestList[0];
                    questList[i].Initialize();
                    onBoardQuestList.Add(questList[i]);
                    availableQuestList.Remove(availableQuestList[0]);
                }
            }
        }
    }

    private void Start()
    {
        FillQuestBoard();
    }
}
