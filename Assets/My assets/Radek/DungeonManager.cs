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
    

    private void GenerateDungeon(GameObject transition, int remainingLength, bool hasBossRoom, GameObject room, int roomsWithoutDescendingArea, int chanceToSpawnDescendingArea)
    {

            //punkt przy³¹czenia, gdzie ma siê wygenerowaæ pokój
            Vector3 transitionPos = transition.transform.position;
            //liczba generowanych pokoi, bez mo¿liwoœci przy³¹czenia/generowania pokoju prowadz¹cego w dó³
            int roomsToGenerateWithoutDescendingArea;
            if (remainingLength > roomsWithoutDescendingArea) roomsToGenerateWithoutDescendingArea = roomsWithoutDescendingArea;
            else roomsToGenerateWithoutDescendingArea = remainingLength;
            int i=0;

        List<GameObject> availableRooms;


        //generowanie pokoi bez przy³¹czania/generowania pokoju prowadz¹cego w dó³
            for (i = 0; i < roomsToGenerateWithoutDescendingArea; i++)
            {
                availableRooms = GetAvailableRooms(room, transition, transitionPos);
                if (availableRooms.Count != 0) transitionPos = AppendRoom(availableRooms, transition, i, transitionPos);
                else
                {
                //przerywa wykonywanie
                Debug.Log("zablokuj przejœcie");
                    return;
                }
            }
        remainingLength -= roomsToGenerateWithoutDescendingArea;

        //szansa na wygenerowanie pokoju prowadz¹cego w dó³, wzór: 1/n; n jest zmniejszane z ka¿d¹ iteracj¹
        System.Random rand = new System.Random();
        int chanceToSpawnDescendingAreaButLocal = chanceToSpawnDescendingArea;
        bool descend = false;
        bool end = false;

        do
        {
            try
            {
                availableRooms = null;
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
                        availableRooms = GetAvailableRooms(room.transform.parent.Find(roomNames.GetDescendingAreas()).gameObject, transition, transitionPos);
                        descend = true;
                    }
                    //gdy nie nast¹pi generowanie zejœcia w dó³ to generowany jest normalny pokój
                    else
                    {
                        //Room
                        availableRooms = GetAvailableRooms(room, transition, transitionPos);
                    }

                }
                //gdy ma zostaæ 1 pokój do wygenerowania to musi byæ to zwyk³y pokój - nie mo¿e byæ to pokój prowadz¹cy w dó³
                else if (remainingLength == 1)
                {
                    //Room
                    availableRooms = GetAvailableRooms(room, transition, transitionPos);
                }
                //gdy pozosta³a d³ugoœæ lochu wynosi 0 to nastêpny pokój jest albo rest room z boss room'em, albo pokój bez wyjœcia
                else //remainingLength==0
                {
                    if (hasBossRoom)
                    {
                        //restRoom and bossRoom
                        availableRooms = GetAvailableRooms(room.transform.parent.Find(roomNames.GetRestRooms()).gameObject, transition, transitionPos);
                        transitionPos = AppendRoom(availableRooms, transition, i, transitionPos);
                        availableRooms = GetAvailableRooms(room.transform.parent.Find(roomNames.GetFinalRooms()).gameObject, transition, transitionPos);
                    }
                    else
                    {
                        //deadEndRoom
                        availableRooms = GetAvailableRooms(room.transform.parent.Find(roomNames.GetDeadEndRooms()).gameObject, transition, transitionPos);
                    }
                    end = true;
                }

                if (availableRooms.Count != 0)
                {
                    transitionPos = AppendRoom(availableRooms, transition, i, transitionPos);
                    if (descend)
                    {
                        GameObject tmp = transition.transform.Find(roomNames.GetGeneratedRoom() + i).gameObject;

                        int length1;
                        int length2;
                        if (remainingLength == 2) { length1 = 1; length2 = 1; }
                        else
                        {
                            //length1 = UnityEngine.Random.Range(1, remainingLength - 1);
                            //length2 = remainingLength - length1;
                            if (UnityEngine.Random.Range(0.0f, 1.0f) >= 0.5f)
                            {

                                length1 = UnityEngine.Random.Range(1, roomsToGenerateWithoutDescendingArea + 1);
                                length2 = remainingLength - length1;
                            }
                            else
                            {

                                length2 = UnityEngine.Random.Range(1, roomsToGenerateWithoutDescendingArea + 1);
                                length1 = remainingLength - length2;
                            }



                            }
                        //pokoj z boss room'em ma priorytet - czyli tam, gdzie jest dalej w lochu
                        if (length1 >= length2)
                        {
                            
                            GenerateDungeon(tmp.transform.Find(roomNames.GetTransition2()).gameObject, length1, hasBossRoom && length1 >= length2, room, roomsWithoutDescendingArea, chanceToSpawnDescendingArea);
                            GenerateDungeon(tmp.transform.Find(roomNames.GetTransition3()).gameObject, length2, hasBossRoom && !(length1 >= length2), room, roomsWithoutDescendingArea, chanceToSpawnDescendingArea);
                            remainingLength = 0;
                        }
                        else
                        {
                            GenerateDungeon(tmp.transform.Find(roomNames.GetTransition3()).gameObject, length2, hasBossRoom && !(length1 >= length2), room, roomsWithoutDescendingArea, chanceToSpawnDescendingArea);
                            GenerateDungeon(tmp.transform.Find(roomNames.GetTransition2()).gameObject, length1, hasBossRoom && length1 >= length2, room, roomsWithoutDescendingArea, chanceToSpawnDescendingArea);
                            remainingLength = 0;

                        }
                        break;
                    }

                    if (end) break;
                }
                else
                {
                    Debug.Log("Zablokuj przejœcie");
                    break;
                }






                chanceToSpawnDescendingAreaButLocal--;
                i++;
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        } while (remainingLength > 0);

    }



    private void GenerateDung(GameObject transition, DungeonGenerateChances chances, int roomsWithoutDescendingArea, int chanceToSpawnDescendingArea)
    {
        StartCoroutine(GenerateDungeon(transition, chances, roomsWithoutDescendingArea, chanceToSpawnDescendingArea));
    }

    IEnumerator GenerateDungeon(GameObject transition, DungeonGenerateChances chances, int roomsWithoutDescendingArea, int chanceToSpawnDescendingArea)
    {
        ClearChild(transition);
        yield return new WaitForSeconds(1);//bez tego ClearChild i GenerateDungeon lec¹ równolegle
        GenerateDungeon(transition, GenerateLength(chances), true, gameObject.transform.Find("Presets").Find("Rooms").gameObject, roomsWithoutDescendingArea, chanceToSpawnDescendingArea);
        GetComponent<NavigationBaker>().BakeSurface();
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


        if(CheckRoom(obj, transitionPos, parent) == false)
        {
            Debug.Log("ten pokoj tu nie moze byc");
        }

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


    //sprawdza czy pokój koliduje z innym
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
                Debug.Log("not safe to place");
                break;
            }
        }





        Destroy(obj);
        return output;
    }
   

}
