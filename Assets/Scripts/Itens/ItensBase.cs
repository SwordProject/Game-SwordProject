using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItensBase : MonoBehaviour {

    public tipoItem tipo;

    public enum tipoItem
    {
        Anel,
        Arremessavel,
        Consumivel,
        Magia
    }
}
