using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDinamica : MonoBehaviour
{
    public GameObject vCam2;

    private void OnTriggerEnter(Collider outros) //testa se o player entrou no cubo imaginário
    {
        switch(outros.gameObject.tag)
        {
            case "CamTrigger":
                vCam2.SetActive(true); // ativa a 2ª camera
                break;
        }
    }

    private void OnTriggerExit(Collider outros) //testa se o player saiu do cubo imaginário
    {
        switch(outros.gameObject.tag)
        {
            case "CamTrigger":
                vCam2.SetActive(false); // desativa a 2ª camera e volta a 1ª camera
                break;
        }
    }

}
