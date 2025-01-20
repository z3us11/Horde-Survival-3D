using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    CheckpointsManager checkpoints;

    private void Awake()
    {
        checkpoints = FindAnyObjectByType<CheckpointsManager>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            checkpoints.CheckpointReached();
        }
    }
}
