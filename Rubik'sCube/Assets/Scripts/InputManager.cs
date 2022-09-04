using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static bool Android = false;
    public static bool Mouse0 = false;
    public static bool Mouse0Down = false;
    public static bool Mouse0Up = false;
    public static bool Mouse1 = false;
    public static bool Mouse1Down = false;
    public static bool Mouse1Up = false;
    public static bool isRotating = false;
    public static Vector3 inputPos;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
            Android = true;
    }

    private void Update()
    {
        if (Android)
        {
            inputPos = Input.mousePosition;
            
            if (isRotating)
            {
                Mouse0 = Input.GetMouseButton(0);
                Mouse0Down = Input.GetMouseButtonDown(0);
                Mouse0Up = Input.GetMouseButtonUp(0);
            }
            else
            {
                Mouse1 = Input.GetMouseButton(0);
                Mouse1Down = Input.GetMouseButtonDown(0);
                Mouse1Up = Input.GetMouseButtonUp(0);
            }
        }
        else
        {
            inputPos = Input.mousePosition;

            Mouse0 = Input.GetKey(KeyCode.Mouse0);
            Mouse1 = Input.GetKey(KeyCode.Mouse1);
            Mouse0Down = Input.GetKeyDown(KeyCode.Mouse0);
            Mouse1Down = Input.GetKeyDown(KeyCode.Mouse1);
            Mouse0Up = Input.GetKeyUp(KeyCode.Mouse0);
            Mouse1Up = Input.GetKeyUp(KeyCode.Mouse1);
        }
    }

    public void SetDragging()
    {
        Mouse0 = false;
        Mouse1 = false;
        Mouse0Down = false;
        Mouse1Down = false;
        Mouse0Up = false;
        Mouse1Up = false;
    }

    public Vector3 InputMousePosition()
    {
        Vector3 newMousePos = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        return inputPos - newMousePos;
    }
}
