using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "characters/LootData", order = 1)]
public class LootData : ScriptableObject
{
    [SerializeField] GameObject loot;
    [SerializeField] int amount;
    [SerializeField] float percentageToDrop;
    [SerializeField] Vector3 offset;

    public void Generate(Transform position)
    {
        float chance;



        for(int i = 0; i < amount; i++)
        {
            chance = UnityEngine.Random.Range(0.0f, 100.0f);
            if(chance<=percentageToDrop) Instantiate(loot, position.position + offset, position.rotation, null);
        }
    }
    
}
