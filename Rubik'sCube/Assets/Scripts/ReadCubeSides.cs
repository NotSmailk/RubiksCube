using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCubeSides : MonoBehaviour
{
    public static ReadCubeSides globalReadCubeSides;

    [field: SerializeField] private Transform readerUp;
    [field: SerializeField] private Transform readerDown;
    [field: SerializeField] private Transform readerLeft;
    [field: SerializeField] private Transform readerRight;
    [field: SerializeField] private Transform readerFront;
    [field: SerializeField] private Transform readerBack;

    [field: SerializeField] private CubeMap cubeMap;
    [field: SerializeField] private GameObject emptyGO;

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();

    private int layerMask = 1 << 8;
    private CubeState cubeState;

    private void Awake()
    {
        globalReadCubeSides = this;
    }

    private void Start()
    {
        cubeState = GetComponent<CubeState>();

        SetRayTransforms();

        ReadCubeState();

        CubeState.started = true;
    }

    public void ReadCubeState()
    {
        cubeState.up = ReadFace(upRays, readerUp);
        cubeState.down = ReadFace(downRays, readerDown);
        cubeState.front = ReadFace(frontRays, readerFront);
        cubeState.back = ReadFace(backRays, readerBack);
        cubeState.left = ReadFace(leftRays, readerLeft);
        cubeState.right = ReadFace(rightRays, readerRight);

        cubeMap?.SetSideColor();
    }

    private void SetRayTransforms()
    {
        upRays = BuildRays(readerUp, new Vector3(90f, 90f, 0f));
        downRays = BuildRays(readerDown, new Vector3(270f, 90f, 0f));
        rightRays = BuildRays(readerRight, new Vector3(0f, 180f, 0f));
        leftRays = BuildRays(readerLeft, new Vector3(0f, 0f, 0f));
        frontRays = BuildRays(readerFront, new Vector3(0f, 90f, 0f));
        backRays = BuildRays(readerBack, new Vector3(0f, 270, 0f));
    }

    private List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.position.x + x,
                                               rayTransform.position.y + y,
                                               rayTransform.position.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }

        rayTransform.localRotation = Quaternion.Euler(direction);

        return rays;
    }

    private List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTrasform)
    {
        List<GameObject> facesHit = new List<GameObject>();

        foreach (var rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;

            if (Physics.Raycast(ray, rayTrasform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                facesHit.Add(hit.collider.gameObject);
            }
        }        

        return facesHit;
    }
}
