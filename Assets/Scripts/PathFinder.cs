using CellField2D;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public bool TryCreatePath(RectangleField<IReferedCell> field, Road StartRoad, out List<IReferedCell> path)
    {
        path = new List<IReferedCell>();
        Vector2Int curCoor = StartRoad.gameObject.GetComponent<TileParticle>().fieldCoordinate;
        path.Add(field.GetCell(curCoor));
        Road Curentroad = StartRoad;
        Dir curentDir = StartRoad.In1;
        while (!Curentroad.endRoad)
        {
            if (field.TryGetCell(curCoor + GetVectorByDir(curentDir), out IReferedCell cell))
            {
                Debug.Log(cell.ToString());
                if (cell.tile == null)
                {
                    return false;
                }
                if (cell.tile.isRoad)
                {
                    Dir opositeDir = GetOppositeDir(curentDir);
                    if (cell.tile.road.In1 == opositeDir)
                    {
                        path.Add(cell);
                        curentDir = cell.tile.road.In2;
                        Curentroad = cell.tile.road;
                        curCoor = cell.tile.fieldCoordinate;
                    }
                    else if (cell.tile.road.In2 == opositeDir)
                    {
                        path.Add(cell);
                        curentDir = cell.tile.road.In1;
                        Curentroad = cell.tile.road;
                        curCoor = cell.tile.fieldCoordinate;
                    }
                    else
                    {
                        return false;
                    }

                    if (cell.tile.road.endRoad)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private Dir GetOppositeDir(Dir dir)
    {
        switch (dir)
        {
            case (Dir.left):
                return Dir.right;
            case (Dir.right):
                return Dir.left;
            case (Dir.top):
                return Dir.bottom;
            case (Dir.bottom):
                return Dir.top;
            default:
                return Dir.top;

        }
    }

    private Vector2Int GetVectorByDir(Dir dir)
    {
        switch (dir)
        {
            case (Dir.left):
                return new Vector2Int(-1, 0);
                break;
            case (Dir.right):
                return new Vector2Int(1, 0);
                break;
            case (Dir.top):
                return new Vector2Int(0, 1);
                break;
            case (Dir.bottom):
                return new Vector2Int(0, -1);
                break;
            default:
                return Vector2Int.up;
                break;

        }
    }
}
