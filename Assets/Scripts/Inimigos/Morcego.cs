using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morcego : Inimigos {
    public override void moverToPlay()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * velocidade);
    }
}
