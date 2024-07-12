using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : Singleton<UI>
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject kickButton;
    public TextMeshProUGUI score;

    void Start()
    {
        OnInit();
        UpdateScore(0);
    }
    void Update()
    {
        kickButton.SetActive(player.IsCanKick);
    }

    public void KickButton()
    {
        player.Kick();
    }

    public void AutoKickButton()
    {
        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
        Ball farthestBall = spawnManager.farthestBall(player.transform);

        farthestBall.onKick();
    }

    public void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateScore(int scoreToAdd)
    {
        player.Score += scoreToAdd;
        score.text = $"Score: {player.Score}";
    }

    protected RectTransform m_RectTransform;
    private Animator m_Animator;
    private float m_OffsetY = 0;
    protected void OnInit()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_Animator = GetComponent<Animator>();

        // xu ly tai tho
        float ratio = (float)Screen.height / (float)Screen.width;
        if (ratio > 2.1f)
        {
            Vector2 leftBottom = m_RectTransform.offsetMin;
            Vector2 rightTop = m_RectTransform.offsetMax;
            rightTop.y = -100f;
            m_RectTransform.offsetMax = rightTop;
            leftBottom.y = 0f;
            m_RectTransform.offsetMin = leftBottom;
            m_OffsetY = 100f;
        }
    }

}
