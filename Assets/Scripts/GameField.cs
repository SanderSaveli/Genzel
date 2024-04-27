using UnityEngine;
using UnityEngine.Tilemaps;
using CellField2D;
using System;

public class GameField : MonoBehaviour
{
    [SerializeField] private GameObject fieldView;
    [SerializeField] private Tilemap fieldLayer;
    [SerializeField] private Tilemap roadLayer;

    private RectangleField<IReferedCell> cellField;

    private void Start()
    {
        cellField = new RectangleField<IReferedCell>();
        InstantField();
        
    }

    public bool TryPlaceFigure(TileParticle centerPart, CellView cell)
    {
        foreach (TileParticle particle in centerPart.figure.tileParticles)
        {
            if (GetPatricleCell(cell, centerPart, particle, out IReferedCell OwnedCell))
            {
                if (OwnedCell.ownerID != 0)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    public void Select(TileParticle centerPart, CellView cell)
    {
        if(TryPlaceFigure(centerPart, cell))
        {
            foreach (TileParticle particle in centerPart.figure.tileParticles)
            {
                if (GetPatricleCell(cell, centerPart, particle, out IReferedCell OwnedCell))
                {
                    OwnedCell.cellView.Select();
                }
            }
        }
    }

    public void DeSelect(TileParticle centerPart, CellView cell)
    {
        if (TryPlaceFigure(centerPart, cell))
        {
            foreach (TileParticle particle in centerPart.figure.tileParticles)
            {
                if (GetPatricleCell(cell, centerPart, particle, out IReferedCell OwnedCell))
                {
                    OwnedCell.cellView.DeSelect();
                }
            }
        }
    }

    public void PlaceFigure(TileParticle centerPart, CellView cell)
    {
        if(TryPlaceFigure(centerPart, cell))
        {
            foreach (TileParticle particle in centerPart.figure.tileParticles)
            {
                if(GetPatricleCell(cell, centerPart, particle, out IReferedCell OwnedCell))
                {
                    Vector2Int cellPos = OwnedCell.coordinates;
                    particle.transform.position = roadLayer.CellToWorld(new Vector3Int(cellPos.x, cellPos.y, 0));
                    OwnedCell.changeOwner(1);
                    particle.lastCellView = OwnedCell.cellView;
                }
            }
        }
    }

    public void LeaveCell(TileParticle centerPart, CellView cell)
    {
        Debug.Log(centerPart.figure.tileParticles.Count);
        foreach (TileParticle particle in centerPart.figure.tileParticles)
        {       
            if (GetPatricleCell(cell, centerPart, particle, out IReferedCell OwnedCell))
            {
                OwnedCell.changeOwner(0);
            }
        }
    }

    private void InstantField()
    {
        int childCount = fieldLayer.transform.childCount;
        for(int i =0; i < childCount; i++)
        {
            if(fieldLayer.transform.GetChild(i).TryGetComponent(out CellView cellView))
            {
                Vector2Int coordinate = (Vector2Int)fieldLayer.WorldToCell(cellView.transform.position);
                IReferedCell ownedCell = new ReferedCell(coordinate, 0, cellView);
                cellField.InstantCell(ownedCell);
                cellView.SetCoordinates(coordinate);
                cellView.gameField = this;
            }
        }

        childCount = roadLayer.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (roadLayer.transform.GetChild(i).TryGetComponent(out TileFigure figure))
            {
                Vector2Int coordinate = (Vector2Int)fieldLayer.WorldToCell(figure.tileParticles[0].transform.position);
                cellField.TryGetCell(coordinate,out IReferedCell ownedCell);
                ownedCell.cellView.DropFigure(figure.tileParticles[0]);
            }
        }
    }

    private bool GetPatricleCell(CellView cell, TileParticle center, TileParticle particle, out IReferedCell referedCell)
    {
        referedCell = null;
        if(cellField.TryGetCell(cell.coordinates + (particle.coordinate - center.coordinate), out IReferedCell OwnedCell))
        {
            referedCell= OwnedCell;
            return true;
        }
        return false;
    }


}
