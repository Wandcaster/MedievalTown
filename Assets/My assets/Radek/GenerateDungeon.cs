using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDungeon : MonoBehaviour
{
    [SerializeField] GameObject transition;
    public delegate void generate(GameObject transition);
    public event generate generateDungeon;
    private void Update()
    {
        //GenerateDungeon(transition);
        if (Input.GetKeyUp(KeyCode.Z))
        {
            generateDungeon(transition);
        }
    }






}
