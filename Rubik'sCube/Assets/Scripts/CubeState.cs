using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> up = new List<GameObject>();
    [field: SerializeField] public List<GameObject> down = new List<GameObject>();
    [field: SerializeField] public List<GameObject> front = new List<GameObject>();
    [field: SerializeField] public List<GameObject> back = new List<GameObject>();
    [field: SerializeField] public List<GameObject> left = new List<GameObject>();
    [field: SerializeField] public List<GameObject> right = new List<GameObject>();

    public static bool autoRotating = false;
    public static bool started = false;
    public static bool isDragging = false;
    public static CubeState globalCubeState;

    private void Awake()
    {
        globalCubeState = this;
    }

    public void PickUp(List<GameObject> cubeSide)
    {
        foreach (GameObject face in cubeSide)
        {
            if (face != cubeSide[4])
            {
                face.transform.parent.parent = cubeSide[4].transform.parent;
            }
        }
    }

    public void PutDown(List<GameObject> littleCubes, Transform pivotTransform)
    {
        foreach (GameObject littlecube in littleCubes)
        {
            if (littlecube != littleCubes[4])
            {
                littlecube.transform.parent.parent = pivotTransform;
            }
        }
    }

    private string GetSideString(List<GameObject> side)
    {
        string sideString = string.Empty;

        foreach (GameObject face in side)
            sideString += face.name[0].ToString();

        return sideString;
    }

    public string GetStateString()
    {
        string stateString = string.Empty;

        stateString += GetSideString(up);
        stateString += GetSideString(right);
        stateString += GetSideString(front);
        stateString += GetSideString(down);
        stateString += GetSideString(left);
        stateString += GetSideString(back);

        return stateString;
    }
}