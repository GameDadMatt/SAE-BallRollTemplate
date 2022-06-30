using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManagement : MonoBehaviour
{
    //Make PlayerManagement a singleton
    public static GameManagement instance;

    public TextMeshProUGUI finishText;
    public TextMeshProUGUI waypointText;
    public TextMeshProUGUI scoreText;

    private Vector3 respawnPosition;
    private int totalWaypoints = 0;
    private int currentWaypoints;
    private int currentScore = 0;

    private BallMovement player;

    private void OnEnable()
    {
        instance = this;
        waypointText.text = "0 / 0";
        scoreText.text = "0";
        finishText.gameObject.SetActive(false);
    }

    public void RegisterPlayer(BallMovement p)
    {
        player = p;
        respawnPosition = player.transform.position;
    }

    public void SetRespawn(Vector3 newPos)
    {
        respawnPosition = newPos;
    }

    public void RespawnPlayer()
    {
        player.Respawn(respawnPosition);
    }

    public int RegisterWaypoint()
    {
        totalWaypoints++;
        UpdateText();
        return totalWaypoints;
    }

    public void WaypointTriggered()
    {
        currentWaypoints++;
        UpdateText();
        if(currentWaypoints >= totalWaypoints)
        {
            FinishGame();
        }
    }

    public void CollectibleTriggered()
    {
        currentScore++;
        UpdateText();
    }

    private void UpdateText()
    {
        waypointText.text = currentWaypoints + " / " + totalWaypoints;
        scoreText.text = currentScore.ToString();
    }

    public void FinishGame()
    {
        finishText.gameObject.SetActive(true);
    }
}
