using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum estadosInimigos
{ 
    PARADO, ALERTA, EXPLORAR, PATRULHA, PERSEGUIR, FURIA
    // IDLE, ALERT, EXPLORE, PATROL, FOLLOW, FURY
}
public class GameManager : MonoBehaviour
{
    public Transform jogador;
    [Header("Slime IA")]
    public float slimeTempoEsperaParado;
    public Transform[] slimePontosPassagem;
    public float slimeDistanciaAtaque = 2.3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
