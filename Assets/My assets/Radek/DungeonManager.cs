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
    private RoomNames roomNames;

    //Serialize it later in GenerateDungeon.cs; number of rooms where there's 0% to generate descendingRoom
    //currently Initiated in Awake()
    private int roomsWithoutDescendingArea;
    private int chanceToSpawnDescendingArea;//1/n.... later n--;

    //Delete it when you serialize it's contents in GenerateDungeon.cs
    private void Awake()
    {
        roomsWithoutDescendingArea = 0;
        chanceToSpawnDescendingArea = 1;
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
        roomNames = new RoomNames();
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

    private void ClearChild(GameObject transition)
    {
        foreach (Transform child in transition.transform)
        {
            Destroy(child.gameObject);
            
        }
        transition.transform.DetachChildren();//ta linijka nie ma sensu, ale aktualizuje referencje do gameObject'ow
                                              //przez co skrypt sie nie wywala jak natrafi na generowanie pokoju 
                                              //krotszego pokoju od poprzedniego
    }
    

    private void GenerateDungeon(GameObject transition, int remainingLength, bool hasBossRoom, GameObject room, bool clearParent)
    {
            //punkt przy³¹czenia, gdzie ma siê wygenerowaæ pokój
            Vector3 transitionPos = transition.transform.position;
            //liczba generowanych pokoi, bez mo¿liwoœci przy³¹czenia/generowania pokoju prowadz¹cego w dó³
            int roomsToGenerateWithoutDescendingArea;
            if (remainingLength > roomsWithoutDescendingArea) roomsToGenerateWithoutDescendingArea = roomsWithoutDescendingArea;
            else roomsToGenerateWithoutDescendingArea = remainingLength;
            //Debug.Log(remainingLength);

            int i=0;

        //generowanie pokoi bez przy³¹czania/generowania pokoju prowadz¹cego w dó³
            for (i = 0; i < roomsToGenerateWithoutDescendingArea; i++)
            {
                transitionPos = AppendRoom(
                room.transform.Find("Room" + Random.Range(0, GetRooms())).gameObject,
                transition, i, transitionPos);

            }
            remainingLength -= roomsToGenerateWithoutDescendingArea;

        //szansa na wygenerowanie pokoju prowadz¹cego w dó³, wzór: 1/n; n jest zmniejszane z ka¿d¹ iteracj¹
        System.Random rand = new System.Random();
        int chanceToSpawnDescendingAreaButLocal = chanceToSpawnDescendingArea;
        do
        {
            try
            {
                
                float randomFloat = (float)rand.NextDouble();//[0;1]
                float chances = (float)1 / chanceToSpawnDescendingAreaButLocal;
                remainingLength--;

                //generowanie lochu
                if (remainingLength > 1)
                {
                    //je¿eli ma nast¹piæ wygenerowanie pokoju to jest to sprawdzane, kiedy ma nast¹piæ zejœcie w dó³
                    if (randomFloat < chances)
                    {
                        //descendArea
                        transitionPos = AppendRoom(
                            room.transform.parent.Find("DescendingAreas").Find("Room" + Random.Range(0, GetFinalRooms())).gameObject,
                            transition, i, transitionPos);

                        GameObject tmp = transition.transform.Find("GeneratedRoom" + i).gameObject;

                        int length1;
                        int length2;
                        if (remainingLength == 2) { length1 = 1; length2 = 1; }
                        else
                        {
                            length1 = UnityEngine.Random.Range(1, remainingLength - 1);
                            length2 = remainingLength - length1;
                            //Debug.Log(tmp.name);
                        }
                        //Debug.Log(tmp.name+";"+(length1 >= length2).ToString()+";"+(!(length1 >= length2)).ToString());
                        GenerateDungeon(tmp.transform.Find("Transition2").gameObject, length1, hasBossRoom && length1 >= length2, room, false);
                        GenerateDungeon(tmp.transform.Find("Transition3").gameObject, length2, hasBossRoom && !(length1 >= length2), room, false);
                        remainingLength = 0;
                        break;
                        //Debug.Log("dzialam pomimo break'a");
                    }
                    //gdy nie nast¹pi generowanie zejœcia w dó³ to generowany jest normalny pokój
                    else
                    {
                        //Room
                        transitionPos = AppendRoom(
                    room.transform.Find("Room" + Random.Range(0, GetRooms())).gameObject,
                    transition, i, transitionPos);
                    }

                }
                //gdy ma zostaæ 1 pokój do wygenerowania to musi byæ to zwyk³y pokój - nie mo¿e byæ to pokój prowadz¹cy w dó³
                else if (remainingLength == 1)
                {
                    //Room
                    transitionPos = AppendRoom(
                    room.transform.Find("Room" + Random.Range(0, GetRooms())).gameObject,
                    transition, i, transitionPos);
                    //Debug.Log("To ja sie odpalam wiele razy");
                }
                //gdy pozosta³a d³ugoœæ lochu wynosi 0 to nastêpny pokój jest albo rest room z boss room'em, albo pokój bez wyjœcia
                else //remainingLength==0
                {
                    if (hasBossRoom)
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






                chanceToSpawnDescendingAreaButLocal--;
                i++;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                //Debug.Log("Bug2");
            }
        } while (remainingLength != 0) ;

    }



    private void GenerateDung(GameObject transition, DungeonGenerateChances chances)
    {
        ClearChild(transition);
        GenerateDungeon(transition, GenerateLength(chances), true, gameObject.transform.Find("Presets").Find("Rooms").gameObject, true);
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
