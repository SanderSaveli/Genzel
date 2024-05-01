using UnityEngine;

public class CellView : MonoBehaviour, IDropHandler
{
    public GameField gameField;
    public Color defaultColor = Color.gray;
    public Color highlightColor = Color.yellow;

    private MeshRenderer _meshRenderer;

    public Vector2Int coordinates { get; private set; }
    private void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        DeSelect();
    }

    public void SetCoordinates(Vector2Int coordinates)
    {
        this.coordinates = coordinates;
    }
    public bool DropFigure(TileParticle figure)
    {
        if (gameField.TryPlaceFigure(figure, this))
        {
            gameField.PlaceFigure(figure, this);
            return true;
        }
        return false;
    }

    public void figureLeave(TileParticle figure)
    {
        gameField.LeaveCell(figure, this);
    }

    public void ChangeOvner(int ownerID)
    {
        if (ownerID == 0)
        {
            DeSelect();
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
        _meshRenderer.materials[0].color = highlightColor;
    }

    public void DeSelect()
    {
        _meshRenderer.materials[0].color = defaultColor;
    }
}
