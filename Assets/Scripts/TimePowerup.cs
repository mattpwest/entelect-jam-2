using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerup : MonoBehaviour {

    public int Seconds = 15;
    private Timer Timer;

    void OnTriggerEnter2D(Collider2D collision) {
        // Cheap & dirty - if anything hits us add time
        Timer.AddTime(0, Seconds);
        Destroy(this.gameObject);
    }

    void Start () {
        Timer = FindObjectOfType<Timer>();
	}
}
