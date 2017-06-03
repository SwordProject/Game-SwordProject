using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocaoMP : ItensBase {

    public PlayerController player;

    // Use this for initialization
    void Start () {
        name = "Poção energia";
        tipo = tipoItem.Consumivel;
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.addEnergia(25);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
