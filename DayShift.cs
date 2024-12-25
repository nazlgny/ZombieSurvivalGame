using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Description: Simulates a day-night cycle by rotating the object.
public class DayShift : MonoBehaviour
{
    void Start()
    {
        // Initialization (if needed).
    }

    void Update()
    {
        // Rotate the object around a specified point.
        transform.RotateAround(new Vector3(250f, 0, 250f), Vector3.right, 0.5f * Time.deltaTime);
    }
}
