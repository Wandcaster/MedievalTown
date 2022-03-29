using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusWindow : MonoBehaviour
{
    [SerializeField]
    private UIActivation uiActivation;
    string text;
    [SerializeField]
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        uiActivation.openStatusWindow += RefreshActiveQuestList;
    }

    // Update is called once per frame
   public void RefreshActiveQuestList()
    {
        textMesh.text = "";
        foreach (var quest in QuestManager.Instance.activeQuestList)
        {
            textMesh.text += quest.name +" "+quest.progress+ System.Environment.NewLine;
        }
        Debug.Log(textMesh.text);
    }
}
