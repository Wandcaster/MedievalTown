using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class BarController : MonoBehaviour
{
    [SerializeField]
    Image hp;
    [SerializeField]
    Image mp;
    private StatisticManager statisticManager;
    private void Start()
    {
        statisticManager = Player.instance.GetComponent<StatisticManager>();
    }
    void Update()
    {
        hp.fillAmount = Mathf.Clamp(statisticManager.hp / statisticManager.MaxHP(), 0, 1f);
        mp.fillAmount = Mathf.Clamp(statisticManager.mp / statisticManager.MaxMP(), 0, 1f);
    }
}
