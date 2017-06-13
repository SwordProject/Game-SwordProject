using System.Collections;
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
    private float timeAtack = 0.08f;
    private float timeDecorridoAtack;
    private bool atack;

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
    private Vector3 inimigo;
    private GameObject myInimigo;
    public LayerMask layerInimigo;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        jumpForce = 500;
        //Inicia a barra de vitalidade do personagem
        vitalidade = 100;
        energia = 100;
        atack = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            atack = true;
            if (Vector3.Distance(transform.GetChild(2).position, inimigo) < 2)
            {
                myInimigo.GetComponent<Inimigos>().addDano(25);
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
        }
        else
            speed = 6;

        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0) * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (energia > 0)
            {
                removeEnargia(20);
                gameController.usarMagia(0, inimigo);
            }
        }
        localizaInimigos();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            gameController.usarArremessavel(2, inimigo);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            gameController.usarItem(4);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            gameController.usarItem(5);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            gameController.usarItem(6);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            gameController.usarItem(7);
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
            if (timeDecorridoAtack >= timeAtack)
            {
                playerAnimator.SetBool("atack", false);
                atack = false;
                timeDecorridoAtack = 0;
            }
            else
            {
                playerAnimator.SetBool("atack", true);
                playerAnimator.SetInteger("pulando", 0);
                playerAnimator.SetFloat("correndo", 0);
                timeDecorridoAtack += Time.deltaTime;
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

    public void setInimigo(Vector3 inimigo)
    {
        this.inimigo = inimigo;
    }

    private void localizaInimigos()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(startLineCast.position, 15f, layerInimigo.value);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (Vector3.Distance(transform.position, inimigo) > Vector3.Distance(transform.position, colliders[i].transform.position))
                {
                    inimigo = colliders[i].transform.position;
                    myInimigo = colliders[i].gameObject;
                }
            }
        }
        else
        {
            inimigo = Vector3.zero;
            myInimigo = null;
        }
    }
}