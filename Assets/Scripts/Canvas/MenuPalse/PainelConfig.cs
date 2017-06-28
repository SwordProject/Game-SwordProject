using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelConfig : MonoBehaviour {
    public List<GameObject> listaSlots = new List<GameObject>();
    private int index = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (index - 1 >= 0)
                index -= 1;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (index + 1 < listaSlots.Count)
                index += 1;
        }
        if (Input.GetButtonDown("Submit"))
        {
            getEvento(listaSlots[index].name);
        }
        if (index < 0)
            index = 0;
        if (index > listaSlots.Count - 1)
            index = listaSlots.Count - 1;
        enterSlot();
    }

    public void enterSlot()
    {
        for (int i = 0; i < listaSlots.Count; i++)
        {
            if (i != index)
                listaSlots[i].GetComponent<Image>().color = Color.white;
            else
                listaSlots[i].GetComponent<Image>().color = Color.yellow;
        }
    }

    private void getEvento(string name)
    {
        GameController controle = GameObject.Find("GameController").GetComponent<GameController>();
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
