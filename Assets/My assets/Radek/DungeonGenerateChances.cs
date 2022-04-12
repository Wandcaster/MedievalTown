using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DungeonGenerateChances
{
    
    //size
    //minimum size will be 2 
    public int smallMin, smallMax, mediumMin, mediumMax, bigMin, bigMax;

    //% for dungeon size
    public int small, medium, big;
    public DungeonGenerateChances(int smallMin, int smallMax, int mediumMin, int mediumMax, int bigMin, int bigMax, int small, int medium)
    {
        this.smallMin = smallMin;
        this.smallMax = smallMax;
        this.mediumMin = mediumMin;
        this.mediumMax = mediumMax;
        this.bigMin = bigMin;
        this.bigMax = bigMax;
        this.small = small;
        this.medium = medium;
        this.big = 100 - small - medium;

        validate();


    }

    private void validate()
    {
        if (smallMin < 2) smallMin = 2;
        if (smallMax < 2) smallMax = 2;
        if (mediumMin < 2) mediumMin = 2;
        if (mediumMax < 2) mediumMax = 2;
        if (bigMin < 2) bigMin = 2;
        if (bigMax < 2) bigMax = 2;
    }
}
