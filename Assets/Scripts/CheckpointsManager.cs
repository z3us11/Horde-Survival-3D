using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour
{
    [SerializeField] List<Checkpoint> checkpointList;
    int currentCheckpoint;
    [SerializeField] Timer timer;


    internal void CheckpointReached()
    {
        timer.UpdateTimer(currentCheckpoint);
        
        currentCheckpoint++;
        if (currentCheckpoint >= checkpointList.Count)
        {
            currentCheckpoint = 0;
            timer.UpdateBestTimer();
        }
        checkpointList[currentCheckpoint].gameObject.SetActive(true);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
