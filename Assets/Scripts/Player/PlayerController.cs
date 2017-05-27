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

    //Variaveis de controle de animação
    private Animator playerAnimator;
    private float controlequeda;

    //Atributos do Sangue
    private float vitalidade = 100;

    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        jumpForce = 500;
    }

    void Update()
    {
        float sangue = 156 * vitalidade / 100;
        GameObject.Find("BarraVitalidade").GetComponent<RectTransform>().sizeDelta = new Vector2(sangue, 10);
        //Variaveis auxiliares de animação. 
        setAnimator();

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
        {
            speed = 6;
        }

        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0) * Time.deltaTime * speed);
    }

    private void raycast()
    {
        if (Physics2D.Linecast(startLineCast.position,endLineCast.position))
            estaNoCaho = true;
        else
            estaNoCaho = false;
    }

    private void setAnimator()
    {
        if (estaNoCaho) {
            playerAnimator.SetInteger("pulando", 0);
            playerAnimator.SetFloat("correndo", Mathf.Abs(Input.GetAxis("Horizontal")));
        }
        else
        {
            playerAnimator.SetInteger("pulando",1);
            playerAnimator.SetFloat("correndo", 0);
        }
    }
}

