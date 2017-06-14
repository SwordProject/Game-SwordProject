using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject prefabeDrop;
    public Transform pauseMenu;
    public Transform player;
    public int limiteSlots;
    private bool isActive = false;
    public List<itemLista> listaItens = new List<itemLista>();
    public List<itemLista> itensEquipados = new List<itemLista>();

    //Variaveis Game over
    public Transform gameOverScreen;
    public float tempoParaReiniciar = 3f;
    float restartTimer;

    //Selecionar itens
    private int indexIven = -1;
    private int indexEqui = -1;

    public struct itemLista
    {
        public GameObject item;
        public int quantidade;
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;
        for (int i = 0; i < 8; i++)
        {
            itemLista itemVazio = new itemLista();
            itemVazio.item = null;
            itemVazio.quantidade = 0;
            itensEquipados.Insert(i, itemVazio);
        }
        for (int i = 0; i < limiteSlots; i++)
        {
            itemLista itemVazio = new itemLista();
            itemVazio.item = null;
            itemVazio.quantidade = 0;
            listaItens.Insert(i, itemVazio);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setActiveMenu();
        }

        if (indexIven!=-1 && indexEqui != -1)
        {
            GameObject novoItem = itensEquipados[indexEqui].item;
            int novaQuant = itensEquipados[indexEqui].quantidade;
            itensEquipados[indexEqui] = listaItens[indexIven];
            itemLista itemNull = new itemLista();
            itemNull.item = null;
            itemNull.quantidade = 0;
            listaItens[indexIven] = itemNull;
            addAoInventario(novoItem, novaQuant);
            indexIven = -1;
            indexEqui = -1;
        }
        gameOver();
    }
    
    public bool addAoInventario(GameObject item, int qnt)
    {
        itemLista novoItem = new itemLista();
        novoItem.item = item;
        novoItem.quantidade = qnt;
        for (int i = 0; i < itensEquipados.Count; i++)
        {
            if (itensEquipados[i].item == item)
            {
                novoItem.quantidade = itensEquipados[i].quantidade + qnt;
                itensEquipados[i] = novoItem;
                return true;
            }
        }
        for (int i = 0; i < listaItens.Count; i++)
        {
            if (listaItens[i].item == item)
            {
                novoItem.quantidade = listaItens[i].quantidade + qnt;
                listaItens[i] = novoItem;
                return true;
            }
        }
        for (int i = 0; i < listaItens.Count; i++)
        {
            if (listaItens[i].item == null)
            {
                listaItens.RemoveAt(i);
                listaItens.Insert(i, novoItem);
                return true;
            }
        }
        return false;
    }

    public void soltarItem(int index)
    {
        GameObject objetoSolto = Instantiate(prefabeDrop,new Vector3(player.transform.position.x-1.2f, player.transform.position.y+0.2f),player.transform.rotation);
        objetoSolto.GetComponent<DropScript>().setDrop(listaItens[index].item, listaItens[index].quantidade);
        itemLista novoItem = new itemLista();
        novoItem.item = null;
        novoItem.quantidade = 0;
        listaItens[index] = novoItem;
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
            pauseMenu.GetComponent<MenuPause>().setisActiveTab(0);
            pauseMenu.gameObject.SetActive(false);
            indexIven = -1;
            indexEqui = -1;
        }
    }

    public void gameOver()
    {
        if (player.GetComponent<PlayerController>().getVitalidade() <= 0)
        {
            if (gameOverScreen.gameObject.activeSelf == false)
            {
                gameOverScreen.gameObject.SetActive(true);
            }
            restartTimer += Time.deltaTime;
            GameObject.Find("TimeBar").GetComponent<RectTransform>().sizeDelta = new Vector2(380 * restartTimer / tempoParaReiniciar, 10);
            if (restartTimer >= tempoParaReiniciar)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void setItemEquipamento(int index)
    {
        if (indexIven != -1)
        {
            if (validaEquipamento(index))
                indexEqui = index;
            else
                Debug.Log("Tipo de item não compativel com slot selecionado!");
        }
        else
        {
            indexEqui = index;
            pauseMenu.GetComponent<MenuPause>().setisActiveTab(2);
        }
    }

    public void setItemIventario(int index)
    {
        if(indexEqui != -1)
        {
            if (itensEquipados[indexEqui].item != null || listaItens[index].item != null)
            {
                if(listaItens[index].item != null)
                {
                    if (validaInventario(index))
                    {
                        indexIven = index;
                        pauseMenu.GetComponent<MenuPause>().setisActiveTab(1);
                    }
                    else
                        Debug.Log("Tipo de item não compativel com slot selecionado!");
                }
                else
                {
                    indexIven = index;
                    pauseMenu.GetComponent<MenuPause>().setisActiveTab(1);
                }
            }
        }
        else
        {
            indexIven = index;
            pauseMenu.GetComponent<MenuPause>().setisActiveTab(1);
        }
    }

    public int getItemEquipamento()
    {
        return indexEqui;
    }

    private bool validaEquipamento(int index)
    {
        bool retorno = false;
        switch (index)
        {
            case 0:
                if (listaItens[indexIven].item.GetComponent<ItensBase>().tipo.Equals(ItensBase.tipoItem.Magia))
                    retorno = true;
                else
                    retorno = false;
                break;
            case 1:
            case 3:
                if (listaItens[indexIven].item.GetComponent<ItensBase>().tipo.Equals(ItensBase.tipoItem.Anel))
                    retorno = true;
                else
                    retorno = false;
                break;
            case 2:
                if (listaItens[indexIven].item.GetComponent<ItensBase>().tipo.Equals(ItensBase.tipoItem.Arremessavel))
                    retorno = true;
                else
                    retorno = false;
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                if (listaItens[indexIven].item.GetComponent<ItensBase>().tipo.Equals(ItensBase.tipoItem.Consumivel))
                    retorno = true;
                else
                    retorno = false;
                break;
        }
        return retorno;
    }

    private bool validaInventario(int index)
    {
        bool retorno = false;
        switch (indexEqui)
        {
            case 0:
                if (listaItens[index].item.GetComponent<ItensBase>().tipo.Equals(ItensBase.tipoItem.Magia))
                    retorno = true;
                else
                    retorno = false;
                break;
            case 1:
            case 3:
                if (listaItens[index].item.GetComponent<ItensBase>().tipo.Equals(ItensBase.tipoItem.Anel))
                    retorno = true;
                else
                    retorno = false;
                break;
            case 2:
                if (listaItens[index].item.GetComponent<ItensBase>().tipo.Equals(ItensBase.tipoItem.Arremessavel))
                    retorno = true;
                else
                    retorno = false;
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                if (listaItens[index].item.GetComponent<ItensBase>().tipo.Equals(ItensBase.tipoItem.Consumivel))
                    retorno = true;
                else
                    retorno = false;
                break;
        }
        return retorno;
    }

    public void usarMagia(int index,Vector3 direcaoInimigo)
    {
        if (itensEquipados[index].item != null)
        {
            if (!Vector3.Equals(direcaoInimigo, Vector3.zero))
            {
                Vector3 playerPivo = player.transform.GetChild(2).transform.position;
                Vector3 direcao = direcaoInimigo - playerPivo;
                GameObject itemUsado = Instantiate(itensEquipados[index].item, Vector3.Normalize(direcao) * 2 + playerPivo, player.transform.rotation);
                itemUsado.GetComponent<ItensBase>().setDirecao(direcao);
            }
            else
            {
                Vector3 playerPivo = player.transform.GetChild(2).transform.position;
                int pivo;
                if (player.transform.localScale.x>0)
                    pivo = 1;
                else
                    pivo = -1;
                GameObject itemUsado = Instantiate(itensEquipados[index].item,new Vector3(playerPivo.x+(1.1f*pivo),playerPivo.y,playerPivo.z),player.transform.rotation);
                Vector3 auxS = new Vector3(itemUsado.transform.localScale.x*pivo, itemUsado.transform.localScale.y, itemUsado.transform.localScale.z);
                itemUsado.transform.localScale = auxS;
            }

            itemLista novoItem = new itemLista();
            if (itensEquipados[index].quantidade - 1 <= 0)
            {
                novoItem.item = null;
                novoItem.quantidade = 0;
            }
            else
            {
                novoItem.item = itensEquipados[index].item;
                novoItem.quantidade = itensEquipados[index].quantidade - 1;
            }
            itensEquipados[index] = novoItem;
        }
    }
    public void usarArremessavel(int index, Vector3 direcaoInimigo)
    {
        if (itensEquipados[index].item != null)
        {
            Vector3 playerPivo = player.transform.GetChild(2).transform.position;
            int pivo;
            if (player.transform.localScale.x > 0)
                pivo = 1;
            else
                pivo = -1;
            GameObject itemUsado = Instantiate(itensEquipados[index].item, new Vector3(playerPivo.x + (1.1f * pivo), playerPivo.y, playerPivo.z), player.transform.rotation);
            Vector3 auxS = new Vector3(itemUsado.transform.localScale.x * pivo, itemUsado.transform.localScale.y, itemUsado.transform.localScale.z);
            itemUsado.transform.localScale = auxS;

            itemLista novoItem = new itemLista();
            if (itensEquipados[index].quantidade - 1 <= 0)
            {
                novoItem.item = null;
                novoItem.quantidade = 0;
            }
            else
            {
                novoItem.item = itensEquipados[index].item;
                novoItem.quantidade = itensEquipados[index].quantidade - 1;
            }
            itensEquipados[index] = novoItem;
        }
    }

    public void usarItem(int index)
    {
        if (itensEquipados[index].item != null)
        {
            GameObject itemUsado = Instantiate(itensEquipados[index].item,player.transform.position,player.transform.rotation,player.transform);
            itemUsado.GetComponent<ItensBase>().setDirecao(Vector3.zero);

            itemLista novoItem = new itemLista();
            if (itensEquipados[index].quantidade - 1 <= 0)
            {
                novoItem.item = null;
                novoItem.quantidade = 0;
            }
            else
            {
                novoItem.item = itensEquipados[index].item;
                novoItem.quantidade = itensEquipados[index].quantidade - 1;
            }
            itensEquipados[index] = novoItem;
        }
    }
}
