using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

    private float timeEspera = 5;
    private float timeDecorrido;

	// Use this for initialization
	void Start () {
        timeDecorrido = 0;
    }
	
	// Update is called once per frame
	void Update () {
        timeDecorrido += Time.deltaTime;
        if (timeDecorrido >= timeEspera)
        {
            SceneManager.LoadScene("Menu Principal");
        }
	}
}
