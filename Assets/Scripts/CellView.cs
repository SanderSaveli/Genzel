using UnityEngine;
using Zenject;

public class CellView : MonoBehaviour, IDropHandler
{
    public GameField gameField;

    public Vector2Int coordinates { get; private set; }
    
    public void SetCoordinates(Vector2Int coordinates)
    {
        this.coordinates = coordinates;
    }
    public bool DropFigure(TileParticle figure)
    {
        if (gameField.TryPlaceFigure(figure, this))
        {
            gameField.PlaceFigure(figure, this);
            figure.PlaceOnCell(this);
            return true;    
        }
        return false;
    }

    public void figureLeave(TileParticle figure)
    {
        gameField.LeaveCell(figure,this);
    }

    public void ChangeOvner(int ownerID)
    {
        Debug.Log("Change" + ownerID+ " " + coordinates);
        if(ownerID == 0)
        {
            DeSelect();
        }
        if (ownerID == 1)
        {
            gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.red;
        }
    }

    public void EnterHover(TileParticle figure)
    {
        gameField.Select(figure, this);
    }

    public void ExitHover(TileParticle figure)
    {
        gameField.DeSelect(figure, this);
    }

    public void Select()
    {
        gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
    }

    public void DeSelect()
    {
        ColorUtility.TryParseHtmlString("#594E4A", out Color myColor);
        gameObject.GetComponent<MeshRenderer>().materials[0].color = myColor;
    }
}
