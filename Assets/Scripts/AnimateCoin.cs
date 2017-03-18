using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCoin : MonoBehaviour {

    public float rotateRate = 90.0f;

    private float yRotate = 0;

	void Start () {
	}
	
	void Update () {
        yRotate += Time.deltaTime * rotateRate;
        transform.localRotation = Quaternion.Euler(0, yRotate, 0);
	}
}
