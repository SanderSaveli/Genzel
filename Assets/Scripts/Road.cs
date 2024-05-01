using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Dir
{
    left,
    right,
    top,
    bottom
}

public enum RoadType
{
    Default,
    StartRoad,
    EndRoad,
}

public class Road : MonoBehaviour
{

    public RoadType roadType = RoadType.Default;
    public Dir In1;
    public Dir In2;

}
