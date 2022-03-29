using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyModel : MonoBehaviour
{
    [SerializeField]
    EnemyData enemyData;
    public float detectionRadius { get { return enemyData.detectionRadius; }  }
    public float stoppingDistance{ get { return enemyData.stoppingDistance; } }
    public float attackRange { get { return enemyData.attackRange; } }
    public LayerMask layerMask { get { return enemyData.layerMask; } }
    public Transform playerTransform { get { return enemyData.playerTransform; } set { enemyData.playerTransform = value; } }
    public GameObject prefab { get { return enemyData.prefab; } }

    public float maxHealth { get { return enemyData.maxHealth; } }


}
