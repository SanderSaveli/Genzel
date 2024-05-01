using CellField2D;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameField), typeof(PathFinder))]
public class GameManager : MonoBehaviour
{
    public static bool canMoveTiles = true;
    private GameMenu menu;
    private GameField gameField;
    private WalkingOnRoad roadMover;
    private PathFinder pathFinder;
    private Action<bool> roadEnd;
    private bool lastResult = false;
    private bool isSets;

    private void Start()
    {
        gameField = GetComponent<GameField>();
        roadMover = FindObjectOfType<WalkingOnRoad>();
        pathFinder = GetComponent<PathFinder>();
        menu = FindObjectOfType<GameMenu>();
        isSets = false;
        canMoveTiles = true;

        Vector3Int startCellPos = (Vector3Int)gameField.startCell.gameObject.GetComponent<TileParticle>().fieldCoordinate;
        roadMover.SetObjectPosition(gameField.roadLayer.CellToWorld(startCellPos));
        isSets = true;
    }

    private void OnEnable()
    {
        roadEnd += RoadEnd;
    }
    public void StratMove()
    {
        canMoveTiles = false;
        lastResult = pathFinder.TryCreatePath(gameField.cellField, gameField.startCell, out List<IReferedCell> cells);
        roadMover.StartMove(GetCellCoordinates(cells), roadEnd);
        AudioManager.Instance.PlaySFX("Move");
    }

    private void RoadEnd(bool isPathComplete)
    {
        AudioManager.Instance.MuteSFX();
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

    private List<Vector3> GetCellCoordinates(List<IReferedCell> cells)
    {
        List<Vector3> cellPositions = new();
        foreach (IReferedCell cell in cells)
        {
            Vector3Int coor = new Vector3Int(cell.x, cell.y, 0);
            Vector3 targetPos = gameField.roadLayer.CellToWorld(coor);
            targetPos.x += 0.5f;
            targetPos.z += 0.5f;
            targetPos.y += 1;
            cellPositions.Add(targetPos);
        }
        return cellPositions;
    }
}
