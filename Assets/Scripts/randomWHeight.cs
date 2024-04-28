using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomWHeight : MonoBehaviour
{
    void Start()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Random.Range(-0.1f, 0.2f);
        transform.position += pos;
    }
}
