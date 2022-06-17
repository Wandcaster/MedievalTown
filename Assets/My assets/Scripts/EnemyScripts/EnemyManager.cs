using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> EnemyPrefabList = new List<GameObject>();
    [SerializeField]
    private List<EnemyManagerData> enemyList;

    private static EnemyManager _instance;
    public static EnemyManager Instance { get { return _instance; } }

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
        Object.DontDestroyOnLoad(this);

    }
    public void Respawn(int enemyCount, int respawnFrequency, Vector3 respawnPosition, float respawnRadius, GameObject enemyPrefab)
    {
        EnemyManagerData enemyFromList = FindEnemyManagerData(enemyPrefab);
        if (enemyFromList != null)
        {
            StartCoroutine(Spawn(enemyCount, respawnFrequency, respawnPosition, respawnRadius, enemyFromList));
        }
    }
    private EnemyManagerData FindEnemyManagerData(GameObject enemy)
    {
        foreach (EnemyManagerData enemyFromList in enemyList)
        {
            if (enemyFromList.enemyPrefab == enemy.gameObject)
            {
                return enemyFromList;
            }
        }
        return null;
    }
    private IEnumerator Spawn(int enemyToRespawn, int respawnFrequency, Vector3 respawnPosition, float respawnRadius, EnemyManagerData enemyData)
    {
        for (int count = 0; count < enemyToRespawn; count++)
        {
                try
                {
                    Vector3 newPoint = Random.insideUnitSphere * respawnRadius + respawnPosition;
                    NavMesh.SamplePosition(newPoint, out NavMeshHit hit, 20, 1);
                    EnemyController enemy = enemyData.MoveFormDeactiveToActive();
                    if (enemy == null)  break;
                    enemy.transform.position = hit.position;
                    enemy.gameObject.SetActive(true);
                    ActivateEnemy(enemy);
                }
                catch (System.Exception e)
                {
                Debug.Log(e);
                    count--;
                }
                yield return new WaitForSeconds(respawnFrequency);            
        }
    }
    void Start()
    {
        GenerateEnemy();
        FillEnemyPrefabList();
    }
    private void GenerateEnemy()
    {
        GameObject tempEnemy=null;
        for(int i =0; i<enemyList.Count;i++)
        {
            for(int enemyCount=0; enemyCount<enemyList[i].maxCount;enemyCount++)
            {
                tempEnemy = Instantiate(enemyList[i].enemyPrefab, transform.position, Quaternion.identity, transform);
                tempEnemy.SetActive(false);
                enemyList[i].deactiveEnemyList.Add(tempEnemy.GetComponent<EnemyController>()) ;
            }
        }
    }
    private void FillEnemyPrefabList()
    {
        EnemyPrefabList.Clear();
        foreach (EnemyManagerData enemyFromList in enemyList)
        {
            EnemyPrefabList.Add(enemyFromList.enemyPrefab);
        }
    }
    public void ActivateEnemy(EnemyController enemy)
    {
        enemy.OnDeath += DeactivateEnemy;
    }
    public void DeactivateEnemy(GameObject enemyType,EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
        FindEnemyManagerData(enemyType).MoveFromActiveToDeactive(enemy);
    }
    public void SetEnemyDieEvent(EnemyController enemyController, IQuest quest)
    {
        EnemyManagerData data=FindEnemyManagerData(enemyController.gameObject);
        data.Die += quest.UpdateProgress;
    }
    public void ClearEnemyDieEvent(EnemyController enemyController, IQuest quest)
    {
        EnemyManagerData data = FindEnemyManagerData(enemyController.gameObject);
        data.Die -= quest.UpdateProgress;
    }
}
    