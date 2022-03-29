using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillEnemyQuestData", menuName = "ScriptableObjects/Quest/KillEnemyQuestData", order = 1)]
public class KillEnemyQuestData : RepeatableQuest
{
    [SerializeField]
    public List<Record<GameObject, int>> enemyToKillList;
}
