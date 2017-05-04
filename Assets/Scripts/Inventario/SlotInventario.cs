using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInventario : MonoBehaviour {

    public GameObject item;
    public GameObject painel;
    public int quantidade;
    private Image itemImage;
    private Text itemText;
    public int id;

    // Use this for initialization
    void Start () {
        itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        itemText = gameObject.transform.GetChild(1).GetComponent<Text>();
        painel = GameObject.Find("Inventario");
    }
	
	// Update is called once per frame
	void Update () {
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

    public bool removeItemSlot()
    {
        if (quantidade == 1)
        {
            delItemSlot();
            return true;
        }
        else
        {
            this.quantidade -= 1;
            itemText.text = quantidade.ToString();
            return false;
        }
    }

    public void delItemSlot()
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
        if(item != null)
            gameObject.GetComponent<Image>().color = Color.green;
    }

    public void exitSlot()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }
    public void selectSlot()
    {
        if (painel.GetComponent<Inventario>().getReturnItem() && item!=null)
        {
            painel.GetComponent<Inventario>().setItemEquipamento(item,id);
        }
        else
        {

        }
    }

    public void setSizeSlot(float size)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(size - 10, size - 10);
        gameObject.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(size - 10, size - 10);
    }
}
