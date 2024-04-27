using System.Collections.Generic;
using UnityEngine;

public class TileFigure : MonoBehaviour
{
    public List<TileParticle> tileParticles { get; private set; }

    private void Awake()
    {
        tileParticles = new List<TileParticle>();
        int childCount = transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out TileParticle particle))
            {
                tileParticles.Add(particle);
                particle.figure = this;
            }
        }
    }

    public void moveFigure(Vector3 deltaMove)
    {
        deltaMove.y = 0;
        transform.position += deltaMove;
    }

    public void SelectFigure()
    {
        Vector3 pos = transform.position;
        pos.y = 2;
        transform.position = pos;
    }

    public void DeSelectFigure()
    {
        Vector3 pos = transform.position;
        pos.y = 1;
        transform.position = pos;
    }

    public void HowerOnFigure()
    {
        Vector3 pos = transform.position;
        pos.y = 1.5f;
        transform.position = pos;
    }
}
