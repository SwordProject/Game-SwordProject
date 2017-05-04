using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PainelBase : MonoBehaviour {

    abstract public void setSizePainel(Vector2 tamanho);
    abstract public void cancelar();
}
