using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    private LineRenderer _line;
    public Player player;
    public Cursor cursor;
    public Transform barrel;
    Vector3[] points;

    void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        points = new Vector3[2];
    }

    void Update()
    {
        _line.enabled = player.isGrounded;        
        points[0] = barrel.transform.position;
        points[1] = cursor.transform.position;
        _line.SetPositions(points);
    }
}
