using TMPro;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] private GameObject Select1;
    [SerializeField] private GameObject Select2;

    [SerializeField] private TMP_Text restartText;
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private PlayerInput input;

    private int selectedIndex = 0;
}
