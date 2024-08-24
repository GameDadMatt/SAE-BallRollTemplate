using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType { None, Finish, Checkpoint, Waypoint, Kill }

public class TriggerArea : MonoBehaviour
{
    public TriggerType triggerType = TriggerType.None;
    private int waypointID = 0;
    private bool isTriggered = false;

    public void Start()
    {
        if (triggerType == TriggerType.Waypoint)
        {
            waypointID = GameManagement.instance.RegisterWaypoint();
        }
        else if(triggerType == TriggerType.None)
        {
            Debug.LogError("A trigger has not had its type set and will do nothing when passed through.");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isTriggered)
        {
            isTriggered = true;
            switch (triggerType)
            {
                case TriggerType.None:
                    isTriggered = false;
                    break;
                case TriggerType.Finish:
                    TriggerFinish();
                    break;
                case TriggerType.Checkpoint:
                    TriggerCheckpoint();
                    break;
                case TriggerType.Waypoint:
                    TriggerWaypoint();
                    break;
                case TriggerType.Kill:
                    TriggerKill();
                    isTriggered = false;
                    break;
            }
        }
    }

    private void TriggerFinish()
    {
        GameManagement.instance.FinishGame();
    }

    private void TriggerCheckpoint()
    {
        GameManagement.instance.SetRespawn(this.transform.position);
    }

    private void TriggerWaypoint()
    {
        GameManagement.instance.WaypointTriggered();
    }

    private void TriggerKill()
    {
        GameManagement.instance.RespawnPlayer();
    }
}
