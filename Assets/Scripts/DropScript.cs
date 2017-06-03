using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropScript : MonoBehaviour {

    public GameObject item = null;
    public int quantidade;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        if(item != null)
            gameObject.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<Transform>().localScale = new Vector3(2, 2, 2);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

    public void setDrop(GameObject item,int quantidade)
    {
        this.item = item;
        this.quantidade = quantidade;
        gameObject.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (gameController.addAoInventario(item,quantidade))
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventario cheio!");
            }
        }
    }

}
