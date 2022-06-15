using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int monsterID;
    [SerializeField] int amountMin;
    [SerializeField] int amountMax;

    bool respawned = false;
    private int GetRandomValue()
    {
        return UnityEngine.Random.Range(amountMin, amountMax+1);
    }


    [SerializeField]
    private Transform position;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("sad");
        if (respawned == false)
        {
            EnemyManager.Instance.Respawn(GetRandomValue(), 0, position.position, 2.5F, EnemyManager.Instance.EnemyPrefabList[monsterID]);
            Debug.Log("Respawn!");
            respawned = true;
        }

    }
}
