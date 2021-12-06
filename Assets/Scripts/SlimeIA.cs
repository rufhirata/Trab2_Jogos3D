using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeIA : MonoBehaviour
{
    private GameManager _GameManager;
    private Animator animacao;
    public int Vidas;
    private bool depoisMorto;

    public estadosInimigos estado;

   // public const float tempoEsperaParado = 3f;
   // public const float tempoEsperaPatrulha = 5f;

    //IA
    private bool andando;
    private bool alerta;
    private NavMeshAgent agente;
    private int idPontoPassagem;
    private Vector3 destino;

    void Start()
    {
        _GameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        animacao = GetComponent<Animator>(); 
        agente = GetComponent<NavMeshAgent>();
        TrocarEstado(estado);       
    }

    void Update()
    {
        GerenciamentoEstado();

        if (agente.desiredVelocity.magnitude >= 0.1f)
        {
            andando = true;
        }       
        else{
            andando = false;
        }
        
        animacao.SetBool("andando", andando);
    }

    IEnumerator Morto()
    {
        depoisMorto = true;
        yield return new WaitForSeconds(2.5F);
        Destroy(this.gameObject);
    }

    #region Meus Métodos

    void GetCorte(int qtde) //GetHit
    {
        if(depoisMorto == true) { return; }

        Vidas -= qtde;
        
        if (Vidas >0)
        {
            TrocarEstado(estadosInimigos.FURIA);
            animacao.SetTrigger("GetHit");
        }
        else
        {
            animacao.SetTrigger("Morrer");
            StartCoroutine("Morto");
        }
    }

    void GerenciamentoEstado()
    {
        switch(estado)
        {
            case estadosInimigos.PERSEGUIR:

                break;

            case estadosInimigos.FURIA:
                destino = _GameManager.jogador.position;
                agente.destination = destino;
                break;
            
            case estadosInimigos.PATRULHA:

                break;
        }
    }

    void TrocarEstado(estadosInimigos novoEstado)
    {
        StopAllCoroutines(); // Encerra todas as Corroutinas
        print(novoEstado);
        estado = novoEstado;
        switch(estado)
        {
            case estadosInimigos.PARADO:
                agente.stoppingDistance = 0;
                destino = transform.position;
                agente.destination = destino;
                StartCoroutine("PARADO");
                break;

            case estadosInimigos.ALERTA:

                break;

            case estadosInimigos.PATRULHA:
                agente.stoppingDistance = 0;
                idPontoPassagem = Random.Range(0, _GameManager.slimePontosPassagem.Length);
                destino = _GameManager.slimePontosPassagem[idPontoPassagem].position;
                agente.destination = destino;
                StartCoroutine("PATRULHA");
                break;

            case estadosInimigos.FURIA:
                destino = transform.position;
                agente.stoppingDistance = _GameManager.slimeDistanciaAtaque;
                agente.destination = destino;

                break;
        }

    }

    IEnumerator PARADO()
    {
        yield return new WaitForSeconds(_GameManager.slimeTempoEsperaParado);
        FicarParado(50); // 50% chance de ficar parado
    }

    IEnumerator PATRULHA()
    {
//        yield return new WaitForSeconds(tempoEsperaPatrulha);
        yield return new WaitUntil(() => agente.remainingDistance <= 0);
        FicarParado(30); // 30% chance de ficar parado
    }

    void FicarParado(int percentual)
    {
        if(NumRandomico() <= percentual)
        {
            TrocarEstado(estadosInimigos.PARADO);
        }
        else
        {
            TrocarEstado(estadosInimigos.PATRULHA);            
        }
    }

    int NumRandomico()
    {
        int numRandomico = Random.Range(0,100);
        return numRandomico;
    }

    #endregion
}
