using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;

    public Text HighScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    public string playerName;

    public string bestPlayerName;

    public int highScore;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        highScore = GameManager.Instance.highScore;
        playerName = GameManager.Instance.playerName;
        bestPlayerName = GameManager.Instance.bestPlayerName;

        ScoreText.text = $"{playerName}\nScore : 0";
        HighScoreText.text = $"Best Score : {highScore}\nName: {bestPlayerName}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

    }


    //add point if brick destroyed
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{playerName}\nScore : {m_Points}";

        if(GameManager.Instance.highScore < m_Points) {UpdateHighScore();}
        
    }

    void UpdateHighScore(){
        GameManager.Instance.highScore = m_Points;
        GameManager.Instance.bestPlayerName = playerName;

        HighScoreText.text = $"Best Score : {m_Points}\nName: {playerName}";
        
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        GameManager.Instance.Save_Data();
    }
}
