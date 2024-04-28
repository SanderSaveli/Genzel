using CellField2D;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameField), typeof(RoadMover), typeof(PathFinder))]
public class GameManager : MonoBehaviour
{
    public static bool canMoveTiles = true;
    private GameMenu menu;
    private GameField gameField;
    private RoadMover roadMover;
    private PathFinder pathFinder;
    public Road StartCell;
    private Action<bool> roadEnd;
    private bool lastResult = false;
    private bool isSets;

    private void Start()
    {
        gameField = GetComponent<GameField>();
        roadMover = GetComponent<RoadMover>();
        pathFinder = GetComponent<PathFinder>();
        menu = FindObjectOfType<GameMenu>();
        isSets = false;
        canMoveTiles = true;
    }

    private void Update()
    {
        if (!isSets)
        {
            roadMover.SetObjectPosition(StartCell.transform.position);
            isSets = true;
        }
    }

    private void OnEnable()
    {
        roadEnd += RoadEnd;
    }
    public void StratMove()
    {
        canMoveTiles = false;
        lastResult = pathFinder.TryCreatePath(gameField.cellField, StartCell, out List<IReferedCell> cells);

        roadMover.MoveToRoad(gameField.roadLayer, cells, roadEnd);
    }

    private void RoadEnd(bool isPathComplete)
    {
        if (lastResult)
        {
            AudioManager.Instance.PlaySFX("Win");
            menu.ShowWin();
        }
        else
        {
            AudioManager.Instance.PlaySFX("Lose");
            menu.ShowLose();
        }
    }
}
