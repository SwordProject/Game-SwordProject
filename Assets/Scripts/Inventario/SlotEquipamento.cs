using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotEquipamento : MonoBehaviour {

    public GameObject item;
    public GameObject painel;
    public int quantidade;
    private Image itemImage;
    private Text itemText;

    // Use this for initialization
    void Start()
    {
        itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        itemText = gameObject.transform.GetChild(1).GetComponent<Text>();
        painel = GameObject.Find("Equipamento");
    }

    // Update is called once per frame
    void Update()
    {
        if (item == null)
        {
            itemText.enabled = false;
            itemImage.enabled = false;
        }
        else
        {
            itemText.enabled = true;
            itemImage.enabled = true;
        }
    }


    public void addItemSlot(GameObject item, int qtd)
    {
        this.item = item;
        this.quantidade = qtd;
        itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        itemText.text = qtd.ToString();
    }

    public void removeItemSlot()
    {
        this.item = null;
        this.quantidade = 0;
        itemImage.sprite = null;
        itemText.text = "0";
    }

    public void addQtdSlot(int qtd)
    {
        quantidade += qtd;
        itemText.text = quantidade.ToString();
    }

    public void enterSlot()
    {
        gameObject.GetComponent<Image>().color = Color.green;
    }

    public void selectSlot()
    {
        painel.GetComponent<Equipamento>().getItemInventario(gameObject);
    }

    public void exitSlot()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public void setSizeSlot(float size)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(size - 10, size - 10);
        gameObject.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(size - 10, size - 10);
    }
}
