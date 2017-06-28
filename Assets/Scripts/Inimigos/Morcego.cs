using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morcego : Inimigos {
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

    }

    public override void morrer()
    {
        Destroy(gameObject, 0.2f);
    }
}
