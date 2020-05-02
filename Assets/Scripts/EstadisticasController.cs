using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EstadisticasController : MonoBehaviour
{

    public static EstadisticasController estadisticasController;

    //Declaracion de niveles, flujos y variables auxiliares del modelo como variables de C#.


    //Variables correspondientes a text.
    public Text txtCo2Atmosfera,txtYear,txtPoblacion,txtCo2Ant,txtArboles, txtEjeY;
    public InputField inpYearSimulacion;
    public Dropdown dd_Paises;
    public Image img_Bandera;

    public Sprite sp_BanderaONU, sp_BanderaChina, sp_BanderaUSA, sp_BanderaAlemania, sp_BanderaArabia, sp_BanderaBrasil;

    //Variables de valores iniciales de algunos niveles
    private double poblacionChinaINI, emisionCo2ChinaINI, poblacionUSAINI,emisionCo2USAINI,poblacionAlemaniaINI,emisionCo2AlemaniaINI,poblacionArabiaINI,emisionCo2ArabiaINI,poblacionBrasilINI,emisionCo2BrasilINI,arbolesINI; 


//Variables del modelo dinamico sistemico (Sujetas a simulacion)

            //China y General
    private double year,yearSimulacion,trillon,billon,millon;
    private double poblacionChina,muertesCh,nacimientosCh,tasaDeNatalidadCh,tasaDeMortalidadCh,efectoSobreMuertes,capacidadCargaMuertes;
    private double emisionCo2Ant,emisionCo2China, industriaCh,tasaIndustrialCh,trabajadorIndustriaCh,industriaPersonasCh,co2Industria,co2Persona,tasaCompraCh,vehiculoCombustibleCh,co2Vehiculo,plasticoQuemadoCh,tasaQuemaCh,co2PlasticoQuemado,co2AntAtm,tasaCo2AntAtm,co2ChAnt,tasaCo2ChAnt;

    private double co2Atmosfera,co2AH2CO3,absorcionCO2PlanMar,co2AC,co2CapturadoArbol,co2Actual,ppmActual;

    private double cLitosfera,cACO2,cO2ExpSuperficie;

    private double h2CO3Hidrosfera,h2CO3ACO2,tasaCO2ExpAgua;

    private double arboles,nacimientosArb,tasaNatalidadArb,tasaPlantacionCh,tasaPlantacionUS,tasaPlantacionAl,tasaPlantacionAr,tasaPlantacionBr,plantacion,tasaTalaCh,tasaTalaUS,tasaTalaAl,tasaTalaAr,tasaTalaBr,capacidadCargaIncendios,tasaIncendios,deforestacion;



                //USA
    
    private double poblacionUSA,muertesUS,nacimientosUS,tasaDeNatalidadUS,tasaDeMortalidadUS;
    private double emisionCo2USA, industriaUS,tasaIndustrialUS,trabajadorIndustriaUS,industriaPersonasUS,tasaCompraUS,vehiculoCombustibleUS,plasticoQuemadoUS,tasaQuemaUS,co2USAnt,tasaCo2USAnt;

                //Alemania

    private double poblacionAlemania,muertesAl,nacimientosAl,tasaDeNatalidadAl,tasaDeMortalidadAl;
    private double emisionCo2Alemania, industriaAl,tasaIndustrialAl,trabajadorIndustriaAl,industriaPersonasAl,tasaCompraAl,vehiculoCombustibleAl,plasticoQuemadoAl,tasaQuemaAl,co2AlAnt,tasaCo2AlAnt;

                //Arabia Saudita
    private double poblacionArabia,muertesAr,nacimientosAr,tasaDeNatalidadAr,tasaDeMortalidadAr;
    private double emisionCo2Arabia, industriaAr,tasaIndustrialAr,trabajadorIndustriaAr,industriaPersonasAr,tasaCompraAr,vehiculoCombustibleAr,plasticoQuemadoAr,tasaQuemaAr,co2ArAnt,tasaCo2ArAnt;

                //Brasil
    private double poblacionBrasil,muertesBr,nacimientosBr,tasaDeNatalidadBr,tasaDeMortalidadBr;
    private double emisionCo2Brasil, industriaBr,tasaIndustrialBr,trabajadorIndustriaBr,industriaPersonasBr,tasaCompraBr,vehiculoCombustibleBr,plasticoQuemadoBr,tasaQuemaBr,co2BrAnt,tasaCo2BrAnt;

                //Resto del Mundo
    private double emisionCo2Restante, co2RestoMundo, co2InicialRes, efectoAumentoCo2Res,co2ResAnt,tasaCo2ResAnt;




    private bool simulacion; //Variable que indica cuando se está realizando una simulación, para que el TEXT del año actual no se vea alterado por ello.
    private int lapsoSimulacionGrafica, espacioAños; /*Variable "espacioAños" : Espacio entre años del eje X de 
            la grafica (Ej: la grafica solo mostrará años de 10 en 10, luego espacioAños = 10)*/

    private string variableGrafica,ultimoAñoSimulacion;

    


//Lista donde se almacenan los datos que se mostrarán en la gráfica.
     private List<double> datosGrafica;



//Variables del modelo dinamico sistemico (Que se encargan de almacenar los valores del AÑO ACTUAL)

        //China y General
 private double yearACT, poblacionChACT, co2AtmosferaACT, muertesChACT, nacimientosChACT, industriaChACT, vehiculoCombustibleChACT, plasticoQuemadoChACT,
                    emisionCo2AntACT, co2AntAtmACT, arbolesACT, nacimientosArbACT, plantacionACT, deforestacionACT, cLitosferaACT, h2CO3HidrosferaACT, co2ACACT,
                        cACO2ACT, co2AH2CO3ACT, h2CO3ACO2ACT, emisionCo2ChACT, co2ChAntACT;

        //USA
private double poblacionUSACT, muertesUSACT, nacimientosUSACT, industriaUSACT, vehiculoCombustibleUSACT, plasticoQuemadoUSACT,
                    emisionCo2USACT, co2USAntACT;

        //Alemania

private double poblacionAlACT, muertesAlACT, nacimientosAlACT, industriaAlACT, vehiculoCombustibleAlACT, plasticoQuemadoAlACT,
                    emisionCo2AlACT, co2AlAntACT;

        //Arabia Saudita

private double poblacionArACT, muertesArACT, nacimientosArACT, industriaArACT, vehiculoCombustibleArACT, plasticoQuemadoArACT,
                    emisionCo2ArACT, co2ArAntACT;

        //Brasil

private double poblacionBrACT, muertesBrACT, nacimientosBrACT, industriaBrACT, vehiculoCombustibleBrACT, plasticoQuemadoBrACT,
                    emisionCo2BrACT, co2BrAntACT;

        //RestoDelMundo

private double emisionCo2ResACT, co2ResAntACT, co2RestoMundoACT;


private int indexPais;



    void Start()
    {
        estadisticasController = this;
        indexPais = 0;
        this.co2Actual = 2187050000000;
        this.ppmActual = 415;
        this.trillon = 1000000000000;
        this.billon = 1000000000;
        this.millon = 1000000;
        this.simulacion = false;
        this.lapsoSimulacionGrafica = 100;
        this.espacioAños = 10;
        variableGrafica = "co2Atmosfera";


        InicializarVariablesModelo();
        MantenerDatosActuales();
        ActualizarGrafica();
        ActualizarValores(0,false);

        

    }

    private void InicializarVariablesModelo()  //Valores iniciales de las variables del modelo para el año 2020.
    {

        //China y General
        year = 2020;
        yearSimulacion = year;
        poblacionChinaINI = 1395380000;
        poblacionChina = poblacionChinaINI;
        emisionCo2ChinaINI = 10800000000;
        co2Atmosfera = 415;
        capacidadCargaMuertes = 604.75;
        capacidadCargaIncendios = 415;
        tasaDeNatalidadCh = 12;
        tasaDeMortalidadCh = 7.11;
        EfectoMuertes();
        muertesCh = (tasaDeMortalidadCh*poblacionChina*efectoSobreMuertes)/1000;
        nacimientosCh = (tasaDeNatalidadCh*poblacionChina)/1000;
        emisionCo2Ant = 32000000000;
        emisionCo2China = emisionCo2ChinaINI;
        tasaIndustrialCh = 0.09;
        trabajadorIndustriaCh = 200;
        industriaPersonasCh = tasaIndustrialCh/trabajadorIndustriaCh;
        industriaCh = industriaPersonasCh * poblacionChina;
        co2Industria = 10000;
        co2Persona = 0.328;
        tasaCompraCh = 0.43;
        vehiculoCombustibleCh = poblacionChina*tasaCompraCh;
        co2Vehiculo = 2.336;
        tasaQuemaCh = 0.89;
        plasticoQuemadoCh = poblacionChina*tasaQuemaCh;
        co2PlasticoQuemado = 2.3;
        tasaCo2AntAtm = 0.98;
        tasaCo2ChAnt = 1;
        co2AntAtm = emisionCo2Ant * tasaCo2AntAtm;
        co2ChAnt = emisionCo2China * tasaCo2ChAnt;
        arbolesINI = 3000000000000;
        arboles = arbolesINI;
        tasaNatalidadArb = 0.0009;
        tasaPlantacionCh = 1;
        tasaPlantacionUS = 1;
        tasaPlantacionAl = 1;
        tasaPlantacionAr = 1;
        tasaPlantacionBr = 1;
        nacimientosArb=arboles*tasaNatalidadArb;
        plantacion = (poblacionChina*tasaPlantacionCh) + (poblacionUSA*tasaPlantacionUS)+ (poblacionAlemania*tasaPlantacionAl)+ (poblacionArabia*tasaPlantacionAr)+(poblacionBrasil*tasaPlantacionBr);
        tasaTalaCh = 2.6;
        tasaTalaUS = 2.6;
        tasaTalaAl = 2.6;
        tasaTalaAr = 2.6;
        tasaTalaBr = 2.6;
        TasaIncendios();
        deforestacion = ((poblacionChina*tasaTalaCh)+(poblacionUSA*tasaTalaUS)+(poblacionAlemania*tasaTalaAl)+(poblacionArabia*tasaTalaAr)+(poblacionBrasil*tasaTalaBr))*tasaIncendios;
        absorcionCO2PlanMar = 0.001;
        co2AH2CO3 = ((co2Atmosfera*co2Actual)/ppmActual)*absorcionCO2PlanMar;
        co2CapturadoArbol = 0.004;
        co2AC = co2CapturadoArbol*arboles;
        cLitosfera = 56000000000000000;
        cO2ExpSuperficie= 0.00000001;
        cACO2= cLitosfera*cO2ExpSuperficie;
        h2CO3Hidrosfera = 560000000000000;
        tasaCO2ExpAgua = 0.000001;
        h2CO3ACO2 = h2CO3Hidrosfera*tasaCO2ExpAgua;

        //USA
        poblacionUSAINI = 318582000;
        poblacionUSA = poblacionUSAINI;
        emisionCo2USAINI = 3700000000;
        tasaDeNatalidadUS = 11.2;
        tasaDeMortalidadUS = 8.6;
        muertesUS = (tasaDeMortalidadUS*poblacionUSA*efectoSobreMuertes)/1000;
        nacimientosUS = (tasaDeNatalidadUS*poblacionUSA)/1000;
        emisionCo2USA = emisionCo2USAINI;
        tasaIndustrialUS = 0.08;
        trabajadorIndustriaUS = 80;
        industriaPersonasUS = tasaIndustrialUS/trabajadorIndustriaUS;
        industriaUS = industriaPersonasUS * poblacionUSA;
        tasaCompraUS = 0.13;
        vehiculoCombustibleUS = poblacionUSA*tasaCompraUS;
        tasaQuemaUS = 0.56;
        plasticoQuemadoUS = poblacionUSA*tasaQuemaUS;
        tasaCo2USAnt = 1;
        co2USAnt = emisionCo2USA * tasaCo2USAnt;

        //Alemania
        poblacionAlemaniaINI = 83019200;
        poblacionAlemania = poblacionAlemaniaINI;
        emisionCo2AlemaniaINI = 750000000;
        tasaDeNatalidadAl = 9.3;
        tasaDeMortalidadAl = 11.3;
        muertesAl = (tasaDeMortalidadAl*poblacionAlemania*efectoSobreMuertes)/1000;
        nacimientosAl = (tasaDeNatalidadAl*poblacionAlemania)/1000;
        emisionCo2Alemania = emisionCo2AlemaniaINI;
        tasaIndustrialAl = 0.05;
        trabajadorIndustriaAl = 80;
        industriaPersonasAl = tasaIndustrialAl/trabajadorIndustriaAl;
        industriaAl = industriaPersonasAl * poblacionAlemania;
        tasaCompraAl = 0.55;
        vehiculoCombustibleAl = poblacionAlemania*tasaCompraAl;
        tasaQuemaAl = 0.56;
        plasticoQuemadoAl = poblacionAlemania*tasaQuemaAl;
        tasaCo2AlAnt = 1;
        co2AlAnt = emisionCo2Alemania * tasaCo2AlAnt;

        //Arabia Saudita
        poblacionArabiaINI = 33700000;
        poblacionArabia = poblacionArabiaINI;
        emisionCo2ArabiaINI = 665000000;
        tasaDeNatalidadAr = 17.3;
        tasaDeMortalidadAr = 3.45;
        muertesAr = (tasaDeMortalidadAr*poblacionArabia*efectoSobreMuertes)/1000;
        nacimientosAr = (tasaDeNatalidadAr*poblacionArabia)/1000;
        emisionCo2Arabia = emisionCo2ArabiaINI;
        tasaIndustrialAr = 0.14;
        trabajadorIndustriaAr = 80;
        industriaPersonasAr = tasaIndustrialAr/trabajadorIndustriaAr;
        industriaAr = industriaPersonasAr * poblacionArabia;
        tasaCompraAr = 0.45;
        vehiculoCombustibleAr = poblacionArabia*tasaCompraAr;
        tasaQuemaAr = 0.56;
        plasticoQuemadoAr = poblacionArabia*tasaQuemaAr;
        tasaCo2ArAnt = 1;
        co2ArAnt = emisionCo2Arabia * tasaCo2ArAnt;

        //Brasil
        poblacionBrasilINI = 209500000;
        poblacionBrasil = poblacionBrasilINI;
        emisionCo2BrasilINI = 500000000;
        tasaDeNatalidadBr = 13.95;
        tasaDeMortalidadBr = 6.7;
        muertesBr = (tasaDeMortalidadBr*poblacionBrasil*efectoSobreMuertes)/1000;
        nacimientosBr = (tasaDeNatalidadBr*poblacionBrasil)/1000;
        emisionCo2Brasil = emisionCo2BrasilINI;
        tasaIndustrialBr = 0.002;
        trabajadorIndustriaBr = 60;
        industriaPersonasBr = tasaIndustrialBr/trabajadorIndustriaBr;
        industriaBr = industriaPersonasBr * poblacionBrasil;
        tasaCompraBr = 0.006;
        vehiculoCombustibleBr = poblacionBrasil*tasaCompraBr;
        tasaQuemaBr = 0.76;
        plasticoQuemadoBr = poblacionBrasil*tasaQuemaBr;
        tasaCo2BrAnt = 1;
        co2BrAnt = emisionCo2Brasil * tasaCo2BrAnt;

        //Resto del Mundo
        emisionCo2Restante = 16000000000;
        EfectoAumentoCo2Res();
        co2InicialRes = 16000000000;
        co2RestoMundo = co2InicialRes*efectoAumentoCo2Res;
        tasaCo2ResAnt = 1;
        co2ResAnt = emisionCo2Restante*tasaCo2ResAnt;

    }

    private void ActualizarGrafica() //Actualiza la gráfica de la variable seleccionada.
    {
   
        datosGrafica = new List<double>();
        int yearGrafica = (int)year; //Se guarda la variable "año" en una auxiliar "año grafica", ya que la primera cambiará en las siguientes lineas debido a la simulación.

        double mayor = 0; /*Variable donde se almacenará el mayor de los valores a graficar, cuya unidad será tomada como
         referencia para todos los valores de la grafica*/ 


        for(int i = 0;i<lapsoSimulacionGrafica;i++)
        {
            
            if (i % espacioAños == 0)
            {
                datosGrafica.Add((DeterminarVariableModelo()));
                if(DeterminarVariableModelo() > mayor)
                { mayor = DeterminarVariableModelo(); }
            }

            AvanzarAño();

        }

        

        double cantidad = DeterminarUnidad(mayor);
        mayor = mayor / cantidad; /*Se reduce el tamaño del numero mayor segun su unidad (Ej: 3 trillones) para mas
        adelante pasarlo como parametro al metodo de la clase windows graph, donde se convertira en el numero mayo del
        eje Y de la grafica*/

        for(int i=0;i<datosGrafica.Count;i++)
        {
            datosGrafica[i] = (float)(datosGrafica[i] / cantidad);
        }


        string texto = "Toneladas";

        switch(variableGrafica)
        {
            case "co2Atmosfera":
                texto = "co2 Atmosfera (PPM)";
                break;
            case "emisionCo2Ant":
                texto = "emisiones co2 (Toneladas)";
                break;
            case "arboles":
                texto = "Arboles";
                break;
            case "emisionCo2China":
                texto = "emisiones co2 (Toneladas)";
                break;
            case "poblacionChina":
                texto = "Personas";
                break;
            case "emisionCo2USA":
                texto = "emisiones co2 (Toneladas)";
                break;
            case "poblacionUSA":
                texto = "Personas";
                break;
            case "emisionCo2Alemania":
                texto = "emisiones co2 (Toneladas)";
                break;
            case "poblacionAlemania":
                texto = "Personas";
                break;
            case "emisionCo2Arabia":
                texto = "emisiones co2 (Toneladas)";
                break;
            case "poblacionArabia":
                texto = "Personas";
                break;
            case "emisionCo2Brasil":
                texto = "emisiones co2 (Toneladas)";
                break;
            case "poblacionBrasil":
                texto = "Personas";
                break;
                
        }
        

        txtEjeY.text = texto + " (" + DeterminarUnidadTexto(cantidad) + ")";

        


        Window_Graph.window_Graph.cleanGraphic();
        Window_Graph.window_Graph.setvalueList(datosGrafica,yearGrafica,mayor); /* Se envia la variable año grafica, la cual representa el año inicial de la simulación en la grafica*/
        datosGrafica.Clear();

        RecuperarDatosActuales(); //Se reestablecen los datos actuales luego de graficar 
        

    }

    public void VerResultadosAction() //Muestra en la escena una simulación de las variables en un año especifico.
    {
        
        
        datosGrafica = new List<double>();
        MantenerDatosActuales();

        //¡MOMENTANEO! (Cambiar luego para mejora del código) Se hace un try catch en caso de que el usuario ingrese un valor NO númerico en la text box.
        try
        { this.yearSimulacion = double.Parse(inpYearSimulacion.text);}
        catch{ simulacion = false;} //Si el usuario ingreso un NUEVO valor NO númerico en el input field, la simulacion anterior termina
        
        double tiempo = yearSimulacion - yearACT;

        if (tiempo>0) /* Se pone esta condición porque para que se pueda realizar una simulacion, el año ingresado debe ser mayor que el año actual*/
        {
            this.simulacion = true;


            for(int i = 0;i<tiempo;i++)
            {
              AvanzarAño();
              
            }

            
        }
        else
        {   //Si el usuario ingresa un NUEVO valor en el input field equivalente al AÑO actual o menor del mismo, la simulación termina.
            simulacion = false;
        }

        datosGrafica.Clear();
        
        ActualizarValores(indexPais,false);
        ActualizarGrafica();

        /*Guarda el ultimo año de simulacion antes de resetear el texto de el input field, para luego utilizarlo
            en caso de que se quiera ver la grafica de otra variable del modelo para el mismo año*/
        ultimoAñoSimulacion = inpYearSimulacion.text;
        inpYearSimulacion.text = "";
        
    }
    

    private void MantenerDatosActuales()  //Guarda los valores de las variables en el AÑO ACTUAL, para que no se pierdan al momento de realizar alguna simulación.
    {
    
       //China y General
        yearACT = year;
        poblacionChACT = poblacionChina;
        co2AtmosferaACT = co2Atmosfera;
        muertesChACT = muertesCh;
        nacimientosChACT = nacimientosCh;
        industriaChACT = industriaCh;
        vehiculoCombustibleChACT = vehiculoCombustibleCh;
        plasticoQuemadoChACT = plasticoQuemadoCh;
        emisionCo2AntACT = emisionCo2Ant;
        emisionCo2ChACT = emisionCo2China;
        co2AntAtmACT = co2AntAtm;
        co2ChAntACT = co2ChAnt;
        arbolesACT = arboles;
        nacimientosArbACT = nacimientosArb;
        plantacionACT = plantacion;
        deforestacionACT = deforestacion;
        cLitosferaACT = cLitosfera; 
        h2CO3HidrosferaACT = h2CO3Hidrosfera;
        co2ACACT = co2AC;
        cACO2ACT = cACO2;
        co2AH2CO3ACT = co2AH2CO3;
        h2CO3ACO2ACT = h2CO3ACO2;

        //USA
        poblacionUSACT = poblacionUSA;
        muertesUSACT = muertesUS;
        nacimientosUSACT = nacimientosUS;
        industriaUSACT = industriaUS;
        vehiculoCombustibleUSACT = vehiculoCombustibleUS;
        plasticoQuemadoUSACT = plasticoQuemadoUS;
        emisionCo2USACT = emisionCo2USA;
        co2USAntACT = co2USAnt;

        //Alemania
        poblacionAlACT = poblacionAlemania;
        muertesAlACT = muertesAl;
        nacimientosAlACT = nacimientosAl;
        industriaAlACT = industriaAl;
        vehiculoCombustibleAlACT = vehiculoCombustibleAl;
        plasticoQuemadoAlACT = plasticoQuemadoAl;
        emisionCo2AlACT = emisionCo2Alemania;
        co2AlAntACT = co2AlAnt;

        //Arabia
        poblacionArACT = poblacionArabia;
        muertesArACT = muertesAr;
        nacimientosArACT = nacimientosAr;
        industriaArACT = industriaAr;
        vehiculoCombustibleArACT = vehiculoCombustibleAr;
        plasticoQuemadoArACT = plasticoQuemadoAr;
        emisionCo2ArACT = emisionCo2Arabia;
        co2ArAntACT = co2ArAnt;

        //Brasil
        poblacionBrACT = poblacionBrasil;
        muertesBrACT = muertesBr;
        nacimientosBrACT = nacimientosBr;
        industriaBrACT = industriaBr;
        vehiculoCombustibleBrACT = vehiculoCombustibleBr;
        plasticoQuemadoBrACT = plasticoQuemadoBr;
        emisionCo2BrACT = emisionCo2Brasil;
        co2BrAntACT = co2BrAnt;

        //Resto del Mundo
        co2RestoMundoACT = co2RestoMundo;
        emisionCo2ResACT = emisionCo2Restante; 
        co2ResAntACT = co2ResAnt; 
        
        
    }


    private void RecuperarDatosActuales() //Trae de vuelta los valores de las variables en el AÑO ACTUAL que hayan sido alterados por alguna simulación.
    {

        
        //China y General
        year = yearACT;
        poblacionChina = poblacionChACT;
        co2Atmosfera = co2AtmosferaACT;
        muertesCh = muertesChACT;
        nacimientosCh = nacimientosChACT;
        industriaCh = industriaChACT;
        vehiculoCombustibleCh = vehiculoCombustibleChACT;
        plasticoQuemadoCh = plasticoQuemadoChACT;
        emisionCo2Ant = emisionCo2AntACT;
        emisionCo2China = emisionCo2ChACT;
        co2AntAtm = co2AntAtmACT;
        co2ChAnt = co2ChAntACT;
        arboles = arbolesACT;
        nacimientosArb = nacimientosArbACT;
        plantacion = plantacionACT;
        deforestacion = deforestacionACT;
        cLitosfera = cLitosferaACT; 
        h2CO3Hidrosfera = h2CO3HidrosferaACT;
        co2AC = co2ACACT;
        cACO2 = cACO2ACT;
        co2AH2CO3 = co2AH2CO3ACT;
        h2CO3ACO2 = h2CO3ACO2ACT;

        //USA
        poblacionUSA = poblacionUSACT;
        muertesUS = muertesUSACT;
        nacimientosUS = nacimientosUSACT;
        industriaUS = industriaUSACT;
        vehiculoCombustibleUS = vehiculoCombustibleUSACT;
        plasticoQuemadoUS = plasticoQuemadoUSACT;
        emisionCo2USA = emisionCo2USACT;
        co2USAnt = co2USAntACT;

        //Alemania
        poblacionAlemania = poblacionAlACT;
        muertesAl = muertesAlACT;
        nacimientosAl = nacimientosAlACT;
        industriaAl = industriaAlACT;
        vehiculoCombustibleAl = vehiculoCombustibleAlACT;
        plasticoQuemadoAl = plasticoQuemadoAlACT;
        emisionCo2Alemania = emisionCo2AlACT;
        co2AlAnt = co2AlAntACT;

        //Arabia
        poblacionArabia = poblacionArACT;
        muertesAr = muertesArACT;
        nacimientosAr = nacimientosArACT;
        industriaAr = industriaArACT;
        vehiculoCombustibleAr = vehiculoCombustibleArACT;
        plasticoQuemadoAr = plasticoQuemadoArACT;
        emisionCo2Arabia = emisionCo2ArACT;
        co2ArAnt = co2ArAntACT;

        //Brasil
        poblacionBrasil = poblacionBrACT;
        muertesBr = muertesBrACT;
        nacimientosBr = nacimientosBrACT;
        industriaBr = industriaBrACT;
        vehiculoCombustibleBr = vehiculoCombustibleBrACT;
        plasticoQuemadoBr = plasticoQuemadoBrACT;
        emisionCo2Brasil = emisionCo2BrACT;
        co2BrAnt = co2BrAntACT;

        //Resto del Mundo
        co2RestoMundo = co2RestoMundoACT;
        emisionCo2Restante = emisionCo2ResACT; 
        co2ResAnt = co2ResAntACT; 

    }
    
    public void AvanzarAñoActual() /*Avanza el año ACTUAL del juego, el cual no puede ser retrocedido*/
    {
        simulacion = false; /*Al avanzar el año actual, se cancela toda simulacion existente*/
        AvanzarAño(); 
        MantenerDatosActuales();
        ActualizarGrafica();
        ActualizarValores(0,false); 
        inpYearSimulacion.placeholder.GetComponent<Text>().text = yearACT.ToString(); /* El placeholder del inputField "Año Simulacion" pasa a contener el texto del año actual. */

    }
    
    private void AvanzarAño() /*Avanza un año, este metodo se utilzia tanto para avanzar un año actual como para avanzar un año en la simulacion*/
    {

        //Ecuaciones de los niveles y flujos del modelo.



        //China y Global
        year++;
        poblacionChina = poblacionChina + nacimientosCh - muertesCh;
        co2Atmosfera = co2Atmosfera+ (((cACO2 + co2AntAtm + h2CO3ACO2 - co2AC - co2AH2CO3)*ppmActual)/co2Actual);
        EfectoMuertes();
        muertesCh = (tasaDeMortalidadCh*poblacionChina*efectoSobreMuertes)/1000;
        nacimientosCh = (tasaDeNatalidadCh*poblacionChina)/1000;
        industriaCh = industriaPersonasCh * poblacionChina;
        vehiculoCombustibleCh = poblacionChina*tasaCompraCh;
        plasticoQuemadoCh = poblacionChina*tasaQuemaCh;
        emisionCo2China = emisionCo2China + (co2Persona*poblacionChina)+(co2Vehiculo*vehiculoCombustibleCh)+(plasticoQuemadoCh*co2PlasticoQuemado)+(co2Industria*industriaCh)-co2ChAnt;
        emisionCo2Ant = emisionCo2Ant + co2ChAnt + co2USAnt + co2AlAnt + co2ArAnt + co2BrAnt + co2ResAnt - co2AntAtm;
        co2ChAnt = emisionCo2China*tasaCo2ChAnt;
        co2AntAtm = emisionCo2Ant*tasaCo2AntAtm;
        if(arboles>=deforestacion){arboles= arboles + plantacion + nacimientosArb - deforestacion;}
        TasaIncendios();
        nacimientosArb = tasaNatalidadArb*arboles;
        cLitosfera = cLitosfera + co2AC - cACO2; 
        h2CO3Hidrosfera = h2CO3Hidrosfera + co2AH2CO3 - h2CO3ACO2 ;
        if(co2Atmosfera >= 250){co2AC= co2CapturadoArbol * arboles;}
        cACO2 = cLitosfera * cO2ExpSuperficie;
        co2AH2CO3 = ((co2Atmosfera*co2Actual)/ppmActual) * absorcionCO2PlanMar;
        h2CO3ACO2 = h2CO3Hidrosfera * tasaCO2ExpAgua;

        //USA
        poblacionUSA = poblacionUSA + nacimientosUS - muertesUS;
        muertesUS = (tasaDeMortalidadUS*poblacionUSA*efectoSobreMuertes)/1000;
        nacimientosUS = (tasaDeNatalidadUS*poblacionUSA)/1000;
        industriaUS = industriaPersonasUS * poblacionUSA;
        vehiculoCombustibleUS = poblacionUSA*tasaCompraUS;
        plasticoQuemadoUS = poblacionUSA*tasaQuemaUS;
        emisionCo2USA = emisionCo2USA + (co2Persona*poblacionUSA)+(co2Vehiculo*vehiculoCombustibleUS)+(plasticoQuemadoUS*co2PlasticoQuemado)+(co2Industria*industriaUS)-co2USAnt;
        co2USAnt = emisionCo2USA*tasaCo2USAnt;

        //Alemania
        poblacionAlemania = poblacionAlemania + nacimientosAl - muertesAl;
        muertesAl = (tasaDeMortalidadAl*poblacionAlemania*efectoSobreMuertes)/1000;
        nacimientosAl = (tasaDeNatalidadAl*poblacionAlemania)/1000;
        industriaAl = industriaPersonasAl * poblacionAlemania;
        vehiculoCombustibleAl = poblacionAlemania*tasaCompraAl;
        plasticoQuemadoAl = poblacionAlemania*tasaQuemaAl;
        emisionCo2Alemania = emisionCo2Alemania + (co2Persona*poblacionAlemania)+(co2Vehiculo*vehiculoCombustibleAl)+(plasticoQuemadoAl*co2PlasticoQuemado)+(co2Industria*industriaAl)-co2AlAnt;
        co2AlAnt = emisionCo2Alemania*tasaCo2AlAnt;

        //Arabia Saudita
        poblacionArabia = poblacionArabia + nacimientosAr - muertesAr;
        muertesAr = (tasaDeMortalidadAr*poblacionArabia*efectoSobreMuertes)/1000;
        nacimientosAr = (tasaDeNatalidadAr*poblacionArabia)/1000;
        industriaAr = industriaPersonasAr * poblacionArabia;
        vehiculoCombustibleAr = poblacionArabia*tasaCompraAr;
        plasticoQuemadoAr = poblacionArabia*tasaQuemaAr;
        emisionCo2Arabia = emisionCo2Arabia + (co2Persona*poblacionArabia)+(co2Vehiculo*vehiculoCombustibleAr)+(plasticoQuemadoAr*co2PlasticoQuemado)+(co2Industria*industriaAr)-co2ArAnt;
        co2ArAnt = emisionCo2Arabia*tasaCo2ArAnt;

        //Brasil
        poblacionBrasil = poblacionBrasil + nacimientosBr - muertesBr;
        muertesBr = (tasaDeMortalidadBr*poblacionBrasil*efectoSobreMuertes)/1000;
        nacimientosBr = (tasaDeNatalidadBr*poblacionBrasil)/1000;
        industriaBr = industriaPersonasBr * poblacionBrasil;
        vehiculoCombustibleBr = poblacionBrasil*tasaCompraBr;
        plasticoQuemadoBr = poblacionBrasil*tasaQuemaBr;
        emisionCo2Brasil = emisionCo2Brasil + (co2Persona*poblacionBrasil)+(co2Vehiculo*vehiculoCombustibleBr)+(plasticoQuemadoBr*co2PlasticoQuemado)+(co2Industria*industriaBr)-co2BrAnt;
        co2BrAnt = emisionCo2Brasil*tasaCo2BrAnt;

        //Resto del Mundo
        emisionCo2Restante = emisionCo2Restante + co2RestoMundo - co2ResAnt;
        EfectoAumentoCo2Res();
        co2RestoMundo = co2InicialRes*efectoAumentoCo2Res;
        co2ResAnt = emisionCo2Restante*tasaCo2ResAnt;



        //Plantacion y deforestacion, se sacan al final luego de tener todas las poblaciones.
        plantacion = (tasaPlantacionCh*poblacionChina)+(tasaPlantacionUS*poblacionUSA)+(tasaPlantacionAl*poblacionAlemania)+(tasaPlantacionAr*poblacionArabia)+(tasaPlantacionBr*poblacionBrasil);
        deforestacion = ((poblacionChina * tasaTalaCh)+(poblacionUSA*tasaTalaUS)+(poblacionAlemania*tasaTalaAl)+(poblacionArabia*tasaTalaAr)+(poblacionBrasil*tasaTalaBr)) * tasaIncendios;

    }

    public void DropdownCambiarPais(int index)
    {
        ActualizarValores(index,true);
    }
    private void ActualizarValores(int index, bool cambiopais)//Actualiza los TEXT de las estadisticas en la escena.
    {

        
        indexPais = index;
        double cantidad = 1;
        string unidad = "";

        switch(index)
        {
            case 0:
                txtCo2Atmosfera.gameObject.SetActive(true);
                txtPoblacion.gameObject.SetActive(false);
                txtCo2Ant.gameObject.SetActive(true);
                txtArboles.gameObject.SetActive(true);

                cantidad = DeterminarUnidad(co2Atmosfera);
                unidad = DeterminarUnidadTexto(co2Atmosfera);
                txtCo2Atmosfera.text = "CO2 en la atmosfera (PPM): " + (float)(co2Atmosfera/cantidad) + unidad;

                cantidad = DeterminarUnidad(emisionCo2Ant);
                unidad = DeterminarUnidadTexto(cantidad);
                txtCo2Ant.text = "Emisiones de CO2 (Toneladas): " + (float)(emisionCo2Ant/cantidad) + unidad;

                cantidad = DeterminarUnidad(arboles);
                unidad = DeterminarUnidadTexto(cantidad);
                txtArboles.text = "Arboles: " + (float)(arboles/cantidad) + unidad;

                if(cambiopais){CambiarGraficaCo2AtmAction(); img_Bandera.sprite = sp_BanderaONU;}
                break;
            
            case 1:

                txtCo2Atmosfera.gameObject.SetActive(false);
                txtPoblacion.gameObject.SetActive(true);
                txtCo2Ant.gameObject.SetActive(true);
                txtArboles.gameObject.SetActive(false);

                cantidad = DeterminarUnidad(poblacionChina);
                unidad = DeterminarUnidadTexto(cantidad);
                txtPoblacion.text = "Poblacion: " + (float)(poblacionChina/cantidad) + unidad;

                cantidad = DeterminarUnidad(emisionCo2China);
                unidad = DeterminarUnidadTexto(cantidad);
                txtCo2Ant.text = "Emisiones de CO2 (Toneladas): " + (float)(emisionCo2China/cantidad) + unidad;

               if(cambiopais){CambiarGraficaPoblacionAction();img_Bandera.sprite = sp_BanderaChina;}
                break;
            
            case 2:

                txtCo2Atmosfera.gameObject.SetActive(false);
                txtPoblacion.gameObject.SetActive(true);
                txtCo2Ant.gameObject.SetActive(true);
                txtArboles.gameObject.SetActive(false);

                cantidad = DeterminarUnidad(poblacionUSA);
                unidad = DeterminarUnidadTexto(cantidad);
                txtPoblacion.text = "Poblacion: " + (float)(poblacionUSA/cantidad) + unidad;

                cantidad = DeterminarUnidad(emisionCo2USA);
                unidad = DeterminarUnidadTexto(cantidad);
                txtCo2Ant.text = "Emisiones de CO2 (Toneladas): " + (float)(emisionCo2USA/cantidad) + unidad;

                if(cambiopais){CambiarGraficaPoblacionAction();img_Bandera.sprite = sp_BanderaUSA;}
                break;
            
            case 3:

                txtCo2Atmosfera.gameObject.SetActive(false);
                txtPoblacion.gameObject.SetActive(true);
                txtCo2Ant.gameObject.SetActive(true);
                txtArboles.gameObject.SetActive(false);

                cantidad = DeterminarUnidad(poblacionAlemania);
                unidad = DeterminarUnidadTexto(cantidad);
                txtPoblacion.text = "Poblacion: " + (float)(poblacionAlemania/cantidad) + unidad;

                cantidad = DeterminarUnidad(emisionCo2Alemania);
                unidad = DeterminarUnidadTexto(cantidad);
                txtCo2Ant.text = "Emisiones de CO2 (Toneladas): " + (float)(emisionCo2Alemania/cantidad) + unidad;

                if(cambiopais){CambiarGraficaPoblacionAction();img_Bandera.sprite = sp_BanderaAlemania;}
                break;
            
            case 4:

                txtCo2Atmosfera.gameObject.SetActive(false);
                txtPoblacion.gameObject.SetActive(true);
                txtCo2Ant.gameObject.SetActive(true);
                txtArboles.gameObject.SetActive(false);

                cantidad = DeterminarUnidad(poblacionArabia);
                unidad = DeterminarUnidadTexto(cantidad);
                txtPoblacion.text = "Poblacion: " + (float)(poblacionArabia/cantidad) + unidad;

                cantidad = DeterminarUnidad(emisionCo2Arabia);
                unidad = DeterminarUnidadTexto(cantidad);
                txtCo2Ant.text = "Emisiones de CO2 (Toneladas): " + (float)(emisionCo2Arabia/cantidad) + unidad;

                if(cambiopais){CambiarGraficaPoblacionAction();img_Bandera.sprite = sp_BanderaArabia;}
                break;
            
            case 5:

                txtCo2Atmosfera.gameObject.SetActive(false);
                txtPoblacion.gameObject.SetActive(true);
                txtCo2Ant.gameObject.SetActive(true);
                txtArboles.gameObject.SetActive(false);

                cantidad = DeterminarUnidad(poblacionBrasil);
                unidad = DeterminarUnidadTexto(cantidad);
                txtPoblacion.text = "Poblacion: " + (float)(poblacionBrasil/cantidad) + unidad;

                cantidad = DeterminarUnidad(emisionCo2Brasil);
                unidad = DeterminarUnidadTexto(cantidad);
                txtCo2Ant.text = "Emisiones de CO2 (Toneladas): " + (float)(emisionCo2Brasil/cantidad) + unidad;

                if(cambiopais){CambiarGraficaPoblacionAction();img_Bandera.sprite = sp_BanderaBrasil;}
                break;

        }

        

        
        
    }

    private double DeterminarUnidad(double valor) /* Determina la unidad (millon, billon o trillon) de un numero pasado por parametro, y devuelve el valor de dicha unidad*/
    {

        //Determina la unidad (millon, billón o trillón) en DOUBLE de un valor pasado por parametro.
        double cantidad = 1;

        if(valor >= millon && valor <billon )
        {
            cantidad = millon;
        }
        else if(valor >= billon && valor<trillon)
        {
            cantidad = billon;
        }
        else if(valor >= trillon)
        {
            cantidad = trillon;
        }

        return cantidad;
    }

    private string DeterminarUnidadTexto(double cantidad)/*Determina la unidad (millon, billon o trillon) de un numero pasado por parametro, y devuelve el TEXTO del nombre de dicha unidad*/
    {

        //Determina la unidad (millón, billón o trillón) en STRING de un valor pasado por parametro. 
        string unidad = "";

        if(cantidad == millon)
        {
            unidad = " Mill";
        }
        else if (cantidad == billon)
        {
            unidad = " Bill";
        }
        else if (cantidad == trillon)
        {
            unidad = " Trill";
        }
            
        return unidad;
    }
    


    private void EfectoMuertes() //Metodo que representa el comportamiento del Lookup efecto sobre las muertes del modelo.
    {
        double oper = co2Atmosfera / capacidadCargaMuertes;
        
         efectoSobreMuertes = 0.9 + (oper*0.2);
        

    }


  
    private void TasaIncendios() //Metodo que representa el comportamiento del Lookup tasa de incendios forestales del modelo.  
    {
        double oper = co2Atmosfera / capacidadCargaIncendios;
       
        tasaIncendios = 1.05 + (oper*0.1);
        

    }

    private void EfectoAumentoCo2Res()
    {
        double oper = (year-2020)/10;
        efectoAumentoCo2Res = 1 + (oper*0.1);
    }

    public void ResetearDatosAction() //Cancela toda simulacion existente y retorna las variables y las graficas a sus valores del año actual.
    {

        simulacion = false;
        variableGrafica = "co2Atmosfera";
        RecuperarDatosActuales();
        ActualizarGrafica();
        img_Bandera.sprite = sp_BanderaONU;
        dd_Paises.SetValueWithoutNotify(0);
        ActualizarValores(0,false);

    }

    public void CambiarGraficaCo2AtmAction() //Muestra en la grafica los valores de la variable "Co2Atmosfera"
    {
        variableGrafica = "co2Atmosfera";
        conocerSimulacion();
    }

    public void CambiarGraficaCo2AntAction() //Muestra en la grafica los valores de la variable "emisionCo2Ant"
    {
        switch(indexPais)
        {
            case 0:
                variableGrafica = "emisionCo2Ant";
                break;
            case 1:
                variableGrafica = "emisionCo2China";
                break;
            case 2:
                variableGrafica = "emisionCo2USA";
                break;
            case 3:
                variableGrafica = "emisionCo2Alemania";
                break;
            case 4:
                variableGrafica = "emisionCo2Arabia";
                break;
            case 5:
                variableGrafica = "emisionCo2Brasil";
                break;
        }
        
        conocerSimulacion();
    }

    public void CambiarGraficaPoblacionAction() //Muestra en la grafica los valores de la variable "poblacion"
    {
        switch(indexPais)
        {
            case 1:
                variableGrafica = "poblacionChina";
                break;
            case 2:
                variableGrafica = "poblacionUSA";
                break;
            case 3:
                variableGrafica = "poblacionAlemania";
                break;
            case 4:
                variableGrafica = "poblacionArabia";
                break;
            case 5:
                variableGrafica = "poblacionBrasil";
                break;
        }

        conocerSimulacion();
    }

    public void CambiarGraficaArbolesAction() //Muestra en la grafica los valores de la variable "arboles"
    {
        variableGrafica = "arboles";
        conocerSimulacion();
    }


    private void conocerSimulacion() /*Metodo para saber si la sección de estadisticas está en modo simulacion o no, para saber qué datos se deben graficar al cambiar de variable*/
    { 
        if(simulacion)
        {
            
            inpYearSimulacion.text = ultimoAñoSimulacion; 
            VerResultadosAction();
            

            
        }
        else{
            ActualizarGrafica();
        }
    }

    private double DeterminarVariableModelo() /*Determina que variable del modelo se ha seleccionado actualmente, para saber cual de ellas se debe graficar*/
    {
        double variableModelo = co2Atmosfera;

        switch(variableGrafica)
        {
            case "co2Atmosfera":
                variableModelo = co2Atmosfera;
                break;
            case "emisionCo2Ant":
                variableModelo = emisionCo2Ant;
                break;
            case "arboles":
                variableModelo = arboles;
                break;
            case "emisionCo2China":
                variableModelo = emisionCo2China;
                break;
            case "poblacionChina":
                variableModelo = poblacionChina;
                break;
            case "emisionCo2USA":
                variableModelo = emisionCo2USA;
                break;
            case "poblacionUSA":
                variableModelo = poblacionUSA;
                break;
            case "emisionCo2Alemania":
                variableModelo = emisionCo2Alemania;
                break;
            case "poblacionAlemania":
                variableModelo = poblacionAlemania;
                break;
            case "emisionCo2Arabia":
                variableModelo = emisionCo2Arabia;
                break;
            case "poblacionArabia":
                variableModelo = poblacionArabia;
                break;
            case "emisionCo2Brasil":
                variableModelo = emisionCo2Brasil;
                break;
            case "poblacionBrasil":
                variableModelo = poblacionBrasil;
                break;
                
        }

        return variableModelo;
    }

    public double getArbolesINI()
    {
        return arbolesINI;
    }
    public double getPoblacionChinaINI()
    {
        return poblacionChinaINI;
    }

    public double getEmisionCo2ChinaINI()
    {
        return emisionCo2ChinaINI;
    }

    public double getPoblacionUSAINI()
    {
        return poblacionUSAINI;
    }

    public double getEmisionCo2USAINI()
    {
        return emisionCo2USAINI;
    }

    public double getPoblacionAlemaniaINI()
    {
        return poblacionAlemaniaINI;
    }

    public double getEmisionCo2AlemaniaINI()
    {
        return emisionCo2AlemaniaINI;
    }

    public double getPoblacionArabiaINI()
    {
        return poblacionArabiaINI;
    }

    public double getEmisionCo2ArabiaINI()
    {
        return emisionCo2ArabiaINI;
    }

    public double getPoblacionBrasilINI()
    {
        return poblacionBrasilINI;
    }

    public double getEmisionCo2BrasilINI()
    {
        return emisionCo2BrasilINI;
    }



    public double getArbolesACT()
    {
        return arbolesACT;
    }

    public double getPoblacionChinaACT()
    {
        return poblacionChACT;
    }

    public double getEmisionCo2ChinaACT()
    {
        return emisionCo2ChACT;
    }

    public double getPoblacionUSAACT()
    {
        return poblacionUSACT;
    }

    public double getEmisionCo2USAACT()
    {
        return emisionCo2USACT;
    }

    public double getPoblacionAlemaniaACT()
    {
        return poblacionAlACT;
    }

    public double getEmisionCo2AlemaniaACT()
    {
        return emisionCo2AlACT;
    }

    public double getPoblacionArabiaACT()
    {
        return poblacionArACT;
    }

    public double getEmisionCo2ArabiaACT()
    {
        return emisionCo2ArACT;
    }

    public double getPoblacionBrasilACT()
    {
        return poblacionBrACT;
    }

    public double getEmisionCo2BrasilACT()
    {
        return emisionCo2BrACT;
    }

    public double getEmisionCo2Ant()
    {
        return emisionCo2AntACT;
    }

    public double getVehiculoCombustibleChinaACT()
    {
        return vehiculoCombustibleChACT;
    }

    public double getIndustriaChinaACT()
    {
        return industriaChACT;
    }

    public double getPlasticoQuemadoChinaACT()
    {
        return plasticoQuemadoChACT;
    }

    public double getVehiculoCombustibleUSAACT()
    {
        return vehiculoCombustibleUSACT;
    }

    public double getIndustriaUSAACT()
    {
        return industriaUSACT;
    }

    public double getPlasticoQuemadoUSAACT()
    {
        return plasticoQuemadoUSACT;
    }

    public double getVehiculoCombustibleAlemaniaACT()
    {
        return vehiculoCombustibleAlACT;
    }

    public double getIndustriaAlemaniaACT()
    {
        return industriaAlACT;
    }

    public double getPlasticoQuemadoAlemaniaACT()
    {
        return plasticoQuemadoAlACT;
    }

    public double getVehiculoCombustibleArabiaACT()
    {
        return vehiculoCombustibleArACT;
    }

    public double getIndustriaArabiaACT()
    {
        return industriaArACT;
    }

    public double getPlasticoQuemadoArabiaACT()
    {
        return plasticoQuemadoArACT;
    }

    public double getVehiculoCombustibleBrasilACT()
    {
        return vehiculoCombustibleBrACT;
    }

    public double getIndustriaBrasilACT()
    {
        return industriaBrACT;
    }

    public double getPlasticoQuemadoBrasilACT()
    {
        return plasticoQuemadoBrACT;
    }


   

    public void alterarTasaCompraCh(double valor)
    {
        tasaCompraCh+= valor;
    }

    public void alterarTasaDeNatalidadCh(double valor)
    {
        tasaDeNatalidadCh+= valor;
    }

    public void alterarTasaDeMortalidadCh(double valor)
    {
        tasaDeMortalidadCh+= valor; 
    }

    public void alterarTasaIndustrialCh(double valor)
    {
        tasaIndustrialCh+= valor; 
    }

    public void alterarTrabajadorIndustriaCh(double valor)
    {
        trabajadorIndustriaCh+= valor; 
    }

    public void alterarTasaQuemaCh(double valor)
    {
        tasaQuemaCh+= valor; 
    }

    public void alterarTasaPlantacionCh(double valor)
    {
        tasaPlantacionCh+= valor;
    }

    public void alterarTasaTalaCh(double valor)
    {
        tasaTalaCh+= valor;
    }

    public void alterarTasaCompraUS(double valor)
    {
        tasaCompraUS+= valor;
    }

    public void alterarTasaDeNatalidadUS(double valor)
    {
        tasaDeNatalidadUS+= valor;
    }

    public void alterarTasaDeMortalidadUS(double valor)
    {
        tasaDeMortalidadUS+= valor; 
    }

    public void alterarTasaIndustrialUS(double valor)
    {
        tasaIndustrialUS+= valor; 
    }

    public void alterarTrabajadorIndustriaUS(double valor)
    {
        trabajadorIndustriaUS+= valor; 
    }

    public void alterarTasaQuemaUS(double valor)
    {
        tasaQuemaUS+= valor; 
    }

    public void alterarTasaPlantacionUS(double valor)
    {
        tasaPlantacionUS+= valor;
    }

    public void alterarTasaTalaUS(double valor)
    {
        tasaTalaUS+= valor;
    }

    public void alterarTasaCompraAl(double valor)
    {
        tasaCompraAl+= valor;
    }

    public void alterarTasaDeNatalidadAl(double valor)
    {
        tasaDeNatalidadAl+= valor;
    }

    public void alterarTasaDeMortalidadAl(double valor)
    {
        tasaDeMortalidadAl+= valor; 
    }

    public void alterarTasaIndustrialAl(double valor)
    {
        tasaIndustrialAl+= valor; 
    }

    public void alterarTrabajadorIndustriaAl(double valor)
    {
        trabajadorIndustriaAl+= valor; 
    }

    public void alterarTasaQuemaAl(double valor)
    {
        tasaQuemaAl+= valor; 
    }

    public void alterarTasaPlantacionAl(double valor)
    {
        tasaPlantacionAl+= valor;
    }

    public void alterarTasaTalaAl(double valor)
    {
        tasaTalaAl+= valor;
    }

    public void alterarTasaCompraAr(double valor)
    {
        tasaCompraAr+= valor;
    }

    public void alterarTasaDeNatalidadAr(double valor)
    {
        tasaDeNatalidadAr+= valor;
    }

    public void alterarTasaDeMortalidadAr(double valor)
    {
        tasaDeMortalidadAr+= valor; 
    }

    public void alterarTasaIndustrialAr(double valor)
    {
        tasaIndustrialAr+= valor; 
    }

    public void alterarTrabajadorIndustriaAr(double valor)
    {
        trabajadorIndustriaAr+= valor; 
    }

    public void alterarTasaQuemaAr(double valor)
    {
        tasaQuemaAr+= valor; 
    }

    public void alterarTasaPlantacionAr(double valor)
    {
        tasaPlantacionAr+= valor;
    }

    public void alterarTasaTalaAr(double valor)
    {
        tasaTalaAr+= valor;
    }
    
    public void alterarTasaCompraBr(double valor)
    {
        tasaCompraBr+= valor;
    }

    public void alterarTasaDeNatalidadBr(double valor)
    {
        tasaDeNatalidadBr+= valor;
    }

    public void alterarTasaDeMortalidadBr(double valor)
    {
        tasaDeMortalidadBr+= valor; 
    }

    public void alterarTasaIndustrialBr(double valor)
    {
        tasaIndustrialBr+= valor; 
    }

    public void alterarTrabajadorIndustriaBr(double valor)
    {
        trabajadorIndustriaBr+= valor; 
    }

    public void alterarTasaQuemaBr(double valor)
    {
        tasaQuemaBr+= valor; 
    }

    public void alterarTasaPlantacionBr(double valor)
    {
        tasaPlantacionBr+= valor;
    }

    public void alterarTasaTalaBr(double valor)
    {
        tasaTalaBr+= valor;
    }



}


