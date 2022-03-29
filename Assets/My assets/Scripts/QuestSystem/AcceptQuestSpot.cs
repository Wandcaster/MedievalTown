using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AcceptQuestSpot : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        IQuest quest = other.GetComponent<IQuest>();
        if (quest == null) return;
        if(quest.IsActive)
        {
            if(quest.progress>=1)
            {
                other.GetComponent<Throwable>().interactable.attachedToHand.DetachObject(other.gameObject);
                quest.EndQuest();
            }
        }
        else
        {
           quest.StartQuest();
        }
    }
}
