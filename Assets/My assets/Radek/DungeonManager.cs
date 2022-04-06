using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class DungeonManager : MonoBehaviour
{
    private int numberOfRooms;
    private int numberOfRestRooms;
    private int numberOfFinalRooms;
    public int GetRooms() { return numberOfRooms; }

    public int GetRestRooms() { return numberOfRestRooms; }
    public int GetFinalRooms() { return numberOfFinalRooms; }



    private Transform dungeonEntrance;

    //TODO: prawdopodobienstwo na dana wielkosc lochu (maly/sredni/duzy)
    private int GenerateLength(int min, int max)
    {
        return Random.Range(min, max);
    }
    private void Start()
    {
        numberOfRooms = gameObject.transform.Find("Presets").Find("Rooms").childCount;
        numberOfFinalRooms = gameObject.transform.Find("Presets").Find("FinalRooms").childCount;
        numberOfRestRooms = gameObject.transform.Find("Presets").Find("RestRooms").childCount;

        GenerateDungeon[] tab = FindObjectsOfType<GenerateDungeon>();
        foreach (GenerateDungeon ob in tab)
        {
            ob.generateDungeon += generateDung;
        }
    }

    private void clearChild(GameObject transition)
    {
        foreach (Transform child in transition.transform)
        {
            Destroy(child.gameObject);
        }
    }


    private void generateDung(GameObject transition)
    {
        int length = GenerateLength(5, 7);

        int tmp;
        GameObject room;
        Vector3 transitionOffset;

        Vector3 transitionPos = transition.transform.position;

        clearChild(transition);

        room = gameObject.transform.Find("Presets").Find("Rooms").gameObject;
        for (int i = 0; i < length-2; i++)
        {
            //tmp = Random.Range(0, GetRooms());
            //room = gameObject.transform.Find("Presets").Find("Rooms").Find("Room" + tmp).gameObject;

            //GameObject obj = Instantiate(room, transition.transform);
            //obj.SetActive(true);
            //obj.name = "GeneratedRoom" + i;
            //transitionOffset = obj.transform.position - obj.transform.Find("Transition1").transform.position;



            //obj.transform.position = new Vector3(
            //    transitionPos.x + transitionOffset.x,
            //    transitionPos.y + transitionOffset.y,
            //    transitionPos.z + transitionOffset.z
            //    );

            //transitionPos = obj.transform.Find("Transition2").position;

            transitionPos = AppendRoom(
                room.transform.Find("Room" + Random.Range(0, GetRooms())).gameObject,
                transition, i, transitionPos);
            



        }

        //transitionPos = AppendRoom(
        //       room.transform.Find("Room" + Random.Range(0, GetRooms())).gameObject,
        //       transition, length-1, transitionPos);




        tmp = Random.Range(0, GetFinalRooms());
        room = gameObject.transform.Find("Presets").Find("FinalRooms").Find("Room" + tmp).gameObject;

        GameObject obj2 = Instantiate(room, transition.transform);
        obj2.SetActive(true);
        obj2.name = "GeneratedRoom" + (length-1);
        transitionOffset = obj2.transform.position - obj2.transform.Find("Transition1").transform.position;
        obj2.transform.position = new Vector3(
                transitionPos.x + transitionOffset.x,
                transitionPos.y + transitionOffset.y,
                transitionPos.z + transitionOffset.z
                );








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

        transitionPos = obj.transform.Find("Transition2").position;
        return transitionPos;
    }



}
