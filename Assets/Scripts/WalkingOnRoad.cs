using CellField2D;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class WalkingOnRoad : MonoBehaviour
{
    public float RotationSpeed = 2f;
    public float speed = 2f;
    private int curIndex = -1;

    [SerializeField] private float minDistanceToVisitTarget = 0.1f;
    [SerializeField] private float minDistanceToNextRotate;
    private List<Vector3> CurentPath;
    private ParticleSystem particleSystem;
    Action<bool> action;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetObjectPosition(Vector3 pos)
    {
        pos.x += 0.5f;
        pos.z += 0.5f;
        pos.y += 1;
        transform.transform.position = pos;
    }
    public void StartMove(List<Vector3> path, Action<bool> pathComplete)
    {
        CurentPath = path;
        particleSystem.Play();
        curIndex = 0;
        action = pathComplete;
    }

    private void Update()
    {
        if (curIndex >= 0 && curIndex < CurentPath.Count)
        {
            MoveObject(CurentPath[curIndex]);
            if (Vector3.Distance(transform.position, CurentPath[curIndex]) <= minDistanceToVisitTarget)
            {
                curIndex++;
                if (curIndex >= CurentPath.Count)
                {
                    StopMove();
                }
            }
        }
    }
    private void MoveObject(Vector3 pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
        Vector3 direction = pos - transform.position;
        if(direction.magnitude < minDistanceToNextRotate && curIndex < CurentPath.Count-1)
        {
            direction = CurentPath[curIndex+1] - transform.position;
        }
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float step = RotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

    }

    private void StopMove()
    {
        action?.Invoke(true);
        action = null;
        curIndex = -1;
        particleSystem.Stop();
    }
}