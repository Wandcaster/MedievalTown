using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRespawnOnEnter : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyToRespawn;
    private Vector3 position;
    private bool isDone = false;
    [SerializeField]
    private bool repeat=false;
    [SerializeField]
    private int EnemyCount=1;
    [SerializeField]
    private int respawnFrequency=1;
    [SerializeField]
    private int respawnRadius = 1;
    bool isTouch = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        position = transform.position;
        if(!isTouch)
        {
            isTouch = true;
            if (repeat == false)
            {
                if (isDone == false)
                {
                    EnemyManager.Instance.Respawn(EnemyCount, respawnFrequency, position, respawnRadius, enemyToRespawn);
                    isDone = true;
                }
            }
            else
            {
                EnemyManager.Instance.Respawn(EnemyCount, respawnFrequency, position, respawnRadius, enemyToRespawn);
                isDone = true;
            }
//            Debug.Log("dziala");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isTouch = false;
    }
}
