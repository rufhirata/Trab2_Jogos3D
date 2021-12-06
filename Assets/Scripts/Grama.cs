using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grama : MonoBehaviour
{
    public ParticleSystem fxParticula; // fxHit
    private bool cortado;

    void GetCorte(int qtde) //GetHit
    {
        if (cortado == false)
        {
            cortado = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
            fxParticula.Emit(20);
        }
    }

}
