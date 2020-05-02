using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiendaController : MonoBehaviour
{
    public Button btnComprarPrioridad, btnComprarConcentracion, btnComprarGenio;
    public Text txtNivelPrioridad, txtNivelConcentracion, txtNivelGenio;

    public Text txtInsuficientePrioridad, txtInsuficienteConcentracion, txtInsuficienteGenio;

    public Text txtUsarObjeto1, txtUsarObjeto2, txtUsarObjeto3;

    bool desbloqueadoPriodad, desbloqueadoConcentracion, desbloqueadoGenio, prioridadActivo, concentracionActivo, genioActivo;

    // Start is called before the first frame update
    void Start()
    {
        desbloqueadoPriodad = false;
        desbloqueadoConcentracion = false;
        desbloqueadoGenio = false;
        prioridadActivo = false;
        concentracionActivo = false;
        genioActivo = false;
    }

    // Update is called once per frame
    void Update()
    {
        ComprobarNivel();
        if(prioridadActivo == false && concentracionActivo == false && genioActivo == false)
        {ComprobarGreenCoins();}
        else{
            ComprobarItemActivo();
        }
        
    }

    public void ComprarPrioridadAction()
    {
        prioridadActivo = true;
        PlaySceneController.playSceneController.GestionarGreenCoins(-15);

    }

    public void ComprarConcentracionAction()
    {
        concentracionActivo = true;
        PlaySceneController.playSceneController.GestionarGreenCoins(-30);
    }

    public void ComprarGenioAction()
    {
       genioActivo = true;
        PlaySceneController.playSceneController.GestionarGreenCoins(-50);
    }

    private void ComprobarItemActivo()
    {
        if(desbloqueadoPriodad)
        {
            btnComprarPrioridad.gameObject.SetActive(false);
            txtInsuficientePrioridad.gameObject.SetActive(false);
            txtUsarObjeto1.gameObject.SetActive(true);
        }

        if(desbloqueadoConcentracion)
        {
            btnComprarConcentracion.gameObject.SetActive(false);
            txtInsuficienteConcentracion.gameObject.SetActive(false);
            txtUsarObjeto2.gameObject.SetActive(true);
        }

        if(desbloqueadoGenio)
        {
            btnComprarGenio.gameObject.SetActive(false);
            txtInsuficienteGenio.gameObject.SetActive(false);
            txtUsarObjeto3.gameObject.SetActive(true);
        }
    }

    private void ComprobarNivel()
    {

        int nivel = PlaySceneController.playSceneController.getNivel();

        if(desbloqueadoPriodad == false)
        {
            if(nivel >= 2)
            {
                desbloqueadoPriodad = true;
                txtNivelPrioridad.gameObject.SetActive(false);
            }
        }

        if(desbloqueadoConcentracion == false)
        {
            if(nivel >= 5)
            {
                desbloqueadoConcentracion = true;
                txtNivelConcentracion.gameObject.SetActive(false);
            }
        }

        if(desbloqueadoGenio == false)
        {
            if(nivel >= 8)
            {
                desbloqueadoGenio = true;
                txtNivelGenio.gameObject.SetActive(false);
            }
        }
    }

    private void ComprobarGreenCoins()
    {
        int gCoins = PlaySceneController.playSceneController.getGreenCoins();

        if(desbloqueadoPriodad == true)
        {
            if(gCoins >= 15)
            {
                btnComprarPrioridad.gameObject.SetActive(true);
                txtInsuficientePrioridad.gameObject.SetActive(false);
            }
            else
            {
                btnComprarPrioridad.gameObject.SetActive(false);
                txtInsuficientePrioridad.gameObject.SetActive(true);
            }
        }

        if(desbloqueadoConcentracion == true)
        {
            if(gCoins >= 30)
            {
                btnComprarConcentracion.gameObject.SetActive(true);
                txtInsuficienteConcentracion.gameObject.SetActive(false);
            }
            else
            {
                btnComprarConcentracion.gameObject.SetActive(false);
                txtInsuficienteConcentracion.gameObject.SetActive(true);
            }
        }

        if(desbloqueadoGenio == true)
        {
            if(gCoins >= 50)
            {
                btnComprarGenio.gameObject.SetActive(true);
                txtInsuficienteGenio.gameObject.SetActive(false);
            }
            else
            {
                btnComprarGenio.gameObject.SetActive(false);
                txtInsuficienteGenio.gameObject.SetActive(true);
            }
        }
    }
}
