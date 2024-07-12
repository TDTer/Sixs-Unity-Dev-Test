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

}
