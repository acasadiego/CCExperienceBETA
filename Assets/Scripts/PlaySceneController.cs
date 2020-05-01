using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySceneController : MonoBehaviour
{
    private Scene scene;

    public static PlaySceneController playSceneController;
    public GameObject mapa, estadisticas, tienda, info, panelFinal;
   
    public TextAsset ChinaTxt, EstadosuTxt, AlemaniaTxt, ArabiasTxt, BrasilTxt;
    private bool isShowingMapa, isShowingEstadisticas, isShowingTienda, isShowingInfo;

    public Text txtUserName, txtAñoActual, txtGreenCoins, txtNivel, txtExperiencia, txtEstado, txtMensajeFinal;
    
    private int añoActual, greenCoins, nivel, experiencia;

    bool estadisticasIniciadas;

    double timer;

     string estado;
    
    void Start()
    {
        estado = "EMISIONES DE CO2 ALTAS";
        txtEstado.text = "Estado: " + estado;
        txtEstado.color = Color.magenta;
        nivel=1;
        experiencia=0;
        timer = 0;
        greenCoins = 5;
        txtGreenCoins.text = greenCoins.ToString();
        playSceneController = this;
        añoActual = 2020;
        txtUserName.text = PlayerData.playerData.getUsername();
        scene = SceneManager.GetActiveScene();
        EstadisticasAction(); //Se carga la pestaña estadisticas para poder activar el script EstadisticasController y tener sus metodos disponibles.

    }   

    void Update()//En el update nos aseguramos mediante un tiempo prudente (timer) que el script EstadisticasController cargue completamente al inicio.
    {
        if(estadisticasIniciadas == false)
        {
            timer += Time.deltaTime;
        }

        if(estadisticasIniciadas==false && timer>0.005)
        {
            estadisticasIniciadas = true;
            MapaAction();
        }

    }


    // Update is called once per frame

    public void VolverAction(){
        SceneManager.LoadScene(scene.buildIndex-2);
        ResetearDatosEstadisticas(); /*Se cancela toda simulacion existente al volver al menu principal*/
    }

    public void SalirAction(){
        Application.Quit();
    }

    public void MapaAction(){
        ResetearDatosEstadisticas(); /*Se cancela toda simulacion existente al cambiar de pestaña*/
        isShowingEstadisticas= false;
        estadisticas.SetActive(isShowingEstadisticas);
        isShowingTienda = false;
        tienda.SetActive(isShowingTienda);
        isShowingMapa = true;
        mapa.SetActive(isShowingMapa);
    }

    public void EstadisticasAction(){

        isShowingMapa = false;
        mapa.SetActive(isShowingMapa);
        isShowingTienda = false;
        tienda.SetActive(isShowingTienda);
        isShowingEstadisticas = true;
        estadisticas.SetActive(isShowingEstadisticas);

    }

    public void TiendaAction(){
        ResetearDatosEstadisticas(); /*Se cancela toda simulacion existente al cambiar de pestaña*/
        isShowingMapa = false;
        mapa.SetActive(isShowingMapa);
        isShowingEstadisticas= false;
        estadisticas.SetActive(isShowingEstadisticas);
        isShowingTienda = true;
        tienda.SetActive(isShowingTienda);
    }

    //Mostrar info de los diferentes paises presentes en el mapa
    public void ChinaAction(){
        isShowingInfo = true;
        info.GetComponent<UnityEngine.UI.Text>().text = ChinaTxt.text;
        info.SetActive(isShowingInfo);
    }

    public void EstadosuAction(){
        isShowingInfo = true;
        info.GetComponent<UnityEngine.UI.Text>().text = EstadosuTxt.text;
        info.SetActive(isShowingInfo);
    }

    public void AlemaniaAction(){
        isShowingInfo = true;
        info.GetComponent<UnityEngine.UI.Text>().text = AlemaniaTxt.text;
        info.SetActive(isShowingInfo);
    }

    public void ArabiasAction(){
        isShowingInfo = true;
        info.GetComponent<UnityEngine.UI.Text>().text = ArabiasTxt.text;
        info.SetActive(isShowingInfo);
    }

    public void BrasilAction(){
        isShowingInfo = true;
        info.GetComponent<UnityEngine.UI.Text>().text = BrasilTxt.text;
        info.SetActive(isShowingInfo);
    }

    public void BackAction(){
        ResetearDatosEstadisticas();
        isShowingInfo = false;
        info.SetActive(isShowingInfo);
    }

    public void AvanzarAñoAction() //Avanza un año, independientemente de si el objeto estadisticas este activado o no.
    {
        determinarEstado();
        añadirExperiencia(1);
        MissionController.misionController.HideMisionDetails();
        añoActual++;
        txtAñoActual.text = "Año: " + añoActual;
        EstadisticasController.estadisticasController.AvanzarAñoActual();
        MissionController.misionController.GenerarMisiones();
        GestionarGreenCoins(1);

    }

    private void ResetearDatosEstadisticas()
    {
        /*Si estadisticas se encuentra desactivado, no tiene sentido resetear datos,
        puesto que no se ha ejecutado ninguna simulacion*/
        if(isShowingEstadisticas) 
        {
            EstadisticasController.estadisticasController.ResetearDatosAction();
        }
    }

    public void GestionarGreenCoins(int cantidad)
    {
        if(greenCoins+cantidad >= 0)
        {
            greenCoins += cantidad;
        }
        else
        {
            greenCoins = 0;
        }

        txtGreenCoins.text = greenCoins.ToString();
    }

    public int getGreenCoins()
    {
        return greenCoins;
    }

    public void añadirExperiencia(int expGanada)
    {
        experiencia+=expGanada;
        if(experiencia>=nivel*10)
        {
            nivel++;
            experiencia = 0;
            txtNivel.text = "Nivel " + nivel;
        }
        txtExperiencia.text = experiencia + "/" +  nivel*10;
    }

    private void determinarEstado()
    {
        double emisionesCo2 = EstadisticasController.estadisticasController.getEmisionCo2Ant();
        

        if(emisionesCo2 >= 32000000000 && emisionesCo2 < 55000000000)
        {
            estado = "EMISIONES DE CO2 ALTAS";
            txtEstado.color = Color.magenta;
            
        }
        else if(emisionesCo2 >= 55000000000 && emisionesCo2 < 70000000000)
        {
            estado = "EMISIONES DE CO2 DEMASIADO ALTAS";
            txtEstado.color = Color.red;
        }
        else if(emisionesCo2 >= 70000000000)
        {
            estado = "HA PERDIDO EL JUEGO";
            txtEstado.color = Color.red;
            panelFinal.SetActive(true);
            panelFinal.GetComponent<Image>().color = Color.red;
            txtMensajeFinal.text = "HAS PERDIDO EL JUEGO. LAS EMISIONES DE CO2 GLOBALES SON DE 70 BILLONES DE TON";

        }
        else if(emisionesCo2 < 32000000000 && emisionesCo2>= 31500000000)
        {
            estado = "EMISIONES DE CO2 MODERADAS";
            txtEstado.color = Color.blue;
        }
        else if(emisionesCo2 < 31500000000 && emisionesCo2>= 31000000000)
        {
            estado = "VAS SALVANDO EL PLANETA";
            txtEstado.color = Color.green;
        }
        else
        {
            estado ="HAS GANADO EL JUEGO";
            txtEstado.color = Color.green;
            panelFinal.SetActive(true);
            panelFinal.GetComponent<Image>().color = Color.magenta;
            txtMensajeFinal.text = "HAS GANADO EL JUEGO. LAS EMISIONES DE CO2 GLOBALES SON DE 31 BILLONES DE TON ¡Y BAJANDO!";
        }

        txtEstado.text = "Estado: " + estado;
    }


   


}
