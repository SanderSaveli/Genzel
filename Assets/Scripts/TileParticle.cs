using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TileParticle : MonoBehaviour
{
    Camera _camera;
    public Transform center;
    public TileFigure figure;
    public Vector2Int fieldCoordinate => lastCellView.coordinates;
    public Vector2Int coordinate => _coordinate;
    [SerializeField] private Vector2Int _coordinate;
    [SerializeField] private bool _isRoad;
    public bool isRoad => _isRoad;
    public Road road { get; private set; }
    private Vector3 prevPos;

    public CellView lastCellView;
    private IDropHandler lastSelectedView;
    bool isMovable;

    private void Awake()
    {
        _coordinate.x = (int)transform.localPosition.x;
        _coordinate.y = (int)transform.localPosition.z;
        figure = GetComponentInParent<TileFigure>();
        center = GetComponent<Transform>();
        if (isRoad)
        {
            road = GetComponent<Road>();
        }
    }
    private void Start()
    {
        _camera = Camera.main;
    }

    public virtual void StartDrag()
    {
        isMovable = true;
        figure.SelectFigure();
        if (lastCellView != null)
        {
            lastCellView.figureLeave(this);
        }
    }

    public virtual void UpdateDrag()
    {
        Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (isMovable)
        {
            figure.moveFigure(pos - prevPos);
            if (TryCatchDropHandler(out IDropHandler handler))
            {
                if (lastSelectedView != null)
                {
                    if (handler != lastSelectedView)
                    {
                        lastSelectedView.ExitHover(this);
                        lastSelectedView = handler;
                        AudioManager.Instance.PlaySFX("TileMove");
                    }
                    handler.EnterHover(this);
                }
                else
                {
                    lastSelectedView = handler;
                }
            }
            else
            {
                lastSelectedView.ExitHover(this);
            }
        }
        prevPos = pos;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    }

    public virtual void EndDrag()
    {
        isMovable = false;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction);
        bool flag = false;
        figure.DeSelectFigure();
        if (TryCatchDropHandler(out IDropHandler handler))
        {

            flag = handler.DropFigure(this);
        }
        if (!flag)
        {
            if (lastCellView != null)
            {
                lastCellView.DropFigure(this);
            }
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.canMoveTiles)
        {
            StartDrag();
            AudioManager.Instance.PlaySFX("TileUp");
        }
    }

    private void OnMouseUp()
    {
        if (GameManager.canMoveTiles)
        {
            EndDrag();
            AudioManager.Instance.PlaySFX("TileDown");
        }
    }

    private void Update()
    {
        if (GameManager.canMoveTiles)
        {
            UpdateDrag();
        }
    }

    private bool TryCatchDropHandler(out IDropHandler handler)
    {
        handler = null;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.TryGetComponent<IDropHandler>(out IDropHandler h))
            {
                handler = h;
                return true;
            }
        }
        return false;
    }

    public void PlaceOnCell(CellView view)
    {
        //_coordinate = view.coordinates;
    }
}
