using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSeguir : MonoBehaviour {

    public Transform player;
    public float suavizacao = 0.3f;
    private float positionScreenX = 3f;
    private float positionScreenY = 2f;
    private Vector2 velocidade = new Vector2();

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float auxX = Mathf.SmoothDamp(transform.position.x, player.position.x + positionScreenX, ref velocidade.x, suavizacao);
        float auxY = Mathf.SmoothDamp(transform.position.y, player.position.y + positionScreenY, ref velocidade.y, suavizacao);
        transform.position = new Vector3(auxX, auxY, -10f);
    }

    public void setScreenBack()
    {
        positionScreenX = -3f;
    }
    public void setScreenFront()
    {
        positionScreenX = 3f;
    }
}
