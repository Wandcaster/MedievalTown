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
        roomsWithoutDescendingArea = 3;
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
        roomNames = new RoomNames();
        numberOfRooms = gameObject.transform.Find(roomNames.GetPresets()).Find("Rooms").childCount;
        numberOfFinalRooms = gameObject.transform.Find(roomNames.GetPresets()).Find(roomNames.GetFinalRooms()).childCount;
        numberOfRestRooms = gameObject.transform.Find(roomNames.GetPresets()).Find(roomNames.GetRestRooms()).childCount;
        numberOfDescendingRooms = gameObject.transform.Find(roomNames.GetPresets()).Find(roomNames.GetDescendingAreas()).childCount;
        numberOfDeadEndRooms = gameObject.transform.Find(roomNames.GetPresets()).Find(roomNames.GetDeadEndRooms()).childCount;
        

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
            //punkt przy��czenia, gdzie ma si� wygenerowa� pok�j
            Vector3 transitionPos = transition.transform.position;
            //liczba generowanych pokoi, bez mo�liwo�ci przy��czenia/generowania pokoju prowadz�cego w d�
            int roomsToGenerateWithoutDescendingArea;
            if (remainingLength > roomsWithoutDescendingArea) roomsToGenerateWithoutDescendingArea = roomsWithoutDescendingArea;
            else roomsToGenerateWithoutDescendingArea = remainingLength;
            //Debug.Log(remainingLength);

            int i=0;
        //List<GameObject> availableRooms = GetAvailableRooms(room, transition);
        //for (int j = 0; j < availableRooms.Count; j++) Debug.Log(availableRooms[j].name);

        //Debug.Break();
        List<GameObject> availableRooms;


        //generowanie pokoi bez przy��czania/generowania pokoju prowadz�cego w d�
            for (i = 0; i < roomsToGenerateWithoutDescendingArea; i++)
            {
                //availableRooms = GetAvailableRooms(room, transition);
                availableRooms = GetAvailableRooms(room, transition, transitionPos);
                if (availableRooms.Count != 0) transitionPos = AppendRoom(availableRooms, transition, i, transitionPos);
                else
                {
                //przerywa wykonywanie
                    return;
                }
            }
        remainingLength -= roomsToGenerateWithoutDescendingArea;

        //szansa na wygenerowanie pokoju prowadz�cego w d�, wz�r: 1/n; n jest zmniejszane z ka�d� iteracj�
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
                    //je�eli ma nast�pi� wygenerowanie pokoju to jest to sprawdzane, kiedy ma nast�pi� zej�cie w d�
                    if (randomFloat < chances)
                    {
                        //GetAvailableRooms(room.transform.parent.Find(roomNames.GetDescendingAreas()), transition);
                        //descendArea
                        transitionPos = AppendRoom(
                             GetAvailableRooms(room.transform.parent.Find(roomNames.GetDescendingAreas()).gameObject, transition, transitionPos),
                            transition, i, transitionPos);

                        GameObject tmp = transition.transform.Find(roomNames.GetGeneratedRoom() + i).gameObject;

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
                        //pokoj z boss room'em ma priorytet - czyli tam, gdzie jest dalej w lochu
                            if (length1 >= length2)
                            {
                                GenerateDungeon(tmp.transform.Find(roomNames.GetTransition2()).gameObject, length1, hasBossRoom && length1 >= length2, room, false);
                                GenerateDungeon(tmp.transform.Find(roomNames.GetTransition3()).gameObject, length2, hasBossRoom && !(length1 >= length2), room, false);
                                remainingLength = 0;
                            }
                            else
                            {
                            GenerateDungeon(tmp.transform.Find(roomNames.GetTransition3()).gameObject, length2, hasBossRoom && !(length1 >= length2), room, false);
                            GenerateDungeon(tmp.transform.Find(roomNames.GetTransition2()).gameObject, length1, hasBossRoom && length1 >= length2, room, false);
                            remainingLength = 0;

                        }



                        break;
                    }
                    //gdy nie nast�pi generowanie zej�cia w d� to generowany jest normalny pok�j
                    else
                    {
                        //Room
                        transitionPos = AppendRoom(
                    GetAvailableRooms(room, transition, transitionPos),
                    transition, i, transitionPos);
                    }

                }
                //gdy ma zosta� 1 pok�j do wygenerowania to musi by� to zwyk�y pok�j - nie mo�e by� to pok�j prowadz�cy w d�
                else if (remainingLength == 1)
                {
                    //Room
                    transitionPos = AppendRoom(
                    GetAvailableRooms(room, transition, transitionPos),
                    transition, i, transitionPos);
                }
                //gdy pozosta�a d�ugo�� lochu wynosi 0 to nast�pny pok�j jest albo rest room z boss room'em, albo pok�j bez wyj�cia
                else //remainingLength==0
                {
                    if (hasBossRoom)
                    {
                        //restRoom and bossRoom
                        //GetAvailableRooms(room.transform.parent.Find(roomNames.GetRestRooms()).gameObject)
                        transitionPos = AppendRoom(
                               GetAvailableRooms(room.transform.parent.Find(roomNames.GetRestRooms()).gameObject, transition, transitionPos),
                               transition, i++, transitionPos);


                        transitionPos = AppendRoom(
                       GetAvailableRooms(room.transform.parent.Find(roomNames.GetFinalRooms()).gameObject, transition, transitionPos),
                       transition, i, transitionPos);
                    }
                    else
                    {
                        //deadEndRoom
                        transitionPos = AppendRoom(
                       GetAvailableRooms(room.transform.parent.Find(roomNames.GetDeadEndRooms()).gameObject, transition, transitionPos),
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

    private List<GameObject> GetAvailableRooms(GameObject parentFolder, GameObject parent, Vector3 transitionPos)
    {
        List<GameObject> output = new List<GameObject>();
        //Debug.Log("childCount " + parentFolder.transform.childCount);
        for(int i=0; i < parentFolder.transform.childCount; i++)
        {
            if (CheckRoom(parentFolder.transform.GetChild(i).gameObject, transitionPos, parent))
            {
                output.Add(parentFolder.transform.GetChild(i).gameObject);
            }
            //output.Add(parentFolder.transform.GetChild(i).gameObject);
        }
        //if(output.Count==0)
        //{
        //    Debug.Log("Count0");
        //}
        return output;
    }





    //[Tooltip("Returns new location Transition")]
    private Vector3 AppendRoom(List<GameObject> availableRooms, GameObject parent, int generatedRoomID, Vector3 transitionPos)
    {
        //GameObject room = gameObject.transform.Find("Presets").Find("Rooms").Find("Room" + roomID).gameObject;
        if (availableRooms.Count == 0) { return transitionPos; }


        GameObject obj = Instantiate
            (availableRooms[UnityEngine.Random.Range(0, availableRooms.Count)], 
            parent.transform);
        
        obj.SetActive(true);
        obj.transform.Find("Walls").gameObject.SetActive(true);
        obj.transform.Find("POI").gameObject.SetActive(true);
        obj.name = "GeneratedRoom" + generatedRoomID;
        Vector3 transitionOffset = obj.transform.position - obj.transform.Find("Transition1").transform.position;



        obj.transform.position = new Vector3(
            transitionPos.x + transitionOffset.x,
            transitionPos.y + transitionOffset.y,
            transitionPos.z + transitionOffset.z
            );

        try
        {
            transitionPos = obj.transform.Find(roomNames.GetTransition2()).position;
        }
        catch { }

        return transitionPos;
    }


    //sprawdza czy pok�j koliduje z innym
    private bool CheckRoom(GameObject room, Vector3 transitionPos, GameObject parent)
    {
        GameObject obj = Instantiate
            (room, parent.transform);
        obj.SetActive(true);

        GameObject checkArea = obj.transform.Find(roomNames.GetCheckArea()).gameObject;

        Vector3 transitionOffset = obj.transform.position - obj.transform.Find("Transition1").transform.position;
        obj.transform.position = new Vector3(
            transitionPos.x + transitionOffset.x,
            transitionPos.y + transitionOffset.y,
            transitionPos.z + transitionOffset.z
            );


        bool output = true;
        for (int i = 0; i < checkArea.transform.childCount; i++)
        {
            //Debug.Log(checkArea.transform.GetChild(i).GetComponent<CheckArea>().SafeToPlace());
            if (checkArea.transform.GetChild(i).GetComponent<CheckArea>().SafeToPlace() == false)
            { 
                output = false;
                break;
            }
        }





        Destroy(obj);
        return output;
    }
   

}
