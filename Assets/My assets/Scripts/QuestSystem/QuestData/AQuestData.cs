using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AQuestData : ScriptableObject
{
    public string name;
    public int levelOfDifficulty;
    [TextArea]
    public string questInfo;
    public RewardData rewardData;
}
