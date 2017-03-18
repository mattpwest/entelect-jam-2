using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Transform winText;
    public Transform loseText;
    public Transform levelText;

	void Start () {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);

        if (Manager.Instance().IsWinner()) {
            winText.gameObject.SetActive(true);
            Manager.Instance().Level = 1;
        } else if (Manager.Instance().IsLoser()) {
            loseText.gameObject.SetActive(true);
        }

        levelText.GetComponent<Text>().text = "Level " + Manager.Instance().Level;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) {
            Manager.Instance().ToLevel();
        }
    }
}
