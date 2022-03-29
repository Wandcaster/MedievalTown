using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class KillEnemyQuest : IQuest
{
    private int enemyToKillCount;
    private float valueOfOneEnemy;
    public KillEnemyQuestData killEnemyQuestData;
    public override void  EndQuest()
    {
        print(questData.questInfo+"End Quest");
        IsActive = false;
        QuestManager.Instance.FinishQuest(this);
        Reward();
    }
    public override void Reward()
    {
        Debug.Log("Nagroda!");
        foreach (var item in questData.rewardData.RewardItems)
        {
            for(int i=0;i<item.value;i++)Instantiate(item.key, questData.rewardData.position, Quaternion.Euler(questData.rewardData.rotation));
        }

    }
    public override void StartQuest()
    {
        print(questData.questInfo);
        IsActive = true;
        QuestManager.Instance.ActiveQuest(this);
    }
    public override void Initialize()
    {
        killEnemyQuestData = (KillEnemyQuestData)questData;
        title.text = questData.name;
        description.text = questData.questInfo;
        difficulty.text = questData.levelOfDifficulty.ToString();
        foreach (var item in killEnemyQuestData.enemyToKillList)
        {
            enemyToKillCount += item.value;
        }
        valueOfOneEnemy = 1f / enemyToKillCount;
        Debug.Log(enemyToKillCount + " " + valueOfOneEnemy);
    }
    public override void UpdateProgress()
    {
        if (progress + valueOfOneEnemy<=1.1F) progress += valueOfOneEnemy;
    }

    private void Start()
    {
        GetComponent<Rigidbody>().Sleep();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartQuest();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            UpdateProgress();
        }
    }
}
