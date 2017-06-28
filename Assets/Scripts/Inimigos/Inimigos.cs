using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inimigos : MonoBehaviour
{
    //Atributos do Sangue
    public float vitalidade = 100;
    public ParticleSystem particulaSangue;
    public Vector3 casa;

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
            morrer();
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
            float direcao = 0;
            if (player.position.x < transform.position.x)
            {
                if (transform.localScale.x > 0)
                    direcao = 1;
                else
                    direcao = -1;
            }
            else
            {
                if(transform.localScale.x < 0)
                    direcao = 1;
                else
                    direcao = -1;
            }
            Vector3 direcaoInimigo = transform.localScale;
            transform.localScale = new Vector3(direcao*direcaoInimigo.x, direcaoInimigo.y, direcaoInimigo.z);
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
                moverToHouse();
            }
        }

        exibVitalidade();
    }

    public abstract void moverToPlay();
    public abstract void moverToHouse();
    public abstract void exibVitalidade();
    public abstract void morrer();

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
            usarMagia(player.position);
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

    public void usarMagia(Vector3 direcaoInimigo)
    {
        Vector3 playerPivo = transform.position;
        Vector3 direcao = direcaoInimigo - playerPivo;
        GameObject itemUsado = Instantiate(arremessavel, Vector3.Normalize(direcao) * 2 + playerPivo, player.transform.rotation);
        itemUsado.GetComponent<ItensBase>().setDirecao(direcao);
    }
}