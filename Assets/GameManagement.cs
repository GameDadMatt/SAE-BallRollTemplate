using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManagement : MonoBehaviour
{
    //Make PlayerManagement a singleton
    public static GameManagement instance; 

    private Vector3 respawnPosition;
    private int totalWaypoints = 0;
    private int currentWaypoints;

    private BallMovement player;

    private void OnEnable()
    {
        instance = this;
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
        return totalWaypoints;
    }

    public void WaypointTriggered()
    {
        currentWaypoints++;
        if(currentWaypoints >= totalWaypoints)
        {
            FinishGame();
        }
    }

    public void FinishGame()
    {
        Debug.Log("Game complete!");
    }
}
