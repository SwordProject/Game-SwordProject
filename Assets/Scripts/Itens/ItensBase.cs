using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItensBase : MonoBehaviour {

    public tipoItem tipo;
    public Vector3 direcao;

    public enum tipoItem
    {
        Anel,
        Arremessavel,
        Consumivel,
        Magia
    }

    public void setDirecao(Vector3 direcao)
    {
        this.direcao = Vector3.Normalize(direcao);
    }
}
