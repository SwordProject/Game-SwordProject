using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelScript : MonoBehaviour {

    public List<GameObject> listaSlots = new List<GameObject>();
    public int pulodeSlot;
    private int index = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            index -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            index += 1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(index - pulodeSlot >= 0)
                index -= pulodeSlot;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (index + pulodeSlot < listaSlots.Count)
                index += pulodeSlot;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            getEvento(listaSlots[index].name);
        }
        if (index < 0)
            index = 0;
        if (index > listaSlots.Count-1)
            index = listaSlots.Count - 1;
        enterSlot();
    }
    
    public void enterSlot()
    {
        for(int i = 0; i < listaSlots.Count; i++)
        {
            if (i != index)
                listaSlots[i].GetComponent<Image>().color = Color.white;
            else
                listaSlots[i].GetComponent<Image>().color = Color.green;
        }
    }

    private void getEvento(string name)
    {
        ControleMenuPause controle = GameObject.Find("GameController").GetComponent<ControleMenuPause>();
        switch (name)
        {
            case "ButtonContinue":
                controle.setActiveMenu();
                break;
            case "ButtonSair":
                controle.retornarMenuPrincipal();
                break;
        }
    }
}
