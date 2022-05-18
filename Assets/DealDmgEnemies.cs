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

        if (other.tag == "Player" && animator.GetBool("isAttacking"))
        {
            other.GetComponent<StatisticManager>().DamageTaken(enemyController.currentPhysicalDMG, enemyController.currentMagicalDMG);
        }
    }

}
