using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour {

    public static DB instance;

    public List<itemLista> listaItens = new List<itemLista>();
    public List<itemLista> itensEquipados = new List<itemLista>();

    public float vitalidade = 100;
    public float energia = 100;

    private PlayerController player;
    private GameController gameController;

    public struct itemLista
    {
        public GameObject item;
        public int quantidade;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            for (int i = 0; i < 8; i++)
            {
                itemLista itemVazio = new itemLista();
                itemVazio.item = null;
                itemVazio.quantidade = 0;
                itensEquipados.Insert(i, itemVazio);
            }
            for (int i = 0; i < 25; i++)
            {
                itemLista itemVazio = new itemLista();
                itemVazio.item = null;
                itemVazio.quantidade = 0;
                listaItens.Insert(i, itemVazio);
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateDados()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        vitalidade = player.getVitalidade();
        energia = player.getEnergia();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        this.listaItens = gameController.listaItens;
        this.itensEquipados = gameController.itensEquipados;
    }
}
