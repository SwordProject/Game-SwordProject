using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropScript : MonoBehaviour {

    public GameObject item;
    public int quantidade;
    private ControleMenuPause inventario;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Transform>().localScale = new Vector3(3, 3, 3);
        inventario = GameObject.Find("GameController").GetComponent<ControleMenuPause>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (inventario.addAoInventario(item,quantidade))
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
