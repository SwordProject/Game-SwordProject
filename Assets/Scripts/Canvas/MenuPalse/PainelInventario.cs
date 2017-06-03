using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelInventario : MonoBehaviour {
    private List<GameObject> listaSlots = new List<GameObject>();
    private int index = 0;
    public GameObject slot;
    private GameController gameController;
    public GameObject painelConfirme;
    private int indexPainel = 0;
    public List<GameObject> listaButtonPainel = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        criarSlots();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i< gameController.listaItens.Count;i++)
        {
            listaSlots[i].GetComponent<SlotScript>().setSlot(gameController.listaItens[i].item, gameController.listaItens[i].quantidade);
        }

        if (!painelConfirme.activeSelf)
        {
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
                if (index - 4 >= 0)
                {
                    index -= 4;
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (index + 4 < listaSlots.Count)
                {
                    index += 4;
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (gameController.getItemEquipamento() == -1)
                    criarPainelConfirme();
                else
                    gameController.setItemIventario(index);
            }
            if (index < 0)
                index = 0;
            if (index > listaSlots.Count - 1)
                index = listaSlots.Count - 1;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                indexPainel--;
                if (indexPainel < 0)
                    indexPainel = 2;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                indexPainel++;
                if (indexPainel > 2)
                    indexPainel = 0;
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                enterPainel();
            }
            for (int i = 0; i < listaButtonPainel.Count; i++)
            {
                if (i != indexPainel)
                    listaButtonPainel[i].GetComponent<Image>().color = Color.white;
                else
                    listaButtonPainel[i].GetComponent<Image>().color = Color.yellow;
            }
        }
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

    public void criarSlots()
    {
        GameObject PainelConteudo = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        float tamanhoX = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        tamanhoX -= 17;
        float espaçoEntreSlot = tamanhoX * 0.03F;
        float tamanhoSlot = tamanhoX * 0.85F / 4;
        float auxX = espaçoEntreSlot + tamanhoSlot / 2;
        float auxY = -(espaçoEntreSlot + tamanhoSlot / 2);
        int colunas = 0;
        for (int i = 0; i < gameController.limiteSlots; i++)
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
        auxY -= tamanhoSlot / 2 + espaçoEntreSlot;
        PainelConteudo.GetComponent<RectTransform>().sizeDelta = new Vector2(0, auxY * -1);
    }
    
    private void criarPainelConfirme()
    {
        SlotScript slot = listaSlots[index].GetComponent<SlotScript>();
        if (slot.getUso())
        {
            painelConfirme.SetActive(true);
            painelConfirme.transform.position = listaSlots[index].transform.position;
            painelConfirme.transform.GetChild(0).GetComponent<Text>().text = gameController.listaItens[index].item.name;
        }
    }

    private void enterPainel()
    {
        switch (indexPainel)
        {
            case 0:
                gameController.setItemIventario(index);
                painelConfirme.SetActive(false);
                break;
            case 1:
                gameController.soltarItem(index);
                painelConfirme.SetActive(false);
                break;
            case 2:
                painelConfirme.SetActive(false);
                break;
        }
    }
}