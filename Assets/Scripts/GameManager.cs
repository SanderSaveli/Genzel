using CellField2D;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameField gameField;
    public RoadMover roadMover;
    public PathFinder pathFinder;
    public Road StartCell;
    private Action<bool> roadEnd;
    private bool lastResult = false;

    private void OnEnable()
    {
        roadEnd += RoadEnd;
    }
    public void StratMove()
    {
        lastResult = pathFinder.TryCreatePath(gameField.cellField, StartCell, out List<IReferedCell> cells);

        roadMover.MoveToRoad(gameField.roadLayer, cells, roadEnd);
    }

    private void RoadEnd(bool isPathComplete)
    {
        if (lastResult)
        {
            Debug.Log("Win");
        }
        else
        {
            Debug.Log("Lose");
        }
    }
}
