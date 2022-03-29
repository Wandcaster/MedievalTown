using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyToRespawn;

    public IQuest quest;
    // Start is called before the first frame update
    void Start()
    {
       
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnemyManager.Instance.Respawn(7, 1, transform.position, 10, enemyToRespawn);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            QuestManager.Instance.FillQuestBoard();
        }
        
    }

}
