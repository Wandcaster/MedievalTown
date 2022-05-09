using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime;


public class DungeonManager : MonoBehaviour
{
    private int numberOfRooms;
    private int numberOfRestRooms;
    private int numberOfFinalRooms;
    private int numberOfDescendingRooms;
    private int numberOfDeadEndRooms;


    //Serialize it later in GenerateDungeon.cs; number of rooms where there's 0% to generate descendingRoom
    //currently Initiated in Awake()
    private int roomsWithoutDescendingArea;
    private int chanceToSpawnDescendingArea;//1/n.... later n--;

    //Delete it when you serialize it's contents in GenerateDungeon.cs
    private void Awake()
    {
        roomsWithoutDescendingArea = 5;
        chanceToSpawnDescendingArea = 5;
    }


    public int GetRooms() { return numberOfRooms; }

    public int GetRestRooms() { return numberOfRestRooms; }
    public int GetFinalRooms() { return numberOfFinalRooms; }

    public int GetDescendingRooms() { return numberOfDescendingRooms; }
    public int GetDeadEndRooms() { return numberOfDeadEndRooms; }

    private Transform dungeonEntrance;

    private int GenerateLength(DungeonGenerateChances chances)
    {
        int temp = UnityEngine.Random.Range(1, 100);
        if (temp < chances.small) return UnityEngine.Random.Range(chances.smallMin, chances.smallMax);
        if (temp < chances.medium) return UnityEngine.Random.Range(chances.mediumMin, chances.mediumMax);
        return UnityEngine.Random.Range(chances.bigMin, chances.bigMax);

    }
    private void Start()
    {
        numberOfRooms = gameObject.transform.Find("Presets").Find("Rooms").childCount;
        numberOfFinalRooms = gameObject.transform.Find("Presets").Find("FinalRooms").childCount;
        numberOfRestRooms = gameObject.transform.Find("Presets").Find("RestRooms").childCount;
        numberOfDescendingRooms = gameObject.transform.Find("Presets").Find("DescendingAreas").childCount;
        numberOfDeadEndRooms = gameObject.transform.Find("Presets").Find("DeadEndRooms").childCount;

        GenerateDungeon[] tab = FindObjectsOfType<GenerateDungeon>();
        foreach (GenerateDungeon ob in tab)
        {
            ob.generateDungeon += GenerateDung;
        }
        
    }

    private void clearChild(GameObject transition)
    {
        foreach (Transform child in transition.transform)
        {
            Destroy(child.gameObject);
        }
    }
    

    private void GenerateDung2(GameObject transition, int remainingLength, bool hasBoosRoom, GameObject room, bool clearParent)
    {
        Vector3 transitionPos = transition.transform.position;
        if(clearParent)clearChild(transition);
        int roomsToGenerateWithoutDescendingArea;
        if (remainingLength > roomsWithoutDescendingArea) roomsToGenerateWithoutDescendingArea = roomsWithoutDescendingArea;
        else roomsToGenerateWithoutDescendingArea = remainingLength;
        Debug.Log(remainingLength);
        
        int i;

        for (i=0; i < roomsToGenerateWithoutDescendingArea; i++)
        {
            transitionPos = AppendRoom(
            room.transform.Find("Room" + Random.Range(0, GetRooms())).gameObject,
            transition, i, transitionPos);

        }
        remainingLength -= roomsToGenerateWithoutDescendingArea;
        System.Random rand = new System.Random();

        do
        {

            float randomFloat = (float)rand.NextDouble();//[0;1]
            float chances = (float) 1 / chanceToSpawnDescendingArea;
            remainingLength--;

            //if true, then create descendingArea; can't do when remaining length is 1;
            if (remainingLength > 1)
            {
                //Debug.Log(randomFloat.ToString() + "<" + chances.ToString());
                if (randomFloat < chances)
                {
                    //descendArea
                    transitionPos = AppendRoom(
                  room.transform.parent.Find("DescendingAreas").Find("Room" + Random.Range(0, GetFinalRooms())).gameObject,
                  transition, i, transitionPos);
                    //odpal rekurencyjnie 2x
                    GameObject tmp = transition.transform.Find("GeneratedRoom" + i).gameObject;

                    int length1;
                    int length2;
                    if (remainingLength == 2) { length1 = 1;length1 = 1; }
                    else
                    {
                        length1 = UnityEngine.Random.Range(1, remainingLength-1);
                        length2 = remainingLength - length1;
                        Debug.Log(tmp.name);

                        GenerateDung2(tmp.transform.Find("Transition2").gameObject, length1, length1 >= length2, room, false);
                        GenerateDung2(tmp.transform.Find("Transition3").gameObject, length2, length1 < length2, room, false);



                        //Debug.Log("l1=" + length1.ToString() + ";l2=" + length2.ToString());
                    }

                    break;
                }
                else
                {
                    //Room
                    transitionPos = AppendRoom(
                room.transform.Find("Room" + Random.Range(0, GetRooms())).gameObject,
                transition, i, transitionPos);
                }

            }
            else if (remainingLength == 1)
            {
                //Room
                transitionPos = AppendRoom(
                room.transform.Find("Room" + Random.Range(0, GetRooms())).gameObject,
                transition, i, transitionPos);
            }
            else //remainingLength==0
            {
                if (hasBoosRoom)
                {
                    //restRoom and bossRoom
                    transitionPos = AppendRoom(
                           room.transform.parent.Find("RestRooms").Find("Room" + Random.Range(0, GetRestRooms())).gameObject,
                           transition, i++, transitionPos);


                    transitionPos = AppendRoom(
                   room.transform.parent.Find("FinalRooms").Find("Room" + Random.Range(0, GetFinalRooms())).gameObject,
                   transition, i, transitionPos);
                }
                else
                {
                    //deadEndRoom
                    transitionPos = AppendRoom(
                   room.transform.parent.Find("DeadEndRooms").Find("Room" + Random.Range(0, GetDeadEndRooms())).gameObject,
                   transition, remainingLength, transitionPos);
                }
                break;
            }






            chanceToSpawnDescendingArea--;
            i++;
        } while (remainingLength != 0);
    }



    private void GenerateDung(GameObject transition, DungeonGenerateChances chances)
    {
        int remainingLength = GenerateLength(chances);
        GenerateDung2(transition, remainingLength, true, gameObject.transform.Find("Presets").Find("Rooms").gameObject, true);
    }


    //[Tooltip("Returns new location Transition")]
    private Vector3 AppendRoom(GameObject room, GameObject parent, int generatedRoomID, Vector3 transitionPos)
    {
        //GameObject room = gameObject.transform.Find("Presets").Find("Rooms").Find("Room" + roomID).gameObject;

        GameObject obj = Instantiate(room, parent.transform);
        obj.SetActive(true);
        obj.name = "GeneratedRoom" + generatedRoomID;
        Vector3 transitionOffset = obj.transform.position - obj.transform.Find("Transition1").transform.position;



        obj.transform.position = new Vector3(
            transitionPos.x + transitionOffset.x,
            transitionPos.y + transitionOffset.y,
            transitionPos.z + transitionOffset.z
            );

        try
        {
            transitionPos = obj.transform.Find("Transition2").position;
        }
        catch { }

        return transitionPos;
    }



}
