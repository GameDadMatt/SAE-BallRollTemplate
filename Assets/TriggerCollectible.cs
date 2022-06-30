using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollectible : MonoBehaviour
{
    private bool isTriggered;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isTriggered)
        {
            isTriggered = true;
            gameObject.SetActive(false);
            GameManagement.instance.CollectibleTriggered();
        }
    }
}
