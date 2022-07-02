using System.Collections.Generic;
using UnityEngine;

public class SelectCubeFace : MonoBehaviour
{
    private CubeState cubeState;
    private ReadCubeSides readCube;
    private int layerMask = 1 << 8;

    private void Start()
    {
        cubeState = GetComponent<CubeState>();
        readCube = GetComponent<ReadCubeSides>();
    }

    private void Update()
    {
        if (InputManager.Mouse0Down && !CubeState.autoRotating)
        {
            readCube.ReadCubeState();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
            {
                GameObject face = hit.collider.gameObject;

                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };

                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        cubeState.PickUp(cubeSide);

                        cubeSide[4].transform.parent.GetComponent<PivotRotation>().RotateDefaults(cubeSide);
                    }
                }
            }
        }
    }
}
