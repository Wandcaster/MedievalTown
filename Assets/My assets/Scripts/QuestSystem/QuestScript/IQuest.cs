using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class IQuest:MonoBehaviour
{
    public abstract void StartQuest();
    public abstract void EndQuest();
    public  abstract void Reward();
    public  abstract void Initialize();
    public abstract void UpdateProgress();
    public AQuestData questData;
    public bool IsActive;
    [Range(0, 1)]
    public float progress = 0;
    [SerializeField]
    public TextMeshProUGUI title;
    [SerializeField]
    public TextMeshProUGUI description;
    [SerializeField]
    public TextMeshProUGUI difficulty;

}
