using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDungeon : MonoBehaviour
{
    [SerializeField] GameObject transition;
    [Tooltip("Chances to generate small dungeon")]
    [SerializeField] int smallDungeonChances;
    [Tooltip("Chances to generate medium dungeon")]
    [SerializeField] int mediumDungeonChances;


    //TODO: prevent from changing array size
    //[Tooltip("Small dungeon size")]

    [SerializeField] int smallDungeonSizeMin;
    [SerializeField] int smallDungeonSizeMax;


    //[Tooltip("Medium dungeon size")]
    [SerializeField] int mediumDungeonSizeMin;
    [SerializeField] int mediumDungeonSizeMax;

    //[Tooltip("Big dungeon size")]
    [SerializeField] int bigDungeonSizeMin;
    [SerializeField] int bigDungeonSizeMax;



    public delegate void generate(GameObject transition, DungeonGenerateChances rng);
    public event generate generateDungeon;
    private void Update()
    {
        //GenerateDungeon(transition);
        if (Input.GetKeyUp(KeyCode.Z))
        {

            generateDungeon(transition, new DungeonGenerateChances(
                smallDungeonSizeMin, smallDungeonSizeMax+1, 
                mediumDungeonSizeMin, mediumDungeonSizeMax+1,
                bigDungeonSizeMin, bigDungeonSizeMax+1,
                smallDungeonChances, mediumDungeonChances
                
                
                ));
        }
    }






}
