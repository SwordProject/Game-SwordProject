using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControleMenuPause : MonoBehaviour {

    public Transform pauseMenu;
    public Transform player;
    public int limiteSlots;
    private bool isActive = false;
    public List<itemInventario> listaItens = new List<itemInventario>();

    public struct itemInventario
    {
        public GameObject item;
        public int quantidade;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setActiveMenu();
        }
    }
    
    public bool addAoInventario(GameObject item, int qnt)
    {

        itemInventario novoItem = new itemInventario();
        novoItem.item = item;
        novoItem.quantidade = qnt;
        for (int i = 0; i < listaItens.Count; i++)
        {
            if (listaItens[i].item == item)
            {
                novoItem.quantidade = listaItens[i].quantidade + qnt;
                listaItens[i] = novoItem;
                return true;
            }
        }
        if (listaItens.Count < limiteSlots)
        {
            listaItens.Add(novoItem);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void retornarMenuPrincipal()
    {
        SceneManager.LoadScene("Menu Principal");
    }

    public void setActiveMenu()
    {
        isActive = !isActive;
        player.gameObject.GetComponent<PlayerController>().enabled = !isActive;
        if (isActive)
        {
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.GetComponent<LayoutMenuPause>().setisActiveTab(0);
            pauseMenu.gameObject.SetActive(false);
        }
    }
}
