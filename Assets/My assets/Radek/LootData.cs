using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootData : MonoBehaviour
{
    [SerializeField] GameObject loot;
    [SerializeField] int amount;
    [SerializeField] float percentageToDrop;

    public void Generate(Transform position)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(loot, position);
        }



    }
    
}
