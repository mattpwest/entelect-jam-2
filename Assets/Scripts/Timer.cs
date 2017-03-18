using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public int Minutes = 0;
    public int Seconds = 15;

    private Text timeLeftText;

    private int TotalSeconds = 0;
    private float TimeAccumulated = 0.0f;

	void Start () {
        timeLeftText = GetComponent<Text>();

        TotalSeconds = Minutes * 60 + Seconds;
	}
	
	void Update () {
        TimeAccumulated += Time.deltaTime;

        if (TimeAccumulated >= 1.0f) {
            TotalSeconds--;
            TimeAccumulated -= 1.0f;
        }

        if (TotalSeconds <= 0) {
            Manager.Instance().ToLost();
        }

        int minutes = TotalSeconds / 60;
        int seconds = TotalSeconds % 60;
        string text = minutes + ":";
        if (seconds >= 10) {
            text += seconds;
        } else {
            text += "0" + seconds;
        }
        timeLeftText.text = text;
	}

    public void AddTime(int minutes, int seconds) {
        TotalSeconds += minutes * 60 + seconds;
    }
}
