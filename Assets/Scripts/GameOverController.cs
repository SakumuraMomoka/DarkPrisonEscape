using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public static GameOverController instance;

    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TMP_Text restartText;
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private PlayerInput input;

    private int selectedIndex = 0;
    private bool isGameOver = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        gameOverUI.SetActive(false);
    }

    public void GameOver()//playerのhpが０になった時の処理
    {
        if (isGameOver)
            return;

        isGameOver = true;//playerを操作不能にする

        input.canControl = false;
        gameOverUI.SetActive(true);

        Time.timeScale = 0f;

        selectedIndex = 0;
        UpdateSelection();
    }

    private void Update()
    {
        if (!gameOverUI.activeSelf)
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
    
    private void UpdateSelection()//上か下か矢印の描画
    {
        if (selectedIndex == 0)
        {
            restartText.text = "> Restart";
            titleText.text = "  Title";
        }
        else
        {
            restartText.text = "  Restart";
            titleText.text = "> Title";
        }

    }

    private void Select()//選んだ時の動き
    {
        if (selectedIndex == 0)
        {
            Restart();
        }
        else
        {
            GoToTitle();
        }
    }

    private void Restart()
    {
        Time.timeScale = 1f;//動き出す

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    private void GoToTitle()
    {
        SceneManager.LoadScene("Title");

        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        // Time.timeScaleが0のまま残らないようにする
        Time.timeScale = 1f;
    }
}
