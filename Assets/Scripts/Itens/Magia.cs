using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magia : ItensBase {

    public float velocidade = 7;

    // Use this for initialization
    void Start () {
        name = "Magia Negra";
        tipo = tipoItem.Arremessavel;
        Destroy(gameObject, 5);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,0,-6));
        if (Vector3.Equals(direcao, Vector3.zero))
        {
            if (gameObject.transform.localScale.x >= 0)
                direcao = Vector3.right;
            else
                direcao = Vector3.left;
        }
            transform.Translate(direcao * Time.deltaTime * velocidade, Space.World);
        raycast();
    }

    private void raycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.35f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.GetComponent<PlayerController>().addDano(15);
                Destroy(gameObject);
            }
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.GetComponent<Inimigos>().addDano(30);
                Destroy(gameObject);
            }
            if (hit.collider.gameObject.tag == "Arm")
            {
                Destroy(gameObject);
            }
        }
    }
}
