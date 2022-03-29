using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardData", menuName = "ScriptableObjects/Quest/RewardData", order = 1)]
public class RewardData : ScriptableObject
{
    public List<Record<GameObject, int>> RewardItems;
    public Vector3 position;
    public Vector3 rotation;
}
