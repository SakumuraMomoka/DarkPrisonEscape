using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private float time;
    private int score = 0;

    void Update()
    {

    }

    private void timeManager()
    {
        timeText.text = "" + time;//残り時間の表示

        time -= Time.deltaTime;

        if (time <= 0)
        {
            SceneManager.LoadScene("ScoreScene");
        }
    }

    private void scoreManager()
    {

    }
}

