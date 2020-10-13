//This script makes it so an object destructs if the GameTime is not between startTime and endTime

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTime : MonoBehaviour {
    public int startTime;
    public int endTime;
    // Start is called before the first frame update
    void Start () {
        if (TimeTrack.GameTime >= startTime & TimeTrack.GameTime <= endTime) { } else {
            Destroy (gameObject);
        }

    }

    // Update is called once per frame
    void Update () {

    }
}