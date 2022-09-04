using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;

    private Quaternion targetQuaternion;

    private Vector3 localForward;
    private Vector3 mouseRef;
    private Vector3 rotation;

    private bool dragging = false;
    private bool autoRotating = false;

    private float sensitivity = 0.4f;
    private float speed = 300f;
    private float lastAngle = 0f;

    private int dragCoef = 1;

    private ReadCubeSides readCube;
    private CubeState cubeState;

    private void Start()
    {
        readCube = ReadCubeSides.globalReadCubeSides;
        cubeState = CubeState.globalCubeState;
    }

    private void Update()
    {
        if (dragging && !autoRotating)
        {
            SpinSide(activeSide);

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                dragging = false;
                RotateToRightAngle();
            }
        }

        if (autoRotating)
        {
            AutoRotate();
        }
    }

    private void SpinSide(List<GameObject> side)
    {
        rotation = side[4].transform.rotation.eulerAngles;

        Vector3 mouseOffset = (InputManager.inputPos - mouseRef) * dragCoef; 
        
        Vector2 pivotScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, side[4].transform.position);

        //float angle = (mouseOffset.x + mouseOffset.y) * sensitivity;
        float angle = Vector2.Angle(pivotScreenPoint, InputManager.inputPos);

        Debug.Log(angle);

        //if (side == cubeState.front)
        //    rotation.x = angle * -1f;
        //if (side == cubeState.back)
        //    rotation.x = angle * 1f;
        //if (side == cubeState.left)
        //    rotation.z = angle * -1f;
        //if (side == cubeState.right)
        //    rotation.z = angle * 1f;
        //if (side == cubeState.up)
        //    rotation.y = angle * 1f;
        //if (side == cubeState.down)
        //    rotation.y = angle * -1f;

        if (side == cubeState.front)
            rotation.x = angle;
        if (side == cubeState.back)
            rotation.x = angle;
        if (side == cubeState.left)
            rotation.z = angle;
        if (side == cubeState.right)
            rotation.z = angle;
        if (side == cubeState.up)
            rotation.y = angle;
        if (side == cubeState.down)
            rotation.y = angle;

        lastAngle = angle;

        transform.rotation = Quaternion.Euler(rotation);

        //transform.Rotate(rotation, Space.Self);

        mouseRef = InputManager.inputPos;
    }

    public void RotateDefaults(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = InputManager.inputPos;
        dragging = true;
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;

        Vector3 cubeTransform = Camera.main.WorldToScreenPoint(transform.position);

        dragCoef = InputManager.inputPos.y > cubeTransform.y ? 1 : -1;
    }

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.PickUp(side);

        Vector3 localForward = Vector3.zero - side[4].transform.parent.localPosition;

        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;

        activeSide = side;
        autoRotating = true;
    }

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;

        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;
    }

    private void AutoRotate()
    {
        dragging = false;

        var step = speed * Time.deltaTime;

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;

            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadCubeState();

            autoRotating = false;
            CubeState.autoRotating = false;
        }
    }
}
