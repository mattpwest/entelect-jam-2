using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerup : MonoBehaviour {

    private Timer Timer;

    void OnTriggerEnter2D(Collider2D collision) {
        // Cheap & dirty - if anything hits us add time
        Timer.AddTime(0, 15);
        Destroy(this.gameObject);
    }

    void Start () {
        Timer = FindObjectOfType<Timer>();
	}
}
