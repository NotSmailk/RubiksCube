using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    [field: SerializeField] private Transform up;
    [field: SerializeField] private Transform down;
    [field: SerializeField] private Transform front;
    [field: SerializeField] private Transform back;
    [field: SerializeField] private Transform left;
    [field: SerializeField] private Transform right;

    private CubeState cubeState;

    private void Awake()
    {
        cubeState = CubeState.globalCubeState;
    }

    public void SetSideColor()
    {
        UpdateSideColor(cubeState.up, up);
        UpdateSideColor(cubeState.back, back);
        UpdateSideColor(cubeState.down, down);
        UpdateSideColor(cubeState.left, left);
        UpdateSideColor(cubeState.front, front);
        UpdateSideColor(cubeState.right, right);
    }

    private void UpdateSideColor(List<GameObject> face, Transform side)
    {
        int i = 0;

        foreach (Transform map in side)
        {
            map.GetComponent<Image>().color = face[i].GetComponent<MeshRenderer>().material.color;

            i++;
        }
    }
}
