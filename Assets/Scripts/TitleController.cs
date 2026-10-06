using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class TitleController : MonoBehaviour
{
    [SerializeField] private GameObject select1UI;

    [SerializeField] private TMP_Text startGameText;
    [SerializeField] private TMP_Text option;
    [SerializeField] private GameObject optionImage;

    private PlayerInput input;

    private int selectedIndex = 0;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        optionImage.SetActive(false);

        UpdateSelection();
    }

    private void Update()
    {
        if (optionImage.activeSelf)
        {
            if (input.BkeyPressed)
            {
                GoTitle();
            }
        }

        if (!select1UI.activeSelf)
        {
            return;
        }

        if (input.WkeyPressed)
        {
            selectedIndex--;

            if (selectedIndex < 0)
            {
                selectedIndex = 1;
            }

            UpdateSelection();
        }

        if (input.SkeyPressed)
        {
            selectedIndex++;

            if (selectedIndex > 1)
            {
                selectedIndex = 0;
            }

            UpdateSelection();
        }

        if (input.JumpPressed)
        {
            Select();
        }
    }

    private void UpdateSelection()//ã‚©‰º‚©–îˆó‚Ì•`‰æ
    {
        if (selectedIndex == 0)
        {
            startGameText.text = "> Start Game";
            option.text = "  Option";
        }
        else
        {
            startGameText.text = "  Start Game";
            option.text = "> Option";
        }

    }

    private void Select()//‘I‚ñ‚¾Žž‚Ì“®‚«
    {
        if (selectedIndex == 0)
        {
            StartGame();
        }
        else
        {
            GoOption();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void GoOption()
    {
        select1UI.SetActive(false);
        optionImage.SetActive(true);
    }

    private void GoTitle()
    {
        select1UI.SetActive(true);
        optionImage.SetActive(false);
    }
}
