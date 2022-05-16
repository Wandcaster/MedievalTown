using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNames
{
    private string room;
    private string descendingAreas;
    private string transition2;
    private string transition3;
    private string presets;
    private string finalRooms;
    private string deadEndRooms;
    private string restRooms;
    private string generatedRoom;

    public RoomNames()
    {
        room = "Room";
        descendingAreas = "DescendingAreas";
        transition2 = "Transition2";
        transition3 = "Transition3";
        presets = "Presets";
        finalRooms = "FinalRooms";
        deadEndRooms = "DeadEndRooms";
        generatedRoom = "GeneratedRoom";
    }
    public string GetRoom() { return room; }
    public string GetDescendingAreas() { return descendingAreas; }
    public string GetTransition2() { return transition2; }
    public string GetTransition3() { return transition3; }
    public string GetPresets() { return presets; }
    public string GetFinalRooms() { return finalRooms; }
    public string GetDeadEndRooms() { return deadEndRooms; }

}
