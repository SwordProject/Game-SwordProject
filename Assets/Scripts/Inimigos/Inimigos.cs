﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inimigos : MonoBehaviour
{
    //Atributos do Sangue
    public float vitalidade = 100;
    public ParticleSystem particulaSangue;
    private Vector3 casa;

    //atributo Inimigo
    public int distanciaReacao;
    public float velocidade;
    public GameObject arremessavel;

    //atributos atack
    public float danoPerto;
    public float tempoEntreAtackPerto = 1;
    public float tempoEntreAtackLonge = 1;

    public Transform player;
    private float timeAtack;
    private float timeDecisao;
    private int decisao = 0;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").transform.GetChild(2);
        timeDecisao = 3;
        timeAtack = 0;
        casa = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (vitalidade <= 0)
        {
            particulaSangue.Play();
            Destroy(gameObject,0.2f);
        }
        if (timeDecisao >= 3)
        {
            decisao = Random.Range(0, 2);
            timeDecisao = 0;
        }

        timeDecisao += Time.deltaTime;
        timeAtack += Time.deltaTime;

        float distplay = Vector3.Distance(player.position, transform.position);
        if (distplay < distanciaReacao && distplay > 1)
        {
            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
            switch (decisao)
            {
                case 0:
                    moverToPlay();
                    break;
                case 1:
                    atacarDistante();
                    break;
            }
        }
        else
        {
            if (distplay < 1)
            {
                ataquePerto();
            }
            if (distplay > distanciaReacao * 1.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, casa, Time.deltaTime * velocidade);
            }
        }
    }

    public abstract void moverToPlay();

    private void ataquePerto()
    {
        if (timeAtack >= tempoEntreAtackPerto)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().addDano(danoPerto);
            timeAtack = 0;
        }
    }

    private void atacarDistante()
    {
        if (timeAtack >= tempoEntreAtackLonge)
        {
            float pivo = 0;
            if (transform.localScale.x <= 0)
                pivo = 1.5f;
            else
                pivo = -1.5f;
            GameObject itemUsado = Instantiate(arremessavel, new Vector3(transform.position.x + pivo, transform.position.y), transform.rotation);
            itemUsado.GetComponent<ItensBase>().setDirecao(player.position - transform.position);
            timeAtack = 0;
        }
    }

    public void addDano(float dano)
    {
        particulaSangue.Play();
        if (vitalidade - dano <= 0)
        {
            vitalidade = 0;
        }
        else
        {
            vitalidade -= dano;
        }
    }
}