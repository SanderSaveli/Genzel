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
        Debug.Log(curCoor.ToString());
        Road Curentroad = StartRoad;
        Dir curentDir = Dir.right;
        while (!Curentroad.endRoad)
        {
            if (field.TryGetCell(curCoor + GetVectorByDir(curentDir), out IReferedCell cell))
            {
                Debug.Log(path.Count);
                Debug.Log("t" + cell.coordinates + " " + (curCoor + GetVectorByDir(curentDir)));
                if (cell.tile == null)
                {
                    Debug.Log("j");
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
                        Debug.Log("e");
                        return false;
                    }

                    if (cell.tile.road.endRoad)
                    {
                        return true;
                    }
                }
                else
                {
                    Debug.Log("w");
                    return false;
                }
            }
            else
            {
                Debug.Log("s");
                return false;
            }
        }
        Debug.Log("m");
        return false;
    }

    private Dir GetOppositeDir(Dir dir)
    {
        switch (dir)
        {
            case (Dir.left):
                return Dir.right;
                break;
            case (Dir.right):
                return Dir.left;
                break;
            case (Dir.top):
                return Dir.bottom;
                break;
            case (Dir.bottom):
                return Dir.top;
                break;
            default:
                return Dir.top;
                break;

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
