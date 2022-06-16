using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmgEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] EnemyController enemyController;

    //animator i enemyController podpiac do moba, ten skrypt dorzuca sie na bron


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && animator.GetCurrentAnimatorStateInfo(0).IsName("1handedAttack1") && other.GetComponent<StatisticManager>() != null)
        {
            Debug.Log(other.name);
            other.gameObject.GetComponent<StatisticManager>().DamageTaken(enemyController.currentPhysicalDMG, enemyController.currentMagicalDMG);
        }
    }
}
