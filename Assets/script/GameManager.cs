using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글턴을 할당할 전역 변수

    public bool isGameover = false;

    public TextMeshProUGUI scoreText;

    public GameObject gameoverUI;

    private int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재함");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 게임오버 상태에서 마우스 왼쪽 클릭하면 현재 씬 재시작
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void AddScore(int newScore)
    {

        if (!isGameover)
        {
            score += newScore;
            scoreText.text = "Score : " + score;
        }
    }

    public void OnPlayerDead()
    {
        isGameover = true;

        gameoverUI.SetActive(true);
    
    }

}
