using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabMenu : MonoBehaviour {

    private bool isActive = false;
    private List<GameObject> listaPaineis = new List<GameObject>();
    public List<GameObject> listaTabs = new List<GameObject>();
    private int isActiveTab=0;

    // Use this for initialization
    void Start () {
        float sizePainelX = getWScreen(0.3F);
        float sizePainelY = getHScreen(0.6F);
        float espaçoItem = sizePainelX * 0.03F;
        float sizeItem = sizePainelX * 0.82F / 6;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(sizePainelX, sizeItem + espaçoItem * 2);
        setSizePositionTabs(sizeItem, espaçoItem);
        gameObject.GetComponent<RectTransform>().localPosition = new Vector2(0, sizePainelY/2);
        listaPaineis.Add(gameObject.transform.GetChild(0).gameObject);
        listaPaineis.Add(gameObject.transform.GetChild(1).gameObject);
        setSizePositionPaineis(new Vector2(sizePainelX,sizePainelY),new Vector2(0, -(sizePainelY / 2)));
        activeTab(isActiveTab);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("e"))
            isActive = !isActive;
        if (Input.GetKeyDown("1"))
        {
            isActiveTab = 0;
            activeTab(isActiveTab);
        }
        if (Input.GetKeyDown("2"))
        {
            isActiveTab = 1;
            activeTab(isActiveTab);
        }
        if (isActive)
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        else
            gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    public float getWScreen(float x)
    {
        return Screen.width * x;
    }
    public float getHScreen(float y)
    {
        return Screen.height * y;
    }
    private void setSizePositionPaineis(Vector2 tamanho, Vector2 posicao)
    {
        for (int i = 0; i < listaPaineis.Count; i++)
        {
            listaPaineis[i].GetComponent<PainelBase>().setSizePainel(tamanho);
            listaPaineis[i].GetComponent<RectTransform>().localPosition = posicao;
        }
    }
    private void setSizePositionTabs(float tamanho, float espaco)
    {
        float auxX= espaco;
        for (int i = 0; i < listaTabs.Count; i++)
        {
            listaTabs[i].GetComponent<RectTransform>().sizeDelta = new Vector2(tamanho,tamanho);
            listaTabs[i].GetComponent<RectTransform>().localPosition = new Vector2(auxX, 0);
            auxX += tamanho + espaco;
        }
    }
    public void activeTab(int tab)
    {
        for (int i = 0; i < listaPaineis.Count; i++)
        {
            if (i != tab)
            {
                listaPaineis[i].GetComponent<PainelBase>().cancelar();
                listaPaineis[i].GetComponent<RectTransform>().localScale = Vector3.zero;
                listaTabs[i].GetComponent<Image>().color = Color.grey;
            }
            else
            {
                listaPaineis[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                listaTabs[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}
