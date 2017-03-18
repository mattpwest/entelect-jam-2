using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager {

    private static Manager instance;

    protected Manager() {
        Level = 1;
    }

    public static Manager Instance() {
        if (instance == null) {
            instance = new Manager();
        }

        return instance;
    }

    public int Level { get; set; }
    private int maxLevel = 2;
    private bool loser;

    public bool IsLoser() {
        return loser;
    }

    public bool IsWinner() {
        return Level > maxLevel;
    }

    public void ToLost() {
        loser = true;
        SceneManager.LoadScene("Victory");
    }

    public void ToMenuForNextLevel() {
        Level++;
        loser = false;
        SceneManager.LoadScene("Victory");
    }

    public void ToLevel() {
        SceneManager.LoadScene("Level" + Level);
    }
}
