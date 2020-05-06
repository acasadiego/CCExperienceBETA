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
    public GameObject mapa, estadisticas, tienda, panelInfoPais, panelFinal;
   
    private bool isShowingMapa, isShowingEstadisticas, isShowingTienda, isShowingInfo;

    public Text txtUserName, txtAñoActual, txtGreenCoins, txtNivel, txtExperiencia, txtEstado, txtMensajeFinal,txtNombrePais, txtDescripcionPais, txtVehiculosPais, txtIndustriaPais, txtQuemaPais;
    
    private int añoActual, greenCoins, nivel, experiencia;

    public Image imgBandera, imgObjeto;

    public Sprite sp_BanderaChina, sp_BanderaUSA, sp_BanderaAlemania, sp_BanderaArabia, sp_BanderaBrasil, sp_Prioridad, sp_Concnetracion, sp_Genio; 

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
        greenCoins = 100;
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

        ConocerObjetoActivo();

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
        imgBandera.sprite = sp_BanderaChina;
        txtNombrePais.text = "CHINA";
        txtDescripcionPais.text = "La República Popular China es un país ubicado en Asia Oriental. Es caracteristico por ser la primera potencia económica mundial por PIB. Su idioma oficial es el mandarín y es un estado unipartidista gobernado por el Partido Comunista. Su sede de gobierno y capital es Pekín.";
        txtVehiculosPais.text = ((int)EstadisticasController.estadisticasController.getVehiculoCombustibleChinaACT()).ToString();
        txtIndustriaPais.text = ((int)EstadisticasController.estadisticasController.getIndustriaChinaACT()).ToString();
        txtQuemaPais.text = ((int)EstadisticasController.estadisticasController.getPlasticoQuemadoChinaACT()).ToString();
        panelInfoPais.SetActive(isShowingInfo);
    }

    public void EstadosuAction(){
        isShowingInfo = true;
        imgBandera.sprite = sp_BanderaUSA;
        txtNombrePais.text = "ESTADOS UNIDOS";
        txtDescripcionPais.text = "Es un país soberano constituido en república federal constitucional compuesta por cincuenta estados y un distrito federal. La mayor parte del país se ubica en el medio de América del Norte. No tiene lengua oficial pero su idioma más hablado es el inglés.";
        txtVehiculosPais.text = ((int)EstadisticasController.estadisticasController.getVehiculoCombustibleUSAACT()).ToString();
        txtIndustriaPais.text = ((int)EstadisticasController.estadisticasController.getIndustriaUSAACT()).ToString();
        txtQuemaPais.text = ((int)EstadisticasController.estadisticasController.getPlasticoQuemadoUSAACT()).ToString();
        panelInfoPais.SetActive(isShowingInfo);
    }

    public void AlemaniaAction(){
        isShowingInfo = true;
        imgBandera.sprite = sp_BanderaAlemania;
        txtNombrePais.text = "ALEMANIA";
        txtDescripcionPais.text = "Es un país soberano centroeuropeo, miembro de la Unión Europea, constituido en Estado social y democrático de derecho y cuya forma de gobierno es la república parlamentaria y federal. Su capital es Berlín. Su lengua oficial es el alemán";
        txtVehiculosPais.text = ((int)EstadisticasController.estadisticasController.getVehiculoCombustibleAlemaniaACT()).ToString();
        txtIndustriaPais.text = ((int)EstadisticasController.estadisticasController.getIndustriaAlemaniaACT()).ToString();
        txtQuemaPais.text = ((int)EstadisticasController.estadisticasController.getPlasticoQuemadoAlemaniaACT()).ToString();
        panelInfoPais.SetActive(isShowingInfo);
    }

    public void ArabiasAction(){
        isShowingInfo = true;
        imgBandera.sprite = sp_BanderaArabia;
        txtNombrePais.text = "ARABIA SAUDITA";
        txtDescripcionPais.text = "Es un país de Asia Occidental ubicado en la península arábiga, cuya forma de gobierno es la monarquía absoluta. Su liderazgo en la exportación mundial de petróleo la ha convertido en una de las veinte economías más grandes del planeta.";
        txtVehiculosPais.text = ((int)EstadisticasController.estadisticasController.getVehiculoCombustibleArabiaACT()).ToString();
        txtIndustriaPais.text = ((int)EstadisticasController.estadisticasController.getIndustriaArabiaACT()).ToString();
        txtQuemaPais.text = ((int)EstadisticasController.estadisticasController.getPlasticoQuemadoArabiaACT()).ToString();
        panelInfoPais.SetActive(isShowingInfo);
    }

    public void BrasilAction(){
        isShowingInfo = true;
        imgBandera.sprite = sp_BanderaBrasil;
        txtNombrePais.text = "BRASIL";
        txtDescripcionPais.text = "Es un país soberano de América del Sur que comprende la mitad oriental del subcontinente y algunos grupos de pequeñas islas en el océano Atlántico. Su idioma oficial es el portugués.";
        txtVehiculosPais.text = ((int)EstadisticasController.estadisticasController.getVehiculoCombustibleBrasilACT()).ToString();
        txtIndustriaPais.text = ((int)EstadisticasController.estadisticasController.getIndustriaBrasilACT()).ToString();
        txtQuemaPais.text = ((int)EstadisticasController.estadisticasController.getPlasticoQuemadoBrasilACT()).ToString();
        panelInfoPais.SetActive(isShowingInfo);
    }

    public void BackAction(){
        ResetearDatosEstadisticas();
        isShowingInfo = false;
        panelInfoPais.SetActive(isShowingInfo);
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
        GestionarGreenCoins(2);
        if(añoActual == 2120)
        {
            JuegoTerminado();
        }

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
        else if(emisionesCo2 < 32000000000 && emisionesCo2>= 31500000000)
        {
            estado = "EMISIONES DE CO2 MODERADAS";
            txtEstado.color = Color.blue;
        }
        else if(emisionesCo2 < 31500000000)
        {
            estado = "VAS SALVANDO EL PLANETA";
            txtEstado.color = Color.green;
        }


        txtEstado.text = "Estado: " + estado;
    }

    private void JuegoTerminado()
    {
        panelFinal.SetActive(true);
        txtMensajeFinal.text = "Han pasado 100 años desde que empezaste. El juego ha finalizado. El CO2 en la atmosfera previsto para este año era de 988 PPM. Gracias a tus acciones, lograste reducirlo a " + ((int)EstadisticasController.estadisticasController.getCo2AtmosferaACT()) + " PPM.";
    }

    public int getNivel()
    {
        return nivel;
    }

    private void ConocerObjetoActivo()
    {
        if(TiendaController.tiendaController.getPrioridadActivo())
        {
            imgObjeto.sprite = sp_Prioridad;
        }
        else if(TiendaController.tiendaController.getConcentracionActivo())
        {
            imgObjeto.sprite = sp_Concnetracion;
        }
        else if(TiendaController.tiendaController.getGenioActivo())
        {
            imgObjeto.sprite = sp_Genio;
        }
        else
        {
            imgObjeto.sprite = null;
        }
    }


   


}
