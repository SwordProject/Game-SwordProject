using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MorcegoBoss : Inimigos {

    private float timeMorteDecorrido = 0;

    public override void moverToPlay()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * velocidade);
    }

    public override void moverToHouse()
    {
        transform.position = Vector3.MoveTowards(transform.position, casa, Time.deltaTime * velocidade);
    }

    public override void exibVitalidade()
    {
        if (vitalidade >= 0)
            GameObject.Find("BossVitalidade").GetComponent<RectTransform>().sizeDelta = new Vector2(500 * vitalidade / 1000, 10);
    }

    public override void morrer()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.down, Time.deltaTime * velocidade);
        timeMorteDecorrido += Time.deltaTime;
        Time.timeScale = 0.2f;
        if (timeMorteDecorrido > 0.7)
        {
            DB baseDado = GameObject.Find("DB").GetComponent<DB>();
            baseDado.updateDados();
            SceneManager.LoadScene("Win");
        }
    }
}
