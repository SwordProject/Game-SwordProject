using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour {

    public List<GameObject> listaPaineis = new List<GameObject>();
    public List<GameObject> listaTabs = new List<GameObject>();
    private int isActiveTab=0;

    // Use this for initialization
    void Start () {
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
                listaTabs[tab].GetComponent<Image>().color = Color.yellow;
            }
        }
    }

    public void setisActiveTab(int valor)
    {
        isActiveTab = valor;
        activeTab(isActiveTab);
    }
}
