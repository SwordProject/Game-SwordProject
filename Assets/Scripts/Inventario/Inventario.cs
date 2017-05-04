using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : PainelBase {

    private int index;
    public int quantidadeSlots;
    public GameObject slots;
    private GameObject painelItens;
    private bool returnItem = false;

    // Use this for initialization
    void Start () {
        index = 0;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public bool addItens(GameObject item, int qnt)
    {
        for(int i=0; i<= index; i++)
        {
            SlotInventario slot = GameObject.Find("Slot "+i).GetComponent<SlotInventario>();
            if (slot.item == item)
            {
                slot.addQtdSlot(qnt);
                return true;
            }
        }
        if(index <= 8)
        {
            SlotInventario slot = GameObject.Find("Slot " + index).GetComponent<SlotInventario>();
            slot.addItemSlot(item, qnt);
            index++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void removeItem(int posicoItem)
    {
        SlotInventario slot = GameObject.Find("Slot " + posicoItem).GetComponent<SlotInventario>();
        if (slot.removeItemSlot())
        {
            if (posicoItem < index)
            {
                index--;
                for (int i = posicoItem; i < index; i++)
                {
                    SlotInventario slot1 = GameObject.Find("Slot " + i).GetComponent<SlotInventario>();
                    SlotInventario slot2 = GameObject.Find("Slot " + (i + 1)).GetComponent<SlotInventario>();
                    slot1.addItemSlot(slot2.item, slot2.quantidade);
                }
                SlotInventario fimSlot = GameObject.Find("Slot " + index).GetComponent<SlotInventario>();
                fimSlot.delItemSlot();
            }
        }
    }

    public override void setSizePainel(Vector2 tamanho)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = tamanho;
        painelItens = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        tamanho.x -= 17;
        float distSlot = tamanho.x * 0.03F;
        float auxX = distSlot;
        float auxY = -tamanho.y * 0.2F;
        float sizeSlot = tamanho.x * 0.82F / 5;
        int colunas = 0;
        for (int i = 0; i < quantidadeSlots; i++)
        {
            GameObject slot = Instantiate(slots, painelItens.transform);
            slot.name = "Slot " + i;
            slot.GetComponent<RectTransform>().localPosition = new Vector2(auxX, auxY);
            slot.GetComponent<SlotInventario>().setSizeSlot(sizeSlot);
            slot.GetComponent<SlotInventario>().id=i;
            auxX += sizeSlot + distSlot;
            colunas++;
            if (colunas > 4)
            {
                auxY -= sizeSlot + distSlot;
                auxX = distSlot;
                colunas = 0;
            }
        }
        painelItens.GetComponent<RectTransform>().sizeDelta = new Vector2(0, auxY * -1);
    }

    public override void cancelar()
    {
        returnItem = false;
    }

    public void setReturnItem(bool returnItem)
    {
        this.returnItem = returnItem;
    }
    public bool getReturnItem()
    {
        return returnItem;
    }
    public int getIndex()
    {
        return index;
    }
    public void setItemEquipamento(GameObject item,int posicao)
    {
        GameObject.Find("Equipamento").GetComponent<Equipamento>().setItem(item);
        removeItem(posicao);
    }
}
