using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutMenuPause : MonoBehaviour {

    public List<GameObject> listaPaineis = new List<GameObject>();
    public List<GameObject> listaTabs = new List<GameObject>();
    public GameObject slot;
    private ControleMenuPause controller;
    private int isActiveTab=0;

    // Use this for initialization
    void Start () {
        controller = GameObject.Find("GameController").GetComponent<ControleMenuPause>();
        criarSlots();
        activeTab(isActiveTab);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isActiveTab -= 1;
            if(isActiveTab<0)
                isActiveTab = 0;
            activeTab(isActiveTab);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isActiveTab += 1;
            if (isActiveTab > listaPaineis.Count-1)
                isActiveTab = listaPaineis.Count-1;
            activeTab(isActiveTab);
        }
    }

    private void criarSlots()
    {
        GameObject PainelConteudo = GameObject.Find("PainelInventario").transform.GetChild(0).GetChild(0).gameObject;
        float tamanhoX = GameObject.Find("PainelInventario").GetComponent<RectTransform>().sizeDelta.x;
        tamanhoX -= 17;
        float espaçoEntreSlot = tamanhoX * 0.03F;
        float tamanhoSlot = tamanhoX * 0.85F / 4;
        float auxX = espaçoEntreSlot + tamanhoSlot / 2;
        float auxY = -(espaçoEntreSlot + tamanhoSlot / 2);
        int colunas = 0;
        List<GameObject> listaSlots = new List<GameObject>();
        for (int i = 0; i < controller.limiteSlots; i++)
        {
            listaSlots.Add(Instantiate(slot, PainelConteudo.transform));
            listaSlots[i].name = "Slot " + i;
            listaSlots[i].GetComponent<RectTransform>().localPosition = new Vector2(auxX, auxY);
            listaSlots[i].GetComponent<SlotScript>().setSizeSlot(tamanhoSlot);
            listaSlots[i].GetComponent<SlotScript>().id = i;
            auxX += tamanhoSlot + espaçoEntreSlot;
            colunas++;
            if (colunas >= 4)
            {
                auxY -= tamanhoSlot + espaçoEntreSlot;
                auxX = espaçoEntreSlot + tamanhoSlot / 2;
                colunas = 0;
            }
        }
        auxY -= tamanhoSlot/2 + espaçoEntreSlot;
        GameObject.Find("PainelInventario").GetComponent<PainelScript>().listaSlots = listaSlots;
        PainelConteudo.GetComponent<RectTransform>().sizeDelta = new Vector2(0, auxY*-1);
    }

    public void activeTab(int tab)
    {
        for (int i = 0; i < listaPaineis.Count; i++)
        {
            if (i != tab)
            {
                listaPaineis[i].gameObject.SetActive(false);
                listaTabs[i].GetComponent<Image>().color = Color.grey;
            }
            else
            {
                listaPaineis[tab].gameObject.SetActive(true);
                listaTabs[tab].GetComponent<Image>().color = Color.white;
            }
        }
        switch (listaPaineis[tab].name)
        {
            case "PainelInventario":
                getListaIventario();
                break;
        }
    }

    private void getListaIventario()
    {
        for(int i = 0; i < controller.listaItens.Count; i++)
        {
            SlotScript meuSlot = GameObject.Find("Slot " + i).GetComponent<SlotScript>();
            meuSlot.setSlot(controller.listaItens[i].item, controller.listaItens[i].quantidade);
        }
    }

    public void setisActiveTab(int valor)
    {
        isActiveTab = valor;
        activeTab(isActiveTab);
    }
}
