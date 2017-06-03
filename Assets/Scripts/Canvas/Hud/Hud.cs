using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

    public List<Transform> listaSlotHUD = new List<Transform>();
    private GameController gameController;
    private PlayerController player;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        //Set a vitalidade o player
        if (player.getVitalidade() >= 0)
            GameObject.Find("BarraVitalidade").GetComponent<RectTransform>().sizeDelta = new Vector2(156 * player.getVitalidade() / 100, 10);

        //Set a vitalidade o player
        if (player.energia >= 0)
            GameObject.Find("BarraMP").GetComponent<RectTransform>().sizeDelta = new Vector2(106 * player.energia / 100, 5);

        if (gameController.itensEquipados[0].item != null)
        {
            listaSlotHUD[0].gameObject.SetActive(true);
            listaSlotHUD[0].GetComponent<Image>().sprite = gameController.itensEquipados[0].item.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Transform slotHUD = GameObject.Find("SlotHud 0").transform.GetChild(1);
            slotHUD.gameObject.SetActive(false);
        }
        if (gameController.itensEquipados[2].item != null)
        {
            Transform slotHUD = GameObject.Find("SlotHud 1").transform.GetChild(1);
            slotHUD.gameObject.SetActive(true);
            slotHUD.GetComponent<Image>().sprite = gameController.itensEquipados[2].item.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Transform slotHUD = GameObject.Find("SlotHud 1").transform.GetChild(1);
            slotHUD.gameObject.SetActive(false);
        }
        if (gameController.itensEquipados[4].item != null)
        {
            Transform slotHUD = GameObject.Find("SlotHud 2").transform.GetChild(1);
            slotHUD.gameObject.SetActive(true);
            slotHUD.GetComponent<Image>().sprite = gameController.itensEquipados[4].item.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Transform slotHUD = GameObject.Find("SlotHud 2").transform.GetChild(1);
            slotHUD.gameObject.SetActive(false);
        }
        if (gameController.itensEquipados[5].item != null)
        {
            Transform slotHUD = GameObject.Find("SlotHud 3").transform.GetChild(1);
            slotHUD.gameObject.SetActive(true);
            slotHUD.GetComponent<Image>().sprite = gameController.itensEquipados[5].item.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Transform slotHUD = GameObject.Find("SlotHud 3").transform.GetChild(1);
            slotHUD.gameObject.SetActive(false);
        }
        if (gameController.itensEquipados[6].item != null)
        {
            Transform slotHUD = GameObject.Find("SlotHud 4").transform.GetChild(1);
            slotHUD.gameObject.SetActive(true);
            slotHUD.GetComponent<Image>().sprite = gameController.itensEquipados[6].item.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Transform slotHUD = GameObject.Find("SlotHud 4").transform.GetChild(1);
            slotHUD.gameObject.SetActive(false);
        }
        if (gameController.itensEquipados[7].item != null)
        {
            Transform slotHUD = GameObject.Find("SlotHud 5").transform.GetChild(1);
            slotHUD.gameObject.SetActive(true);
            slotHUD.GetComponent<Image>().sprite = gameController.itensEquipados[7].item.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Transform slotHUD = GameObject.Find("SlotHud 5").transform.GetChild(1);
            slotHUD.gameObject.SetActive(false);
        }
    }
}
