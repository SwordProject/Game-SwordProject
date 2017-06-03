using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocaoHP : ItensBase {

    public PlayerController player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        name = "Poção vitalidade";
        tipo = tipoItem.Consumivel;
        player.addVitalidade(50);
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}