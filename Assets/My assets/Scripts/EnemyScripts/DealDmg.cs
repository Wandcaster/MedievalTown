using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmg : MonoBehaviour
{
    [SerializeField]
    public int dmg;
    [SerializeField]
    int dmgDealed=0;
    [SerializeField]
    int MaxDmgToDeal=10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            other.GetComponent<EnemyController>().currentHealth -= dmg;
            dmgDealed += dmg;
            if (dmgDealed >= MaxDmgToDeal) GetComponent<DestroyToPieces>().DestroyObject();
        }
    }
}
