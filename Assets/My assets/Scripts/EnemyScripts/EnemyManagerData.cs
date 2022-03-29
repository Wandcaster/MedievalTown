using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnData
{    
    public GameObject enemyPrefab;
    public List<GameObject> activeEnemyList;
    public List<GameObject> deactiveEnemyList;
    public int maxCount;
}
