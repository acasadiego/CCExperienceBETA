using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{

    public static MissionController misionController;
    public Dropdown dropdownMisiones;

    public Image img_Bandera;

    public Sprite sp_BanderaChina, sp_BanderaUSA, sp_BanderaAlemania, sp_BanderaArabia, sp_BanderaBrasil;

    public GameObject Panel_Mision1,Panel_Mision2,Panel_Mision3, Panel_Misiones, Panel_MisionInfo, Panel_ResultadoMission, Panel_Negociando, List_Missions;

    public Text txt_Nombre, txt_ProbabilidadExito, txt_Descripcion, txt_Costo, txt_Duracion, txt_Recompensa, txt_Advertencia, txt_Exito, txt_Resultado, txt_InfoResultado;
    private GameObject panelObjects;

    public GameObject img_Cargando;
    
    private List<Mision> misiones, misionesMostradas, misionesChina, misionesEU, misionesAlemania, misionesArabia, misionesBrasil;

    string[] emisionCo2ChVariables;
    string[] poblacionChVariables;

    string[] emisionCo2USVariables;
    string[] poblacionUSVariables;

    string[] emisionCo2AlVariables;
    string[] poblacionAlVariables;

    string[] emisionCo2ArVariables;
    string[] poblacionArVariables;

    string[] emisionCo2BrVariables;
    string[] poblacionBrVariables;
    string[] arbolesVariables;

    Mision misionSeleccionada;
    double timer;

    bool animandoCargando, mostrandoAdvertencia;

    string estado;


    void Start()
    {
        estado = "EMISIONES DE CO2 ALTAS";
        timer = 0;
         misionController = this;
         panelObjects = new GameObject("panelObjects");
         emisionCo2ChVariables = new string[4]{"tasaCompraCh","tasaIndustrialCh","trabajadorIndustriaCh","tasaQuemaCh"};
         poblacionChVariables = new string[2]{"tasaDeNatalidadCh","tasaDeMortalidadCh"};
         emisionCo2USVariables = new string[4]{"tasaCompraUS","tasaIndustrialUS","trabajadorIndustriaUS","tasaQuemaUS"};
         poblacionUSVariables = new string[2]{"tasaDeNatalidadUS","tasaDeMortalidadUS"};
         emisionCo2AlVariables = new string[4]{"tasaCompraAl","tasaIndustrialAl","trabajadorIndustriaAl","tasaQuemaAl"};
         poblacionAlVariables = new string[2]{"tasaDeNatalidadAl","tasaDeMortalidadAl"};
         emisionCo2ArVariables = new string[4]{"tasaCompraAr","tasaIndustrialAr","trabajadorIndustriaAr","tasaQuemaAr"};
         poblacionArVariables = new string[2]{"tasaDeNatalidadAr","tasaDeMortalidadAr"};
         emisionCo2BrVariables = new string[4]{"tasaCompraBr","tasaIndustrialBr","trabajadorIndustriaBr","tasaQuemaBr"};
         poblacionBrVariables = new string[2]{"tasaDeNatalidadBr","tasaDeMortalidadBr"};
         arbolesVariables = new string[10]{"tasaTalaCh","tasaPlantacionCh","tasaTalaUS","tasaPlantacionUS","tasaTalaAl","tasaPlantacionAl","tasaTalaAr","tasaPlantacionAr","tasaTalaBr","tasaPlantacionBr"};
         misiones = new List<Mision>();
         AgregarMisiones();
         GenerarMisiones();
         
    }

    void Update()
    {   
        if(animandoCargando)
        {
            timer += Time.deltaTime;

            if(timer>3)
            {
                animandoCargando=false;
                timer=0;
                img_Cargando.GetComponent<Animator>().SetBool("cargando",false);
                HacerMisionAction();
            }
            
        }

        if(mostrandoAdvertencia)
        {
            timer += Time.deltaTime;

            if(timer>3)
            {
                mostrandoAdvertencia = false;
                timer = 0;
                txt_Advertencia.gameObject.SetActive(false);
            }
        }

        
    }


     //Control de las misiones que aparecen en pantalla
    public void DropdownMisiones(int index) {

        Panel_Mision1.gameObject.SetActive(false);
        Panel_Mision2.gameObject.SetActive(false);
        Panel_Mision3.gameObject.SetActive(false);


        int contador = 0;
        
        switch (index)
        {
            case 0:
                misionesMostradas = misionesChina;
                img_Bandera.sprite = sp_BanderaChina;
                break;
            case 1:
                misionesMostradas = misionesEU;
                img_Bandera.sprite = sp_BanderaUSA;
                break;
            case 2:
                misionesMostradas = misionesAlemania;
                img_Bandera.sprite = sp_BanderaAlemania;
                break;
            case 3:
                misionesMostradas = misionesArabia;
                img_Bandera.sprite = sp_BanderaArabia;
                break;
            case 4:
                misionesMostradas = misionesBrasil;
                img_Bandera.sprite = sp_BanderaBrasil;
                break;
        }


        for(int i = 0; i < misionesMostradas.Count;i++){
                
                Text textAuxiliar;

                switch (contador)
                {
                    case 0:
                        Panel_Mision1.gameObject.SetActive(true);
                        textAuxiliar = Panel_Mision1.GetComponentInChildren<Text>();
                        textAuxiliar.text = misionesMostradas[i].getNombre();
                        contador++;
                        break;
                    case 1:
                        Panel_Mision2.gameObject.SetActive(true);
                        textAuxiliar = Panel_Mision2.GetComponentInChildren<Text>();
                        textAuxiliar.text = misionesMostradas[i].getNombre();
                        contador++;
                        break;
                    case 2:
                        Panel_Mision3.gameObject.SetActive(true);
                        textAuxiliar = Panel_Mision3.GetComponentInChildren<Text>();
                        textAuxiliar.text = misionesMostradas[i].getNombre();
                        contador++;
                        break;


                }
          
        }
                     
    }

    public void GenerarMisiones()
    {
         img_Bandera.sprite = sp_BanderaChina;
         dropdownMisiones.SetValueWithoutNotify(0);
         misionesChina = FiltrarMisiones(0);
         misionesEU = FiltrarMisiones(1);
         misionesAlemania = FiltrarMisiones(2);
         misionesArabia = FiltrarMisiones(3);
         misionesBrasil = FiltrarMisiones(4);
         DropdownMisiones(0);

    }


    public List<Mision> FiltrarMisiones(int indexPais)
    {
        List<Mision> misionesAFiltrar = new List<Mision>(); 

        for(int i = 0; i < misiones.Count;i++)
        {
            int nivelActualVariable = ObtenerNivelVariable(misiones[i].getVariable());

            if(misiones[i].getIndexPais() == indexPais && nivelActualVariable == misiones[i].getNivel())
            {
                misionesAFiltrar.Add(misiones[i]);

            }

           
        }

        while(misionesAFiltrar.Count>3)
        {
            float misionEliminada = Random.Range(-0.0f,misionesAFiltrar.Count);

            misionesAFiltrar.RemoveAt((int)misionEliminada);
        }

        return misionesAFiltrar;

    }

    public void ShowMision1Details()
    {
       SetTextDetails(0);
       misionSeleccionada = misionesMostradas[0];
    }

    public void ShowMision2Details()
    {
       SetTextDetails(1);
       misionSeleccionada = misionesMostradas[1];
    }

    public void ShowMision3Details()
    {
       SetTextDetails(2);
       misionSeleccionada = misionesMostradas[2];
    }

    private void SetTextDetails(int misionnumber)
    {
       Panel_MisionInfo.SetActive(true);
       List_Missions.SetActive(false);
       txt_Nombre.text = misionesMostradas[misionnumber].getNombre();
       txt_ProbabilidadExito.text = misionesMostradas[misionnumber].getProbabilidadExito().ToString();
       txt_Descripcion.text = misionesMostradas[misionnumber].getDescripcion();
       txt_Costo.text = misionesMostradas[misionnumber].getCosto().ToString();
       txt_Duracion.text = misionesMostradas[misionnumber].getDuracion().ToString() + " años.";
       txt_Recompensa.text = misionesMostradas[misionnumber].getRecompensa().ToString();
    }

   public void HideMisionDetails()
   {
      Panel_MisionInfo.SetActive(false);
      List_Missions.SetActive(true);
   }

   private void AgregarMisiones()
   {
       Mision nuevaMision;

       misiones.Add(nuevaMision= new Mision("Control de natalidad en Estados Unidos", "tasaDeNatalidadUS","Legisla una nueva ley que comprometa a los ciudadanos de EU a no tener más de tres hijos por pareja.",1,65,13,1,3,15,-0.2));
       misiones.Add(nuevaMision= new Mision("Camapañas contra la tala de arboles en China", "tasaTalaCh","Reduce la tasa de tala",1,80,13,0,2,14,-0.2));
       misiones.Add(nuevaMision= new Mision("Campaña para plantar arboles en China", "tasaPlantacionCh","Aumenta la tasa de plantacion",1,34,13,0,4,13,0.003));
       misiones.Add(nuevaMision= new Mision("Inversión en automoviles electricos en China", "tasaCompraCh","Reduce la tasa de compra de automoviles a base de combustible fósil",1,20,13,0,1,15,-0.005));
       misiones.Add(nuevaMision= new Mision("Control de natalidad en Estados Unidos", "tasaDeNatalidadUS","Legisla una nueva ley que comprometa a los ciudadanos de EU a no tener más de dos hijos por pareja.",2,65,13,1,1,14,-0.5));
       misiones.Add(nuevaMision= new Mision("Camapañas contra la tala de arboles en China", "tasaTalaCh","Reduce la tasa de tala",-2,80,13,0,2,13,-0.004));
       misiones.Add(nuevaMision= new Mision("Inversión en automoviles electricos en China", "tasaCompraCh","Reduce la tasa de compra de automoviles a base de combustible fósil",2,20,13,0,3,15,-0.008));
       misiones.Add(nuevaMision= new Mision("Control de natalidad en Estados Unidos", "tasaDeNatalidadUS","Legisla una nueva ley que comprometa a los ciudadanos de EU a no tener más de un hijo por pareja.",3,65,13,1,4,14,-1));
       misiones.Add(nuevaMision= new Mision("Camapañas contra la tala de arboles en China", "tasaTalaCh","Reduce la tasa de tala",-3,80,13,0,1,13,-0.008));
       misiones.Add(nuevaMision= new Mision("Control de natalidad en Estados Unidos", "tasaDeNatalidadUS","Legisla una nueva ley que comprometa a los ciudadanos de EU a no tener ningún hijo hasta nuevo aviso.",4,65,13,1,2,15,-1.2));
       misiones.Add(nuevaMision= new Mision("Control de natalidad en Alemania", "tasaDeNatalidadAl","Legisla una nueva ley que comprometa a los ciudadanos de Alemania a no tener más de tres hijos por pareja.",1,65,13,2,3,14,-1.2));
       misiones.Add(nuevaMision= new Mision("Control de natalidad en Arabia", "tasaDeNatalidadAr","Legisla una nueva ley que comprometa a los ciudadanos de Arabia Saudita a no tener más de tres hijos por pareja.",1,65,13,3,4,14,-0.2));
       misiones.Add(nuevaMision= new Mision("Control de natalidad en Brasil", "tasaDeNatalidadBr","Legisla una nueva ley que comprometa a los ciudadanos de Brasil a no tener más de tres hijos por pareja.",1,65,13,4,2,13,-0.2));
       misiones.Add(nuevaMision= new Mision("Camapañas contra la tala de arboles en EU", "tasaTalaUS","Reduce la tasa de tala",1,80,13,1,1,12,-0.004));
       misiones.Add(nuevaMision= new Mision("Campaña para plantar arboles en Alemania", "tasaPlantacionAl","Aumenta la tasa de plantacion",1,34,13,2,3,12,0.003));
   }

    private int ObtenerNivelVariable(string variable)
    {
        int nivelActualVariable = 1;

        for(int i=0;i<emisionCo2ChVariables.Length;i++)
        {
            if(emisionCo2ChVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIEmisionCO2Ch(); break;}
        }

        for(int i=0;i<poblacionChVariables.Length;i++)
        {
            if(poblacionChVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIPoblacionCh(); break;}
        }

        for(int i=0;i<emisionCo2USVariables.Length;i++)
        {
            if(emisionCo2USVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIEmisionCO2US(); break;}
        }

        for(int i=0;i<poblacionUSVariables.Length;i++)
        {
            if(poblacionUSVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIPoblacionUS(); break;}
        }

        for(int i=0;i<emisionCo2AlVariables.Length;i++)
        {
            if(emisionCo2AlVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIEmisionCO2Al(); break;}
        }

        for(int i=0;i<poblacionAlVariables.Length;i++)
        {
            if(poblacionAlVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIPoblacionAl(); break;}
        }

        for(int i=0;i<emisionCo2ArVariables.Length;i++)
        {
            if(emisionCo2ArVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIEmisionCO2Ar(); break;}
        }

        for(int i=0;i<poblacionArVariables.Length;i++)
        {
            if(poblacionArVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIPoblacionAr(); break;}
        }

        for(int i=0;i<emisionCo2BrVariables.Length;i++)
        {
            if(emisionCo2BrVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIEmisionCO2Br(); break;}
        }

        for(int i=0;i<poblacionBrVariables.Length;i++)
        {
            if(poblacionBrVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIPoblacionBr(); break;}
        }



        for(int i=0;i<arbolesVariables.Length;i++)
        {
            if(arbolesVariables[i] == variable)
            { nivelActualVariable = ObtenerPRIArboles();  break;}
        }

        return nivelActualVariable;
    }

    private int ObtenerPRIEmisionCO2Ch()
    {
        double PRI = EstadisticasController.estadisticasController.getEmisionCo2ChinaACT()/EstadisticasController.estadisticasController.getEmisionCo2ChinaINI(); //PRI es el Porcentaje actual Respecto al Inicial.}
        return ComprobarNivelVariableEmisionCO2(PRI);
    }

    private int ObtenerPRIPoblacionCh()
    {
        double PRI = EstadisticasController.estadisticasController.getPoblacionChinaACT()/EstadisticasController.estadisticasController.getPoblacionChinaINI(); //PRI es el Porcentaje actual Respecto al Inicial.
        return ComprobarNivelVariablePoblacion(PRI);
    }

    private int ObtenerPRIEmisionCO2US()
    {
        double PRI = EstadisticasController.estadisticasController.getEmisionCo2USAACT()/EstadisticasController.estadisticasController.getEmisionCo2USAINI(); //PRI es el Porcentaje actual Respecto al Inicial.}
        return ComprobarNivelVariableEmisionCO2(PRI);
    }

    private int ObtenerPRIPoblacionUS()
    {
        double PRI = EstadisticasController.estadisticasController.getPoblacionUSAACT()/EstadisticasController.estadisticasController.getPoblacionUSAINI(); //PRI es el Porcentaje actual Respecto al Inicial.
        return ComprobarNivelVariablePoblacion(PRI);
    }

    private int ObtenerPRIEmisionCO2Al()
    {
        double PRI = EstadisticasController.estadisticasController.getEmisionCo2AlemaniaACT()/EstadisticasController.estadisticasController.getEmisionCo2AlemaniaINI(); //PRI es el Porcentaje actual Respecto al Inicial.}
        return ComprobarNivelVariableEmisionCO2(PRI);
    }

    private int ObtenerPRIPoblacionAl()
    {
        double PRI = EstadisticasController.estadisticasController.getPoblacionAlemaniaACT()/EstadisticasController.estadisticasController.getPoblacionAlemaniaINI(); //PRI es el Porcentaje actual Respecto al Inicial.
        return ComprobarNivelVariablePoblacion(PRI);
    }

    private int ObtenerPRIEmisionCO2Ar()
    {
        double PRI = EstadisticasController.estadisticasController.getEmisionCo2ArabiaACT()/EstadisticasController.estadisticasController.getEmisionCo2ArabiaINI(); //PRI es el Porcentaje actual Respecto al Inicial.}
        return ComprobarNivelVariableEmisionCO2(PRI);
    }

    private int ObtenerPRIPoblacionAr()
    {
        double PRI = EstadisticasController.estadisticasController.getPoblacionArabiaACT()/EstadisticasController.estadisticasController.getPoblacionArabiaINI(); //PRI es el Porcentaje actual Respecto al Inicial.
        return ComprobarNivelVariablePoblacion(PRI);
    }

    private int ObtenerPRIEmisionCO2Br()
    {
        double PRI = EstadisticasController.estadisticasController.getEmisionCo2BrasilACT()/EstadisticasController.estadisticasController.getEmisionCo2BrasilINI(); //PRI es el Porcentaje actual Respecto al Inicial.}
        return ComprobarNivelVariableEmisionCO2(PRI);
    }

    private int ObtenerPRIPoblacionBr()
    {
        double PRI = EstadisticasController.estadisticasController.getPoblacionBrasilACT()/EstadisticasController.estadisticasController.getPoblacionBrasilINI(); //PRI es el Porcentaje actual Respecto al Inicial.
        return ComprobarNivelVariablePoblacion(PRI);
    }

    private int ObtenerPRIArboles()
    {
        double PRI = EstadisticasController.estadisticasController.getArbolesACT()/EstadisticasController.estadisticasController.getArbolesINI(); //PRI es el Porcentaje actual Respecto al Inicial.
        return ComprobarNivelVariableArboles(PRI);
    }

    private int ComprobarNivelVariableEmisionCO2(double PRI)
    {
        int nivelActualVariable = 1;

        if(PRI >=1 && PRI<2)
        {
          nivelActualVariable = 1;  
        }
        else if(PRI>=2 && PRI<4)
        {
          nivelActualVariable = 2;
        }
        else if(PRI>=4 && PRI<7)
        {
            nivelActualVariable = 3;
        }
        else if(PRI>=7 && PRI<9)
        {
            nivelActualVariable=4;
        }
        else if(PRI>=9)
        {
            nivelActualVariable=5;
        }
        else if(PRI<=0.9f && PRI>0.8f)
        {
            nivelActualVariable= -2;
        }
        else if(PRI<=0.8f && PRI>0.6f)
        {
            nivelActualVariable = -3;
        }
        else if(PRI<=0.6f && PRI>0.4f)
        {
            nivelActualVariable = -4;
        }
        else if(PRI<=0.4f)
        {
            nivelActualVariable = -5;
        }

        return nivelActualVariable;
    }

    private int ComprobarNivelVariablePoblacion(double PRI)
    {
        int nivelActualVariable = 1;

        if(PRI >=1 && PRI<2)
        {
          nivelActualVariable = 1;  
        }
        else if(PRI>=2 && PRI<3)
        {
          nivelActualVariable = 2;
        }
        else if(PRI>=3 && PRI<5)
        {
            nivelActualVariable = 3;
        }
        else if(PRI>=5)
        {
            nivelActualVariable=4;
        }
        else if(PRI<=0.8f && PRI>0.4f)
        {
            nivelActualVariable= -2;
        }
        else if(PRI<=0.4f && PRI>0.1f)
        {
            nivelActualVariable = -3;
        }
        else if(PRI<=0.1f && PRI>0.05f)
        {
            nivelActualVariable = -4;
        }
        else if(PRI<=0.05f)
        {
            nivelActualVariable = 5;
        }

        return nivelActualVariable;
    }

    private int ComprobarNivelVariableArboles(double PRI)
    {
        int nivelActualVariable = 1;
        

        if(PRI >=1 && PRI<1.1f)
        {
          nivelActualVariable = 1;  
        }
        else if(PRI>=1.1f && PRI<1.3f)
        {
          nivelActualVariable = 2;
        }
        else if(PRI>=1.3f && PRI<1.5f)
        {
            nivelActualVariable = 3;
        }
        else if(PRI>=1.5f)
        {
            nivelActualVariable=4;
        }
        else if(PRI<=0.8f && PRI>0.6f)
        {
            nivelActualVariable= -2;
        }
        else if(PRI<=0.6f && PRI>0.2f)
        {
            nivelActualVariable = -3;
        }
        else if(PRI<=0.2f)
        {
            nivelActualVariable = -4;
        }


        return nivelActualVariable;
    }

    public void ImplementandoAction()
    {
        if(misionSeleccionada.getCosto() <= PlaySceneController.playSceneController.getGreenCoins())
        {
            PlaySceneController.playSceneController.GestionarGreenCoins(misionSeleccionada.getCosto()*-1);
            Panel_MisionInfo.SetActive(false);
            Panel_Negociando.SetActive(true);


            img_Cargando.GetComponent<Animator>().SetBool("cargando",true);
            animandoCargando = true;
        }
        else
        {
            txt_Advertencia.gameObject.SetActive(true);
            mostrandoAdvertencia = true;
        }
        


    }

    public void HacerMisionAction()
    {
        PlaySceneController.playSceneController.añadirExperiencia(2);
        Panel_Negociando.SetActive(false);
        Panel_ResultadoMission.SetActive(true);

        float aleatorio = Random.Range(0f, 100f);

        int probabilidadExito = misionSeleccionada.getProbabilidadExito();

        if(TiendaController.tiendaController.getGenioActivo())
        { probabilidadExito = 100; TiendaController.tiendaController.setGenioActivo(false);}

        if(probabilidadExito >= aleatorio)
        {
            PlaySceneController.playSceneController.añadirExperiencia(3);
            PlaySceneController.playSceneController.GestionarGreenCoins(misionSeleccionada.getRecompensa());
            txt_Exito.text = "¡LA MISION TUVO EXITO!";
            txt_Resultado.gameObject.SetActive(true);
            txt_InfoResultado.gameObject.SetActive(true);

            switch(misionSeleccionada.getVariable())
            {
                case "tasaCompraCh":

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaCompraCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de natalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de mortalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialCh(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaQuemaCh(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaTalaCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaPlantacionCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaCompraUS":

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaCompraUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de natalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de mortalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialUS(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaQuemaUS(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaTalaUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaPlantacionUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaCompraAl":

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaCompraAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de natalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de mortalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialAl(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaQuemaAl(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaTalaAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaPlantacionAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaCompraAr":

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaCompraAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de natalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de mortalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialAr(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaQuemaAr(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaTalaAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaPlantacionAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaCompraBr":

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaCompraBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de natalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de mortalidad poblacional en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialBr(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaQuemaBr(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaTalaBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito(); 
                    EstadisticasController.estadisticasController.alterarTasaPlantacionBr(misionSeleccionada.getValorExito());
                    break;
            }
        }
        else
        {
            txt_Exito.text = "¡LA MISION NO TUVO EXITO!";
            txt_Resultado.gameObject.SetActive(false);
            txt_InfoResultado.gameObject.SetActive(false);
        }

        int duracion = misionSeleccionada.getDuracion();

        if(TiendaController.tiendaController.getPrioridadActivo())
        {
            duracion = duracion/2;
            TiendaController.tiendaController.setPrioridadActivo(false);
        }

        if(TiendaController.tiendaController.getConcentracionActivo())
        {
            TiendaController.tiendaController.setConcentracionActivo(false);

            if(misionSeleccionada == misionesMostradas[0])
            {
                Panel_Mision1.SetActive(false);
            }
            else if(misionSeleccionada == misionesMostradas[1])
            {
                Panel_Mision2.SetActive(false);
            }
            else
            {
                Panel_Mision3.SetActive(false);
            }

            for(int i=0;i<misionesChina.Count;i++)
            {
                if(misionSeleccionada == misionesChina[i])
                {
                    misionesChina.RemoveAt(i);
                }
            }

            for(int i=0;i<misionesEU.Count;i++)
            {
                if(misionSeleccionada == misionesEU[i])
                {
                    misionesEU.RemoveAt(i);
                }
            }

            for(int i=0;i<misionesAlemania.Count;i++)
            {
                if(misionSeleccionada == misionesAlemania[i])
                {
                    misionesAlemania.RemoveAt(i);
                }
            }

            for(int i=0;i<misionesArabia.Count;i++)
            {
                if(misionSeleccionada == misionesArabia[i])
                {
                    misionesArabia.RemoveAt(i);
                }
            }

            for(int i=0;i<misionesBrasil.Count;i++)
            {
                if(misionSeleccionada == misionesBrasil[i])
                {
                    misionesBrasil.RemoveAt(i);
                }
            }
        }
        else
        {
             for(int i=0; i<duracion;i++)
             {
                PlaySceneController.playSceneController.AvanzarAñoAction();
             }
        }
    



        
    }

    public void EntendidoAction()
    {
        Panel_ResultadoMission.SetActive(false);
        List_Missions.SetActive(true);

    }

    private string comprobarAccion(double valor)
    {
        string accion = "";
        if(valor>0)
        {
            accion = "aumentó";
        }
        else
        {
            accion = "disminuyó";
        }

        return accion;
    }


    
}
