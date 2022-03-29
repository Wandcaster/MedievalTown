using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class EnemyManagerData
{    
    public GameObject enemyPrefab;
    public List<EnemyController> activeEnemyList;
    public List<EnemyController> deactiveEnemyList;
    public int maxCount;

    public delegate void EnemyDie();
    public event EnemyDie Die;

    public void MoveFromActiveToDeactive(EnemyController enemyController)
    {
        if(activeEnemyList.Count!=0)
        {
            deactiveEnemyList.Add(enemyController);
            activeEnemyList.Remove(activeEnemyList.Find(x => x.GetInstanceID() == enemyController.GetInstanceID()));
            Die?.Invoke();
        }
        else
        {
            Debug.LogWarning("ActiveEnemyList is empty");
        }
    }
    public EnemyController MoveFormDeactiveToActive()
    {
        if(deactiveEnemyList.Count!=0)
        {
            EnemyController enemy = deactiveEnemyList[0];
            activeEnemyList.Add(enemy);
            deactiveEnemyList.Remove(enemy);
            return enemy;
        }
        else
        {
            Debug.LogWarning("DeactiveEnemyList is empty");
            return null;
        }
    }
}
