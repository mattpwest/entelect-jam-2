﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLevelExit : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D collision) {
        Manager.Instance().ToMenuForNextLevel();
    }
}
