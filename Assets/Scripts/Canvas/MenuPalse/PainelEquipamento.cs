using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelEquipamento : MonoBehaviour {

    public List<GameObject> listaSlots = new List<GameObject>();
    public int pulodeSlot;
    private int index = 0;
    private GameController gameController;

    private bool axiX = true;
    private bool axiY = true;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < gameController.itensEquipados.Count; i++)
        {
            SlotScript slot = listaSlots[i].GetComponent<SlotScript>();
            slot.setSlot(gameController.itensEquipados[i].item, gameController.itensEquipados[i].quantidade);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && axiX)
        {
            index -= 1;
            axiX = false;
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && axiX)
        {
            index += 1;
            axiX = false;
        }
        if(Input.GetAxisRaw("Horizontal") == 0)
            axiX = true;

        if (Input.GetAxisRaw("Vertical") > 0 && axiY)
        {
            axiY = false;
            if (index - pulodeSlot >= 0)
                index -= pulodeSlot;
        }
        if (Input.GetAxisRaw("Vertical") < 0 && axiY)
        {
            axiY = false;
            if (index + pulodeSlot < listaSlots.Count)
                index += pulodeSlot;
        }
        if (Input.GetAxisRaw("Vertical") == 0)
            axiY = true;

        if (Input.GetButtonDown("Submit") && axiY)
        {
            gameController.setItemEquipamento(index);
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
                listaSlots[i].GetComponent<Image>().color = Color.yellow;
        }
    }
}
