using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StopWatch : MonoBehaviour
{
    Stopwatch stopWatch = new Stopwatch();
    int savedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        stopWatch.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public System.TimeSpan Stop()
    {
        stopWatch.Stop();
        return stopWatch.Elapsed;
    }

    public void PauseStopWatch()
    {
        stopWatch.Stop();
    }

    public void ResumeStopWatch()
    {
        stopWatch.Start();
    }

    public int SaveTime()
    {
        return stopWatch.Elapsed.Minutes;
    }

    public void SetSavedTime(int _time)
    {
        savedTime = _time;
    }
}
