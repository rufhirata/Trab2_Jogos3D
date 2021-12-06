using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorControle : MonoBehaviour
{
    private CharacterController controle;
    private Animator animacao;

    [Header("Config Player")]
    public float velocidade = 3f;

    private Vector3 direcao;
    private bool andando;

    //comandos do usuário
    private float horizontal;
    private float vertical;

    [Header("Ataque Config")]
    public ParticleSystem fxAtaque;
    public Transform areaAtaque; //hitBox
    [Range(0.2f, 1f)]
    public float alcanceAtaque = 0.5f; // hitRange
    public LayerMask objProcurados; //hitMask
    private bool atacando;
    public Collider[] objColididos; // hitInfo
    public int qtdeCorte;

    void Start()
    {
        controle = GetComponent<CharacterController>();
        animacao = GetComponent<Animator>();
    }

    void Update()
    {
        Comandos();
        Movendo();
        Atualizando();
    }

    #region Meus Métodos

    void Comandos() // verifica os comandos do usuário
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire1") && atacando == false) // botão esquerdo mouse config. Project Settings/Input Manager
        {
            Ataque();
        }
    }

    void Ataque()
    {
        atacando = true;
        animacao.SetTrigger("Ataque"); // move espada
        fxAtaque.Emit(1); // gera  faísca

        objColididos = Physics.OverlapSphere(areaAtaque.position, alcanceAtaque, objProcurados);

        foreach(Collider c in objColididos)
        {
            c.gameObject.SendMessage("GetCorte", qtdeCorte, SendMessageOptions.DontRequireReceiver);
        }

    }

    void Movendo() // move o Personagem
    {
        direcao = new Vector3 (horizontal, 0f, vertical).normalized; // move o player

       if(direcao.magnitude > 0.1f)
        {
            float angloPlayer = Mathf.Atan2(direcao.x, direcao.z) * Mathf.Rad2Deg; //calcula o anglo de direção do player
            transform.rotation = Quaternion.Euler(0, angloPlayer, 0); // rotaciona o player para a direção a seguir
            andando = true;
        }
        else
        {
            andando = false;
        }
 
        controle.Move(direcao * velocidade * Time.deltaTime);
    }

    void Atualizando() // atualiza o Animator
    {
        animacao.SetBool("andando", andando);
    }

    void AtaqueFim()
    {
        atacando = false;
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        if(areaAtaque != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(areaAtaque.position, alcanceAtaque);
        }
    }
}
