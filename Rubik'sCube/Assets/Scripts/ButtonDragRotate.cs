using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDragRotate : MonoBehaviour
{
    public void ChangeText()
    {
        InputManager.isRotating = !InputManager.isRotating;

        if (InputManager.isRotating)
            GetComponentInChildren<Text>().text = "Rotate";
        else
            GetComponentInChildren<Text>().text = "Drag";
    }
}
