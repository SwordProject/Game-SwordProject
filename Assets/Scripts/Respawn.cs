using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    private DB baseDado;

    private void Start()
    {
        baseDado = GameObject.Find("DB").GetComponent<DB>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            baseDado.updateDados();
            SceneManager.LoadScene("Level 2");
        }
    }
}
