
using UnityEngine;

public class GateController : MonoBehaviour
{
    private bool button1Pressed = false;
    private bool button2Pressed = false;

    public void SetButton(int buttonNumber, bool pressed)
    {
        if (buttonNumber == 1)
        {
            button1Pressed = pressed;
        }
        else if (buttonNumber == 2)
        {
            button2Pressed = pressed;
        }

        Debug.Log("Button1: " + button1Pressed
                + ", Button2: " + button2Pressed);

        if (button1Pressed && button2Pressed)
        {
            this.gameObject.SetActive(false);
        }
    }
}