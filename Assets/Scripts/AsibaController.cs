
using UnityEngine;

public class AsibaController : MonoBehaviour
{
    [SerializeField] private GateController gateController;
    public int buttonNumber;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (buttonNumber == 1 && other.gameObject.CompareTag("Player"))
        {
            gateController.SetButton(1, true);
        }

        if (buttonNumber == 2 && other.gameObject.CompareTag("Player"))
        {
            gateController.SetButton(2, true);
        }

        if (buttonNumber == 1 && other.gameObject.CompareTag("Girl"))
        {
            gateController.SetButton(1, true);
        }

        if (buttonNumber == 2 && other.gameObject.CompareTag("Girl"))
        {
            gateController.SetButton(2, true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (buttonNumber == 1 && other.gameObject.CompareTag("Player"))
        {
            gateController.SetButton(1, false);
        }

        if (buttonNumber == 2 && other.gameObject.CompareTag("Player"))
        {
            gateController.SetButton(2, false);
        }

        if (buttonNumber == 1 && other.gameObject.CompareTag("Girl"))
        {
            gateController.SetButton(1, false);
        }

        if (buttonNumber == 2 && other.gameObject.CompareTag("Girl"))
        {
            gateController.SetButton(2, false);
        }
    }
}