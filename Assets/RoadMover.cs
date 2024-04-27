using CellField2D;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoadMover : MonoBehaviour
{
    public float speed = 2f;
    public float height = 2.5f;
    public Transform movableObject;
    private int curIndex = -1;
    private List<IReferedCell> CurentPath;
    Tilemap field;
    Action<bool> action;

    public void SetObjectPosition(Vector3 pos)
    {
        pos.y = height;
        movableObject.transform.position = pos;
    }
    public void MoveToRoad(Tilemap field, List<IReferedCell> path, Action<bool> pathComplete)
    {
        curIndex = 0;
        this.field = field;
        this.CurentPath = path;
        this.action = pathComplete;
    }

    private void Update()
    {
        if (curIndex >= 0 && curIndex < CurentPath.Count)
        {
            Vector3Int coor = new Vector3Int(CurentPath[curIndex].x, CurentPath[curIndex].y, 0);
            Vector3 targetPos = field.CellToWorld(coor);
            targetPos.y = height;
            MoveObject(targetPos);
            if (Vector3.Distance(movableObject.position, targetPos) <= 0.01f)
            {
                curIndex++;
                if (curIndex >= CurentPath.Count)
                {
                    action?.Invoke(true);
                    action = null;
                    curIndex = -1;
                }
            }
        }
    }
    private void MoveObject(Vector3 pos)
    {
        pos.y = height;
        movableObject.position = Vector3.MoveTowards(movableObject.position, pos, speed * Time.deltaTime);
    }
}