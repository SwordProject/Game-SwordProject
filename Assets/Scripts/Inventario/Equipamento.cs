using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipamento : PainelBase {

    public List<GameObject> listaSlots = new List<GameObject>();
    private GameObject slotEquip;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void setSizePainel(Vector2 tamanho)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = tamanho;
        float distSlot = tamanho.x * 0.03F;
        float sizeSlot = tamanho.x * 0.82F / 5;
        for (int i = 0; i < listaSlots.Count; i++)
        {
            listaSlots[i].GetComponent<SlotEquipamento>().setSizeSlot(sizeSlot);
        }
        float position = sizeSlot + distSlot;
        listaSlots[0].GetComponent<RectTransform>().localPosition = new Vector2(0, position);
        listaSlots[1].GetComponent<RectTransform>().localPosition = new Vector2(-position, 0);
        listaSlots[2].GetComponent<RectTransform>().localPosition = new Vector2(position, 0);
        listaSlots[3].GetComponent<RectTransform>().localPosition = new Vector2(-position, -position);
        listaSlots[4].GetComponent<RectTransform>().localPosition = new Vector2(position, -position);
    }

    public override void cancelar()
    {

    }

    public void getItemInventario(GameObject slot)
    {
        slotEquip = slot;
        Inventario painelInv = GameObject.Find("Inventario").GetComponent<Inventario>();
        if (painelInv.getIndex() > 0)
        {
            GameObject.Find("TabMenu").GetComponent<TabMenu>().activeTab(1);
            painelInv.setReturnItem(true);
        }
        else
        {
            Debug.Log("Inventario Vazio!");
        }
    }
    public void setItem(GameObject item)
    {
        slotEquip.GetComponent<SlotEquipamento>().addItemSlot(item,1);
        GameObject.Find("TabMenu").GetComponent<TabMenu>().activeTab(0);
    }
}
