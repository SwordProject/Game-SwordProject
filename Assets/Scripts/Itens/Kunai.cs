using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : ItensBase {

    public float velocidade = 10;

	// Use this for initialization
	void Start () {
        name = "Kunai";
        tipo = tipoItem.Arremessavel;
        Destroy(gameObject, 5);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 direcaoFrente;
        if (gameObject.transform.localScale.x >= 0)
            direcaoFrente = Vector3.right;
        else
            direcaoFrente = Vector3.left;
        transform.Translate(direcaoFrente * velocidade * Time.deltaTime);
        if (!gameObject.GetComponent<BoxCollider2D>().enabled)
            raycast();
    }

    private void raycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,0.35f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.GetComponent<PlayerController>().addDano(10);
                Destroy(gameObject);
            }
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.GetComponent<Inimigos>().addDano(15);
                Destroy(gameObject);
            }
            if (hit.collider.gameObject.tag == "Arm")
                Destroy(gameObject);
        }
    }
}
