﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variaveis de movimentação
    private Rigidbody2D playerRigidbody2D;
    private float jumpForce;
    private float speed;

    //variavel que pega a posição de inicio e fim do raicast.
    //O raicast será usado para definir quando o personagem está no chão ou não.
    public Transform startLineCast;
    public Transform endLineCast;
    private bool estaNoCaho;

    //Animação atack
    public float tempoEntreAtack = 0.1f;
    private float timeDecorridoAtack=0;
    private float durationAtack=0;
    private float maxDurationAtack = 0.8f;
    private bool atack;
    private bool darDano = false;

    //Variaveis de controle de animação
    private Animator playerAnimator;

    //Atributos do Sangue
    public float vitalidade;
    public ParticleSystem particulaSangue;
    public ParticleSystem particulaVida;

    //Atributos de Energia
    public float energia;
    public ParticleSystem particulaEnergia;

    //Controle do game
    private GameController gameController;

    //Inimigos
    public List<Transform> listaInimigos = new List<Transform>();
    public LayerMask layerInimigo;

    //Queda por muito tempo
    private float timeQueda = 0;

    private DB baseDado;

    void Start()
    {
        baseDado = GameObject.Find("DB").GetComponent<DB>();
        this.vitalidade = baseDado.vitalidade;
        this.energia = baseDado.energia;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        jumpForce = 500;
        //Inicia a barra de vitalidade do personagem
        atack = false;
    }

    void Update()
    {
        if (durationAtack == 0 && timeDecorridoAtack >= tempoEntreAtack)
        {
            if (Input.GetButton("Fire1"))
            {
                atack = true;
                darDano = true;
            }
        }

        //Chama metodo de animação. 
        setAnimator();

        //Rotaciona o player para direita ou esquerda. 
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
            GameObject.Find("Main Camera").GetComponent<CameraSeguir>().setScreenFront();
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
            GameObject.Find("Main Camera").GetComponent<CameraSeguir>().setScreenBack();
        }

        //Verifica se o personagem esta no chao.
        raycast();

        if (estaNoCaho)
        {
            if (Input.GetButtonDown("Jump"))
            {
                playerRigidbody2D.AddForce(new Vector2(0, jumpForce));
                SoundScript.Instance.MakeJumpSound();
                estaNoCaho = false;
            }
            speed = 8;
            timeQueda = 0;
        }
        else
        {
            timeQueda += Time.deltaTime;
            if (timeQueda > 3)
                addDano(100);
            speed = 6;
        }
        if (atack)
            speed = 3;

        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0) * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (energia > 0)
            {
                removeEnargia(20);
                gameController.usarMagia(0, inimigoMaisProximo());
            }
        }
        localizaInimigos();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            gameController.usarArremessavel(2, inimigoMaisProximo());
        if (Input.GetKeyDown(KeyCode.Alpha3))
            gameController.usarItem(4);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            gameController.usarItem(5);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            gameController.usarItem(6);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            gameController.usarItem(7);
        timeDecorridoAtack += Time.deltaTime;
    }

    private void raycast()
    {
        RaycastHit2D hit = Physics2D.Linecast(startLineCast.position, endLineCast.position);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Ground")
                estaNoCaho = true;
            else
                estaNoCaho = false;
        }
        else
            estaNoCaho = false;
    }

    private void setAnimator()
    {
        if (!atack)
        {
            playerAnimator.SetBool("atack", false);
            if (estaNoCaho)
            {
                playerAnimator.SetInteger("pulando", 0);
                playerAnimator.SetFloat("correndo", Mathf.Abs(Input.GetAxis("Horizontal")));
            }
            else
            {
                playerAnimator.SetInteger("pulando", 1);
                playerAnimator.SetFloat("correndo", 0);
            }
        }
        else
        {
            durationAtack += Time.deltaTime;
            if (durationAtack >= maxDurationAtack)
            {
                atack = false;
                darDano = false;
                playerAnimator.SetBool("atack", false);
                durationAtack = 0;
                timeDecorridoAtack = 0;
            }
            else
            {
                playerAnimator.SetBool("atack", true);
                playerAnimator.SetInteger("pulando", 0);
                playerAnimator.SetFloat("correndo", 0);
                if (darDano && listaInimigos.Count>0)
                    atackFisicoInimigos();
            }
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

    public void removeEnargia(float valor)
    {
        if (energia - valor <= 0)
            energia = 0;
        else
            energia -= valor;
    }

    public void addVitalidade(float vida)
    {
        particulaVida.Play();
        if (vitalidade + vida >= 100)
        {
            vitalidade = 100;
        }
        else
        {
            vitalidade += vida;
        }
    }

    public void addEnergia(float mp)
    {
        particulaEnergia.Play();
        if (energia + mp >= 100)
        {
            energia = 100;
        }
        else
        {
            energia += mp;
        }
    }

    public float getVitalidade()
    {
        return vitalidade;
    }


    public float getEnergia()
    {
        return energia;
    }

    public void setVitalidade(float vitalidade)
    {
        this.vitalidade = vitalidade;
    }


    public void setEnergia(float energia)
    {
        this.energia = energia;
    }

    private void localizaInimigos()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(startLineCast.position, 15f, layerInimigo.value);
        if (colliders.Length > 0)
        {
            listaInimigos.Clear();
            for (int i = 0; i < colliders.Length; i++)
            {
                listaInimigos.Add(colliders[i].transform);
            }
        }
        else
        {
            listaInimigos.Clear();
        }
    }

    private void atackFisicoInimigos()
    {
        if (listaInimigos.Count > 0)
        {
            foreach (Transform inimigo in listaInimigos)
            {
                if (Vector2.Distance(transform.GetChild(2).position, inimigo.position) < 2)
                {
                    inimigo.GetComponent<Inimigos>().addDano(25);
                    darDano = false;
                }
            }
        }
    }

    private Vector3 inimigoMaisProximo()
    {
        Vector3 inimigoProximo = new Vector3();
        foreach (Transform inimigo in listaInimigos)
        {
            if (Vector2.Distance(transform.GetChild(2).position, inimigo.position) < Vector2.Distance(transform.GetChild(2).position, inimigoProximo))
            {
                inimigoProximo = inimigo.position;
            }
        }
        return inimigoProximo;
    }
}