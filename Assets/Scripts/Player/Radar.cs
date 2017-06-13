using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            player.setInimigo(col.transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            player.setInimigo(Vector3.zero);
        }
    }
}
