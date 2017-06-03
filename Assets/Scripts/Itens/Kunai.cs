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
        Vector3 direcao;
        if (gameObject.transform.localScale.x >= 0)
            direcao = Vector3.right;
        else
            direcao = Vector3.left;
        transform.Translate(direcao * velocidade * Time.deltaTime);
        raycast();
    }

    private void raycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,0.35f);
        if (hit.collider != null)
        {
            velocidade = 0;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.GetComponent<PlayerController>().addDano(25);
                Destroy(gameObject);
            }
        }
    }
}
