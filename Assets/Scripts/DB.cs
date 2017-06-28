using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB : MonoBehaviour {

    public static DB instance;

    public List<itemDB> listaItensDB = new List<itemDB>();
    public List<itemDB> listaEquipadosDB = new List<itemDB>();

    public float vitalidade = 100;
    public float energia = 100;

    private PlayerController player;
    private GameController gameController;

    public struct itemDB
    {
        public GameObject item;
        public int quantidade;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            itemDB itemVazio = new itemDB();
            itemVazio.item = null;
            itemVazio.quantidade = 0;
            listaEquipadosDB.Insert(i, itemVazio);
        }
        for (int i = 0; i < 25; i++)
        {
            itemDB itemVazio = new itemDB();
            itemVazio.item = null;
            itemVazio.quantidade = 0;
            listaItensDB.Insert(i, itemVazio);
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
        listaEquipadosDB.Clear();
        listaItensDB.Clear();
        for(int i = 0; i < gameController.listaItens.Count; i++)
        {
            itemDB newItem = new itemDB();
            newItem.item = gameController.listaItens[i].item;
            newItem.quantidade = gameController.listaItens[i].quantidade;
            listaItensDB.Add(newItem);
        }
        for (int i = 0; i < gameController.itensEquipados.Count; i++)
        {
            itemDB newItem = new itemDB();
            newItem.item = gameController.itensEquipados[i].item;
            newItem.quantidade = gameController.itensEquipados[i].quantidade;
            listaEquipadosDB.Add(newItem);
        }
    }
}
