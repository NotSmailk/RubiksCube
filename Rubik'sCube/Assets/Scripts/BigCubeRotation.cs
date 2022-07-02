using UnityEngine;

public class BigCubeRotation : MonoBehaviour
{
    [field: SerializeField] private GameObject target;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private Vector3 previosMousePosition;
    private Vector3 mouseDelta;

    private float angleX;
    private float angleY;
    private float angleZ;

    private float rotationSpeed = 150f;
    private float dragSpeedReduction = 0.1f;

    private void Update()
    {
        SwipeCube();

        DragCube();
    }

    private void SwipeCube()
    {
        if (InputManager.Mouse1Down)
        {
            firstPressPos = new Vector2(InputManager.inputPos.x, InputManager.inputPos.y);

            angleX = 0f;
            angleY = 0f;
            angleZ = 0f;
        }

        if (InputManager.Mouse1Up)
        {
            secondPressPos = new Vector2(InputManager.inputPos.x, InputManager.inputPos.y);

            currentSwipe = firstPressPos - secondPressPos;
            currentSwipe.Normalize();

            angleY = LeftSwipe(angleY);
            angleY = RightSwipe(angleY);

            angleX = LeftUpSwipe(angleX);
            angleX = RightDownSwipe(angleX);

            angleZ = RightUpSwipe(angleZ);
            angleZ = LeftDownSwipe(angleZ);

            target.transform.Rotate(angleX, angleY, angleZ, Space.World);
        }
    }

    private void DragCube()
    {
        if (InputManager.Mouse1)
        {
            mouseDelta = InputManager.inputPos - previosMousePosition;
            mouseDelta *= dragSpeedReduction;

            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, -mouseDelta.y) * transform.rotation;
        }
        else
        {
            if (transform.rotation != target.transform.rotation)
            {
                float step = rotationSpeed * Time.deltaTime;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }
        }

        previosMousePosition = InputManager.inputPos;
    }

    private float LeftSwipe(float rotationAngle)
    {
        return currentSwipe.x > 0f && currentSwipe.y <= 0.5f && currentSwipe.y >= -0.5f ? 90f : rotationAngle;
    }

    private float RightSwipe(float rotationAngle)
    {
        return currentSwipe.x < 0f && currentSwipe.y <= 0.5f && currentSwipe.y >= -0.5f ? -90f : rotationAngle;
    }

    private float LeftDownSwipe(float rotationAngle)
    {
        return currentSwipe.x > 0f && currentSwipe.y > 0.5f ? 90f : rotationAngle;
    }

    private float RightDownSwipe(float rotationAngle)
    {
        return currentSwipe.x < 0f && currentSwipe.y > 0.5f ? -90f : rotationAngle;
    }

    private float LeftUpSwipe(float rotationAngle)
    {
        return currentSwipe.x > 0f && currentSwipe.y < -0.5f ? 90f : rotationAngle;
    }

    private float RightUpSwipe(float rotationAngle)
    {
        return currentSwipe.x < 0f && currentSwipe.y < -0.5f ? -90f : rotationAngle;
    }
}
