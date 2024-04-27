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

public class Road : MonoBehaviour
{

    public bool endRoad = false;
    public Dir In1;
    public Dir In2;

}
