using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour {

    public int id;
    private bool emUso;
    private Image slotIcon;
    private Text slotTexto;

    // Use this for initialization
    void Start () {
        slotIcon = gameObject.transform.GetChild(0).GetComponent<Image>();
        slotTexto = gameObject.transform.GetChild(1).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if(emUso)
        {
            slotIcon.enabled = true;
            slotTexto.enabled = true;
        }
        else
        {
            slotIcon.enabled = false;
            slotTexto.enabled = false;
        }
	}

    public void setSlot(GameObject item, int qnt)
    {
        if (item != null)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            gameObject.transform.GetChild(1).GetComponent<Text>().text = qnt.ToString();
            name = item.name;
            emUso = true;
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = null;
            gameObject.transform.GetChild(1).GetComponent<Text>().text = "0";
            name = "";
            emUso = false;
        }
    }

    public void setSizeSlot(float size)
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(size - 10, size - 10);
        gameObject.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(size - 10, size - 10);
    }

    public bool getUso()
    {
        return emUso;
    }
}
