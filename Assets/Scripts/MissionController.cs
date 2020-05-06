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

            if(misiones[i].getIndexPais() == indexPais /*&& nivelActualVariable == misiones[i].getNivel()*/)
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

        //China
        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a ciertos sectores de la población." ,
        "tasaDeNatalidadCh",
        "Solo los sectores poblacionales con buen equilibrio ecónomico y parejas cuya edad sea superior a 30 años pueden tener hijos. ",
        3,60,50,0,5,80,-3));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a toda la población hasta nuevo aviso." ,
        "tasaDeNatalidadCh",
        "¡ESTAMOS EN RIESGO! El marco legal establece que todos los habitantes del país están obligados a no tener hijos hasta nuevo aviso. ",
        4,46,80,0,6,120,-5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la sobrepoblación" ,
        "tasaDeNatalidadCh",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la sobrepoblación.",
        1,92,12,0,1,22,-0.5));


        misiones.Add(nuevaMision= new Mision("Campaña contra la tala de árboles." ,
        "tasaTalaCh",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la tala de árboles.",
        1,86,10,0,1,21,-0.1));


        misiones.Add(nuevaMision= new Mision("Impuesto sobre la tala de árboles para empresas autorizadas." ,
        "tasaTalaCh",
        "Asigna un costo fijo a las empresas que estén autorizadas para realizar algún tipo de tala.",
        2,86,10,0,2,21,-0.3));

        misiones.Add(nuevaMision= new Mision("Prohibe la tala de árboles en todo el país." ,
        "tasaTalaCh",
        "Legisla una nueva ley que prohiba la tala de árboles, y que esta sea permitida solo en circunstancias sumamente justificables.",
        3,70,30,0,4,56,-0.5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la quema de residuos plásticos." ,
        "tasaQuemaCh",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la quema de residuos plásticos.",
        1,5,89,0,1,18,-0.1));

        misiones.Add(nuevaMision= new Mision("Imponer multas sobre la quema de residuos plásticos." ,
        "tasaQuemaCh",
        "Impone una nueva norma con un sistema de multas que obligue a los cuidadanos a no quemar residuos plásticos y reutilizarlos.",
        1,15,80,0,2,32,-0.25));

        misiones.Add(nuevaMision= new Mision("Más impuestos para las empresas que fabriquen productos a base de plástico." ,
        "tasaQuemaCh",
        "Establece un impuesto sobre empresas que utilicen el plastico en sus operaciones, o que sean productoras de él.",
        2,22,76,0,2,49,-0.45));

        misiones.Add(nuevaMision= new Mision("Prohibe la fabricación y consumo de plástico en el país." ,
        "tasaQuemaCh",
        "Habrá procesos penales y multas de alto costo para personas naturales y juridicas que fabriquen y consuman productos a base de plástico.",
        3,30,70,0,4,49,-0.6));
        

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en el transporte." ,
        "tasaCompraCh",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en el transporte. ",
        2,65,36,0,4,58,-1));

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en la industria en general." ,
        "tasaIndustrialCh",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en todas las empresas. ",
        2,65,40,0,6,65,-0.08));

        misiones.Add(nuevaMision= new Mision("Prohibe la instalación de nuevos sistemas de calefacción." ,
        "tasaIndustrialCh",
        "Legisla una nueva ley que prohibe la instalación y fabricación de nuevos sistemas de calefacción a base de gásoleo. ",
        1,80,18,0,2,30,-0.03));

        misiones.Add(nuevaMision= new Mision("Favorece la compra de coches électricos",
        "tasaCompraCh",
        "Exenta del impuesto de matriculación y aumenta las primas de compra de automóviles ecológicos.",
        1,90,20,0,2,35,-0.08));

        misiones.Add(nuevaMision= new Mision("Impulsa la sivicultura",
        "tasaPlantacionCh",
        "Promueve campañas publicitarias y eventos en los que se traten actividades relacionadas con el cultivo, el cuidado y la explotación de los bosques y montes.",
        1,95,20,0,1,35,0.3));

        misiones.Add(nuevaMision= new Mision("Impuesto sobre los billetes de avión",
        "tasaCompraCh",
        "Fija un impuesto a nivel nacional en cada compra realizada por billete de avión.",
        2,70,28,0,1,40,-0.08));

         misiones.Add(nuevaMision= new Mision("Inversión en empresas que opten por el uso de energías renovables",
        "tasaCompraCh",
        "Reduce impuestos y abre oportunidades para las empresas que decidan operar con energías renovables (éolica, solar, biomasa)",
        2,68,60,0,5,100,-1));

        misiones.Add(nuevaMision= new Mision("Inversión en mejoras de la eficiencia energética.",
        "tasaIndustrialCh",
        "Promueve los estudios y proyectos científicos basados en las mejoras de la eficiencia energética.",
        -1,90,10,0,1,20,-0.02));

        misiones.Add(nuevaMision= new Mision("Campañas de reforestación.",
        "tasaPlantacionCh",
        "Impulsa la reforestación (plantación de árboles) mediante publicidad y actividades organizadas por diferentes sectores y comunidades.",
        1,96,8,0,1,18,0.15));

        misiones.Add(nuevaMision= new Mision("Impulsa las tecnologías de eliminación de CO2",
        "tasaIndustrialCh",
        "Invierte en tecnologías que busquen erradicar las emisiones de CO2, reduciendo o anulando sus impuestos.",
        2,76,26,0,3,34,-0.06));

        misiones.Add(nuevaMision= new Mision("Campañas para reducir el consumo de energía en los hogares.",
        "tasaIndustrialCh",
        "Promueve campañas publicitarias para que la población modere su consumo de energía lo mejor posible.",
        1,96,12,0,1,20,-0.01));

        misiones.Add(nuevaMision= new Mision("Aplicar impuestos a la energía nuclear.",
        "tasaIndustrialCh",
        "Fija un precio adicional a la cantidad de energía nuclear emitida por las industrias.",
        1,75,22,0,2,35,-0.03));

       misiones.Add(nuevaMision= new Mision("Prohibir la implementación de nuevas centrales électricas.",
        "tasaIndustrialCh",
        "Prohíbe la implementación de nuevas centrales électricas que funcionen con carbón en las regiones más contaminadas del país.",
        2,60,30,0,4,50,-0.05));

        misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las centrales électricas del país.",
        "tasaIndustrialCh",
        "Clausura la mitad de las centrales électricas del país elegidas estrátegicamente.",
        3,57,45,0,5,66,-0.07));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las centrales électricas del país.",
        "tasaIndustrialCh",
        "¡ESTAMOS DESESPERADOS! Clausura casi todas las centrales electricas del país con tal de evitar la catástrofe ambiental.",
        4,57,45,0,5,48,-0.07));

        misiones.Add(nuevaMision= new Mision("Restringir cantidad de autos en las calles.",
        "tasaCompraCh",
        "Restringe la cantidad de autos en la calle (Mediante su matricula) para ciudades grandes como Pekín, Shanghái y Cantón.",
        1,92,18,0,1,25,-0.08));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares al mes.",
        "tasaCompraCh",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día al mes.",
        -1,95,12,0,1,19,-0.03));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares a la semana.",
        "tasaCompraCh",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día a la semana.",
        2,79,25,0,1,35,-0.06));

        misiones.Add(nuevaMision= new Mision("Tres días sin vehículos particulares a la semana.",
        "tasaCompraCh",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares tres días a la semana.",
        4,70,32,0,2,95,-0.12));

        misiones.Add(nuevaMision= new Mision("Clausurar el 30% de las minas de carbón del país." ,
        "tasaIndustrialCh",
        "Cierre obligatorio del 30% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        1,80,23,0,3,30,-0.03));

        misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las minas de carbón del país." ,
        "tasaIndustrialCh",
        "Cierre obligatorio de la mitad de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        2,75,28,0,4,42,-0.05));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las minas de carbón del país." ,
        "tasaIndustrialCh",
        "Cierre obligatorio del 80% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        3,70,30,0,5,50,-0.08));

        misiones.Add(nuevaMision= new Mision("Clausurar TODAS de las minas de carbón del país." ,
        "tasaIndustrialCh",
        "Cierre obligatorio de todas las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,65,40,0,6,60,-1));

        //USA
        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener más de dos hijos." ,
        "tasaDeNatalidadUS",
        "Las personas que tengan más de dos hijos sin razón justificada dentro del marco legal obtendrán consecuencias penales.",
        2,65,40,1,3,67,-1));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener más de un hijo." ,
        "tasaDeNatalidadUS",
        "Las personas que tengan más de un hijo sin razón justificada dentro del marco legal obtendrán consecuencias penales.",
        3,62,45,1,4,70,-2));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a ciertos sectores de la población." ,
        "tasaDeNatalidadUS",
        "Solo los sectores poblacionales con buen equilibrio ecónomico y parejas cuya edad sea superior a 30 años pueden tener hijos. ",
        4,60,50,1,5,80,-3));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a toda la población hasta nuevo aviso." ,
        "tasaDeNatalidadUS",
        "¡ESTAMOS EN RIESGO! El marco legal establece que todos los habitantes del país están obligados a no tener hijos hasta nuevo aviso. ",
        4,46,80,1,6,120,-5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la sobrepoblación" ,
        "tasaDeNatalidadUS",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la sobrepoblación.",
        1,92,12,1,1,22,-0.5));

        misiones.Add(nuevaMision= new Mision("Campaña contra la tala de árboles." ,
        "tasaTalaUS",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la tala de árboles.",
        1,86,10,1,1,21,-0.1));

        misiones.Add(nuevaMision= new Mision("Impuesto sobre la tala de árboles para empresas autorizadas." ,
        "tasaTalaUS",
        "Asigna un costo fijo a las empresas que estén autorizadas para realizar algún tipo de tala.",
        2,86,10,1,2,21,-0.3));

        misiones.Add(nuevaMision= new Mision("Prohibe la tala de árboles en todo el país." ,
        "tasaTalaUS",
        "Legisla una nueva ley que prohiba la tala de árboles, y que esta sea permitida solo en circunstancias sumamente justificables.",
        3,70,30,1,4,56,-0.5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la quema de residuos plásticos." ,
        "tasaQuemaUS",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la quema de residuos plásticos.",
        1,5,89,1,1,18,-0.1));

        misiones.Add(nuevaMision= new Mision("Imponer multas sobre la quema de residuos plásticos." ,
        "tasaQuemaUS",
        "Impone una nueva norma con un sistema de multas que obligue a los cuidadanos a no quemar residuos plásticos y reutilizarlos.",
        1,15,80,1,2,32,-0.25));

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en el transporte." ,
        "tasaCompraUS",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en el transporte. ",
        2,65,36,1,4,58,-1));

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en la industria en general." ,
        "tasaIndustrialUS",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en todas las empresas. ",
        2,65,40,1,6,65,-0.08));

        misiones.Add(nuevaMision= new Mision("Prohibe la instalación de nuevos sistemas de calefacción." ,
        "tasaIndustrialUS",
        "Legisla una nueva ley que prohibe la instalación y fabricación de nuevos sistemas de calefacción a base de gásoleo. ",
        1,80,18,1,2,30,-0.03));

        misiones.Add(nuevaMision= new Mision("Favorece la compra de coches électricos",
        "tasaCompraUS",
        "Exenta del impuesto de matriculación y aumenta las primas de compra de automóviles ecológicos.",
        1,90,20,1,2,35,-0.08));

        misiones.Add(nuevaMision= new Mision("Impulsa la sivicultura",
        "tasaPlantacionUS",
        "Promueve campañas publicitarias y eventos en los que se traten actividades relacionadas con el cultivo, el cuidado y la explotación de los bosques y montes.",
        1,95,20,1,1,35,0.3));

        misiones.Add(nuevaMision= new Mision("Impuesto sobre los billetes de avión",
        "tasaCompraUS",
        "Fija un impuesto a nivel nacional en cada compra realizada por billete de avión.",
        2,70,28,1,1,40,-0.08));

         misiones.Add(nuevaMision= new Mision("Inversión en empresas que opten por el uso de energías renovables",
        "tasaCompraUS",
        "Reduce impuestos y abre oportunidades para las empresas que decidan operar con energías renovables (éolica, solar, biomasa)",
        2,68,60,1,5,100,-1));

        misiones.Add(nuevaMision= new Mision("Inversión en mejoras de la eficiencia energética.",
        "tasaIndustrialUS",
        "Promueve los estudios y proyectos científicos basados en las mejoras de la eficiencia energética.",
        -1,90,10,1,1,20,-0.02));

        misiones.Add(nuevaMision= new Mision("Campañas de reforestación.",
        "tasaPlantacionUS",
        "Impulsa la reforestación (plantación de árboles) mediante publicidad y actividades organizadas por diferentes sectores y comunidades.",
        1,96,8,1,1,18,0.15));

        misiones.Add(nuevaMision= new Mision("Impulsa las tecnologías de eliminación de CO2",
        "tasaIndustrialUS",
        "Invierte en tecnologías que busquen erradicar las emisiones de CO2, reduciendo o anulando sus impuestos.",
        2,76,26,1,3,34,-0.06));

        misiones.Add(nuevaMision= new Mision("Campañas para reducir el consumo de energía en los hogares.",
        "tasaIndustrialUS",
        "Promueve campañas publicitarias para que la población modere su consumo de energía lo mejor posible.",
        1,96,12,1,1,20,-0.01));

        misiones.Add(nuevaMision= new Mision("Aplicar impuestos a la energía nuclear.",
        "tasaIndustrialUS",
        "Fija un precio adicional a la cantidad de energía nuclear emitida por las industrias.",
        1,75,22,1,2,35,0.03));


        misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las centrales électricas del país.",
        "tasaIndustrialUS",
        "Clausura la mitad de las centrales électricas del país elegidas estrátegicamente.",
        4,50,70,1,4,100,-0.07));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las centrales électricas del país.",
        "tasaIndustrialUS",
        "¡ESTAMOS DESESPERADOS! Clausura casi todas las centrales electricas del país con tal de evitar la catástrofe ambiental.",
        4,48,80,1,5,120,-0.07));

        misiones.Add(nuevaMision= new Mision("Restringir cantidad de autos en las calles.",
        "tasaCompraUS",
        "Restringe la cantidad de autos en la calle (Mediante su matricula) para las ciudades grandes del país.",
        1,87,18,1,1,25,-0.08));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares al mes.",
        "tasaCompraUS",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día al mes.",
        -1,90,12,1,1,19,-0.03));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares a la semana.",
        "tasaCompraUS",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día a la semana.",
        2,73,25,1,1,35,-0.06));

        misiones.Add(nuevaMision= new Mision("Tres días sin vehículos particulares a la semana.",
        "tasaCompraUS",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares tres días a la semana.",
        4,65,40,1,2,85,-0.12));

        misiones.Add(nuevaMision= new Mision("Clausurar el 30% de las minas de carbón del país." ,
        "tasaIndustrialUS",
        "Cierre obligatorio del 30% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        3,70,29,1,3,40,-0.03));

        misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las minas de carbón del país." ,
        "tasaIndustrialUS",
        "Cierre obligatorio de la mitad de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,65,35,1,4,56,-0.05));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las minas de carbón del país." ,
        "tasaIndustrialUS",
        "Cierre obligatorio del 80% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,60,50,1,5,70,-0.08));

        misiones.Add(nuevaMision= new Mision("Clausurar TODAS de las minas de carbón del país." ,
        "tasaIndustrialUS",
        "Cierre obligatorio de todas las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,65,70,1,6,98,-1));

        misiones.Add(nuevaMision= new Mision("Prohibir la implementación de nuevas centrales électricas.",
        "tasaIndustrialUS",
        "Prohíbe la implementación de nuevas centrales électricas que funcionen con carbón en las regiones más contaminadas del país.",
        4,50,30,1,5,50,-0.05));

        misiones.Add(nuevaMision= new Mision("Suspender normativa que pretende reducir las fugas de las emisiones de los operadores de gas y petroleo." ,
        "tasaIndustrialUS",
        "Las empresas que operan mediante la géneración de emisiones basadas en gas y petroleo te ofrecen una considerable suma de dinero para suspender una normativa que les afecta.",
        -1,85,10,1,1,50,0.02));


        misiones.Add(nuevaMision= new Mision("Asigna una nueva norma que pretende reducir las fugas de las emisiones de los operadores de gas y petroleo." ,
        "tasaIndustrialUS",
        "Las empresas que operan mediante la géneración de emisiones basadas en gas y petroleo deberán pagar multas por las fugas en sus operaciones.",
        1,82,20,1,2,28,-0.02));

        misiones.Add(nuevaMision= new Mision("Frena la norma de vertido de métales tóxicos en el país." ,
        "tasaIndustrialUS",
        "Frena una norma que controla (permite) el vertido de metales tóxicos, como ársenico y mercurio, de las centrales électricas en las vías navegables públicas.",
        2,70,28,1,2,38,-0.03));

        //Alemania
        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener más de dos hijos." ,
        "tasaDeNatalidadAl",
        "Las personas que tengan más de dos hijos sin razón justificada dentro del marco legal obtendrán consecuencias penales.",
        2,65,40,2,3,67,-1));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener más de un hijo." ,
        "tasaDeNatalidadAl",
        "Las personas que tengan más de un hijo sin razón justificada dentro del marco legal obtendrán consecuencias penales.",
        3,62,45,2,4,70,-2));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a ciertos sectores de la población." ,
        "tasaDeNatalidadAl",
        "Solo los sectores poblacionales con buen equilibrio ecónomico y parejas cuya edad sea superior a 30 años pueden tener hijos. ",
        4,60,50,2,5,80,-3));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a toda la población hasta nuevo aviso." ,
        "tasaDeNatalidadAl",
        "¡ESTAMOS EN RIESGO! El marco legal establece que todos los habitantes del país están obligados a no tener hijos hasta nuevo aviso. ",
        4,46,80,2,6,120,-5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la sobrepoblación" ,
        "tasaDeNatalidadAl",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la sobrepoblación.",
        1,92,12,2,1,22,-0.5));

        misiones.Add(nuevaMision= new Mision("Campaña contra la tala de árboles." ,
        "tasaTalaAl",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la tala de árboles.",
        1,86,10,2,1,21,-0.1));

        misiones.Add(nuevaMision= new Mision("Impuesto sobre la tala de árboles para empresas autorizadas." ,
        "tasaTalaAl",
        "Asigna un costo fijo a las empresas que estén autorizadas para realizar algún tipo de tala.",
        2,86,10,2,2,21,-0.3));

        misiones.Add(nuevaMision= new Mision("Prohibe la tala de árboles en todo el país." ,
        "tasaTalaAl",
        "Legisla una nueva ley que prohiba la tala de árboles, y que esta sea permitida solo en circunstancias sumamente justificables.",
        3,70,30,2,4,56,-0.5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la quema de residuos plásticos." ,
        "tasaQuemaAl",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la quema de residuos plásticos.",
        -1,5,89,2,1,18,-0.1));

        misiones.Add(nuevaMision= new Mision("Imponer multas sobre la quema de residuos plásticos." ,
        "tasaQuemaAl",
        "Impone una nueva norma con un sistema de multas que obligue a los cuidadanos a no quemar residuos plásticos y reutilizarlos.",
        1,15,80,2,2,32,-0.25));

        misiones.Add(nuevaMision= new Mision("Suspender normativa que pretende reducir las fugas de las emisiones de los operadores de gas y petroleo." ,
        "tasaIndustrialAl",
        "Las empresas que operan mediante la géneración de emisiones basadas en gas y petroleo te ofrecen una considerable suma de dinero para suspender una normativa que les afecta.",
        -1,85,10,2,1,50,0.02));

        misiones.Add(nuevaMision= new Mision("Asigna una nueva norma que pretende reducir las fugas de las emisiones de los operadores de gas y petroleo." ,
        "tasaIndustrialAl",
        "Las empresas que operan mediante la géneración de emisiones basadas en gas y petroleo deberán pagar multas por las fugas en sus operaciones.",
        1,82,20,2,2,28,-0.02));

        misiones.Add(nuevaMision= new Mision("Frena la norma de vertido de métales tóxicos en el país." ,
        "tasaIndustrialAl",
        "Frena una norma que controla (permite) el vertido de metales tóxicos, como ársenico y mercurio, de las centrales électricas en las vías navegables públicas.",
        2,70,28,2,2,38,-0.03));

        misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las centrales électricas del país.",
        "tasaIndustrialAl",
        "Clausura la mitad de las centrales électricas del país elegidas estrátegicamente.",
        3,70,50,2,4,80,-0.06));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las centrales électricas del país.",
        "tasaIndustrialAl",
        "¡ESTAMOS DESESPERADOS! Clausura casi todas las centrales electricas del país con tal de evitar la catástrofe ambiental.",
        4,48,80,2,5,120,-0.07));

        misiones.Add(nuevaMision= new Mision("Restringir cantidad de autos en las calles.",
        "tasaCompraAl",
        "Restringe la cantidad de autos en la calle (Mediante su matricula) para las ciudades grandes del país.",
        1,87,18,2,1,25,-0.08));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares al mes.",
        "tasaCompraAl",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día al mes.",
        -1,90,12,2,1,19,-0.03));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares a la semana.",
        "tasaCompraAl",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día a la semana.",
        2,73,25,2,1,35,-0.06));

        misiones.Add(nuevaMision= new Mision("Tres días sin vehículos particulares a la semana.",
        "tasaCompraAl",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares tres días a la semana.",
        4,65,40,2,2,85,-0.12));

        misiones.Add(nuevaMision= new Mision("Clausurar el 30% de las minas de carbón del país." ,
        "tasaIndustrialAl",
        "Cierre obligatorio del 30% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        3,70,29,2,3,40,-0.03));

        misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las minas de carbón del país." ,
        "tasaIndustrialAl",
        "Cierre obligatorio de la mitad de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,65,35,2,4,56,-0.05));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las minas de carbón del país." ,
        "tasaIndustrialAl",
        "Cierre obligatorio del 80% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,60,50,2,5,70,-0.08));

        misiones.Add(nuevaMision= new Mision("Clausurar TODAS de las minas de carbón del país." ,
        "tasaIndustrialAl",
        "Cierre obligatorio de todas las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,65,70,2,6,98,-1));

        misiones.Add(nuevaMision= new Mision("Prohibir la implementación de nuevas centrales électricas.",
        "tasaIndustrialAl",
        "Prohíbe la implementación de nuevas centrales électricas que funcionen con carbón en las regiones más contaminadas del país.",
        3,60,30,2,5,50,-0.05));

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en el transporte." ,
        "tasaCompraAl",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en el transporte. ",
        2,65,36,2,4,58,-1));

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en la industria en general." ,
        "tasaIndustrialAl",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en todas las empresas. ",
        2,65,40,2,6,65,-0.08));

        misiones.Add(nuevaMision= new Mision("Prohibe la instalación de nuevos sistemas de calefacción." ,
        "tasaIndustrialAl",
        "Legisla una nueva ley que prohibe la instalación y fabricación de nuevos sistemas de calefacción a base de gásoleo. ",
        1,80,18,2,2,30,-0.03));

        misiones.Add(nuevaMision= new Mision("Favorece la compra de coches électricos",
        "tasaCompraAl",
        "Exenta del impuesto de matriculación y aumenta las primas de compra de automóviles ecológicos.",
        -2,90,20,2,2,35,-0.08));

        misiones.Add(nuevaMision= new Mision("Impulsa la sivicultura",
        "tasaPlantacionAl",
        "Promueve campañas publicitarias y eventos en los que se traten actividades relacionadas con el cultivo, el cuidado y la explotación de los bosques y montes.",
        -1,95,20,2,1,35,0.3));

        misiones.Add(nuevaMision= new Mision("Impuesto sobre los billetes de avión",
        "tasaCompraAl",
        "Fija un impuesto a nivel nacional en cada compra realizada por billete de avión.",
        2,70,28,2,1,40,-0.08));

         misiones.Add(nuevaMision= new Mision("Inversión en empresas que opten por el uso de energías renovables",
        "tasaCompraAl",
        "Reduce impuestos y abre oportunidades para las empresas que decidan operar con energías renovables (éolica, solar, biomasa)",
        2,68,60,2,5,100,-1));

        misiones.Add(nuevaMision= new Mision("Inversión en mejoras de la eficiencia energética.",
        "tasaIndustrialAl",
        "Promueve los estudios y proyectos científicos basados en las mejoras de la eficiencia energética.",
        -1,90,10,2,1,20,-0.02));

        misiones.Add(nuevaMision= new Mision("Campañas de reforestación.",
        "tasaPlantacionAl",
        "Impulsa la reforestación (plantación de árboles) mediante publicidad y actividades organizadas por diferentes sectores y comunidades.",
        1,96,8,2,1,18,0.15));

        misiones.Add(nuevaMision= new Mision("Impulsa las tecnologías de eliminación de CO2",
        "tasaIndustrialAl",
        "Invierte en tecnologías que busquen erradicar las emisiones de CO2, reduciendo o anulando sus impuestos.",
        2,76,26,2,3,34,-0.06));

        misiones.Add(nuevaMision= new Mision("Campañas para reducir el consumo de energía en los hogares.",
        "tasaIndustrialAl",
        "Promueve campañas publicitarias para que la población modere su consumo de energía lo mejor posible.",
        1,96,12,2,1,20,-0.01));

        misiones.Add(nuevaMision= new Mision("Aplicar impuestos a la energía nuclear.",
        "tasaIndustrialAl",
        "Fija un precio adicional a la cantidad de energía nuclear emitida por las industrias.",
        1,75,22,2,2,35,-0.03));

        //Arabia
        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener más de dos hijos." ,
        "tasaDeNatalidadAr",
        "Las personas que tengan más de dos hijos sin razón justificada dentro del marco legal obtendrán consecuencias penales.",
        2,65,40,3,3,67,-1));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener más de un hijo." ,
        "tasaDeNatalidadAr",
        "Las personas que tengan más de un hijo sin razón justificada dentro del marco legal obtendrán consecuencias penales.",
        3,62,45,3,4,70,-2));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a ciertos sectores de la población." ,
        "tasaDeNatalidadAr",
        "Solo los sectores poblacionales con buen equilibrio ecónomico y parejas cuya edad sea superior a 30 años pueden tener hijos. ",
        4,60,50,3,5,80,-3));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a toda la población hasta nuevo aviso." ,
        "tasaDeNatalidadAr",
        "¡ESTAMOS EN RIESGO! El marco legal establece que todos los habitantes del país están obligados a no tener hijos hasta nuevo aviso. ",
        4,46,80,3,6,120,-5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la sobrepoblación" ,
        "tasaDeNatalidadAr",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la sobrepoblación.",
        1,92,12,3,1,22,-0.5));

        misiones.Add(nuevaMision= new Mision("Campaña contra la tala de árboles." ,
        "tasaTalaAr",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la tala de árboles.",
        1,86,10,3,1,21,-0.1));

        misiones.Add(nuevaMision= new Mision("Impuesto sobre la tala de árboles para empresas autorizadas." ,
        "tasaTalaAr",
        "Asigna un costo fijo a las empresas que estén autorizadas para realizar algún tipo de tala.",
        2,86,10,3,2,21,-0.3));

        misiones.Add(nuevaMision= new Mision("Prohibe la tala de árboles en todo el país." ,
        "tasaTalaAr",
        "Legisla una nueva ley que prohiba la tala de árboles, y que esta sea permitida solo en circunstancias sumamente justificables.",
        3,70,30,3,4,56,-0.5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la quema de residuos plásticos." ,
        "tasaQuemaAr",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la quema de residuos plásticos.",
        1,5,89,3,1,18,-0.1));

        misiones.Add(nuevaMision= new Mision("Imponer multas sobre la quema de residuos plásticos." ,
        "tasaQuemaAr",
        "Impone una nueva norma con un sistema de multas que obligue a los cuidadanos a no quemar residuos plásticos y reutilizarlos.",
        1,15,80,3,2,32,-0.25));

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en el transporte." ,
        "tasaCompraAr",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en el transporte. ",
        2,65,36,3,4,58,-1));

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en la industria en general." ,
        "tasaIndustrialAr",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en todas las empresas. ",
        2,65,40,3,6,65,-0.08));

        misiones.Add(nuevaMision= new Mision("Prohibe la instalación de nuevos sistemas de calefacción." ,
        "tasaIndustrialAr",
        "Legisla una nueva ley que prohibe la instalación y fabricación de nuevos sistemas de calefacción a base de gásoleo. ",
        1,80,18,3,2,30,-0.03));

        misiones.Add(nuevaMision= new Mision("Favorece la compra de coches électricos",
        "tasaCompraAr",
        "Exenta del impuesto de matriculación y aumenta las primas de compra de automóviles ecológicos.",
        1,90,20,3,2,35,-0.08));

        misiones.Add(nuevaMision= new Mision("Impulsa la sivicultura",
        "tasaPlantacionAr",
        "Promueve campañas publicitarias y eventos en los que se traten actividades relacionadas con el cultivo, el cuidado y la explotación de los bosques y montes.",
        1,95,20,3,1,35,0.3));

        misiones.Add(nuevaMision= new Mision("Impuesto sobre los billetes de avión",
        "tasaCompraAr",
        "Fija un impuesto a nivel nacional en cada compra realizada por billete de avión.",
        2,70,28,3,1,40,-0.08));

         misiones.Add(nuevaMision= new Mision("Inversión en empresas que opten por el uso de energías renovables",
        "tasaCompraAr",
        "Reduce impuestos y abre oportunidades para las empresas que decidan operar con energías renovables (éolica, solar, biomasa)",
        2,68,60,3,5,100,-1));

        misiones.Add(nuevaMision= new Mision("Inversión en mejoras de la eficiencia energética.",
        "tasaIndustrialAr",
        "Promueve los estudios y proyectos científicos basados en las mejoras de la eficiencia energética.",
        -1,90,10,3,1,20,-0.02));

         misiones.Add(nuevaMision= new Mision("Campañas de reforestación.",
        "tasaPlantacionAr",
        "Impulsa la reforestación (plantación de árboles) mediante publicidad y actividades organizadas por diferentes sectores y comunidades.",
        -2,96,8,3,1,18,0.15));

        misiones.Add(nuevaMision= new Mision("Impulsa las tecnologías de eliminación de CO2",
        "tasaIndustrialAr",
        "Invierte en tecnologías que busquen erradicar las emisiones de CO2, reduciendo o anulando sus impuestos.",
        2,76,26,3,3,34,-0.06));

        misiones.Add(nuevaMision= new Mision("Campañas para reducir el consumo de energía en los hogares.",
        "tasaIndustrialAr",
        "Promueve campañas publicitarias para que la población modere su consumo de energía lo mejor posible.",
        1,96,12,3,1,20,-0.01));

        misiones.Add(nuevaMision= new Mision("Aplicar impuestos a la energía nuclear.",
        "tasaIndustrialAr",
        "Fija un precio adicional a la cantidad de energía nuclear emitida por las industrias.",
        1,75,22,3,2,35,-0.03));

        misiones.Add(nuevaMision= new Mision("Suspender normativa que pretende reducir las fugas de las emisiones de los operadores de gas y petroleo." ,
        "tasaIndustrialAr",
        "Las empresas que operan mediante la géneración de emisiones basadas en gas y petroleo te ofrecen una considerable suma de dinero para suspender una normativa que les afecta.",
        -1,85,10,3,1,50,-0.02));

        misiones.Add(nuevaMision= new Mision("Asigna una nueva norma que pretende reducir las fugas de las emisiones de los operadores de gas y petroleo." ,
        "tasaIndustrialAr",
        "Las empresas que operan mediante la géneración de emisiones basadas en gas y petroleo deberán pagar multas por las fugas en sus operaciones.",
        1,82,20,3,2,28,-0.02));

        misiones.Add(nuevaMision= new Mision("Frena la norma de vertido de métales tóxicos en el país." ,
        "tasaIndustrialAr",
        "Frena una norma que controla (permite) el vertido de metales tóxicos, como ársenico y mercurio, de las centrales électricas en las vías navegables públicas.",
        2,70,28,3,2,38,-0.03));

         misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las centrales électricas del país.",
        "tasaIndustrialAr",
        "Clausura la mitad de las centrales électricas del país elegidas estrátegicamente.",
        3,70,50,3,4,80,-0.06));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las centrales électricas del país.",
        "tasaIndustrialAr",
        "¡ESTAMOS DESESPERADOS! Clausura casi todas las centrales electricas del país con tal de evitar la catástrofe ambiental.",
        4,48,80,3,5,120,-0.07));

        misiones.Add(nuevaMision= new Mision("Restringir cantidad de autos en las calles.",
        "tasaCompraAr",
        "Restringe la cantidad de autos en la calle (Mediante su matricula) para las ciudades grandes del país.",
        1,87,18,3,1,25,-0.08));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares al mes.",
        "tasaCompraAr",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día al mes.",
        -1,90,12,3,1,19,-0.03));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares a la semana.",
        "tasaCompraAr",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día a la semana.",
        2,73,25,3,1,35,-0.06));

        misiones.Add(nuevaMision= new Mision("Tres días sin vehículos particulares a la semana.",
        "tasaCompraAr",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares tres días a la semana.",
        4,65,40,3,2,85,-0.12));

        misiones.Add(nuevaMision= new Mision("Clausurar el 30% de las minas de carbón del país." ,
        "tasaIndustrialAr",
        "Cierre obligatorio del 30% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        3,70,29,3,3,40,-0.03));

        misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las minas de carbón del país." ,
        "tasaIndustrialAr",
        "Cierre obligatorio de la mitad de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,65,35,3,4,56,-0.05));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las minas de carbón del país." ,
        "tasaIndustrialAr",
        "Cierre obligatorio del 80% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,60,50,3,5,70,-0.08));

        misiones.Add(nuevaMision= new Mision("Clausurar TODAS de las minas de carbón del país." ,
        "tasaIndustrialAr",
        "Cierre obligatorio de todas las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,65,70,3,6,98,-1));

        misiones.Add(nuevaMision= new Mision("Prohibir la implementación de nuevas centrales électricas.",
        "tasaIndustrialAr",
        "Prohíbe la implementación de nuevas centrales électricas que funcionen con carbón en las regiones más contaminadas del país.",
        4,40,30,3,5,50,-0.05));

        //Brasil
        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener más de dos hijos." ,
        "tasaDeNatalidadBr",
        "Las personas que tengan más de dos hijos sin razón justificada dentro del marco legal obtendrán consecuencias penales.",
        2,65,40,4,3,67,-1));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener más de un hijo." ,
        "tasaDeNatalidadBr",
        "Las personas que tengan más de un hijo sin razón justificada dentro del marco legal obtendrán consecuencias penales.",
        3,62,45,4,4,70,-2));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a ciertos sectores de la población." ,
        "tasaDeNatalidadBr",
        "Solo los sectores poblacionales con buen equilibrio ecónomico y parejas cuya edad sea superior a 30 años pueden tener hijos. ",
        4,60,50,4,5,80,-3));

        misiones.Add(nuevaMision= new Mision("Legisla una ley que prohiba tener hijos a toda la población hasta nuevo aviso." ,
        "tasaDeNatalidadBr",
        "¡ESTAMOS EN RIESGO! El marco legal establece que todos los habitantes del país están obligados a no tener hijos hasta nuevo aviso. ",
        4,46,80,4,6,120,-5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la sobrepoblación" ,
        "tasaDeNatalidadBr",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la sobrepoblación.",
        1,92,12,4,1,22,-0.5));

        misiones.Add(nuevaMision= new Mision("Campaña contra la tala de árboles." ,
        "tasaTalaBr",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la tala de árboles.",
        1,86,10,4,1,21,-0.1));

        misiones.Add(nuevaMision= new Mision("Impuesto sobre la tala de árboles para empresas autorizadas." ,
        "tasaTalaBr",
        "Asigna un costo fijo a las empresas que estén autorizadas para realizar algún tipo de tala.",
        2,86,10,4,2,21,-0.3));

        misiones.Add(nuevaMision= new Mision("Prohibe la tala de árboles en todo el país." ,
        "tasaTalaBr",
        "Legisla una nueva ley que prohiba la tala de árboles, y que esta sea permitida solo en circunstancias sumamente justificables.",
        3,70,30,4,4,56,-0.5));

        misiones.Add(nuevaMision= new Mision("Campaña de concientización sobre la quema de residuos plásticos." ,
        "tasaQuemaBr",
        "Invierte en publicidad y eventos donde se concientice a los habitantes sobre el imacto ambiental que genera la quema de residuos plásticos.",
        1,5,89,4,1,18,-0.1));

        misiones.Add(nuevaMision= new Mision("Imponer multas sobre la quema de residuos plásticos." ,
        "tasaQuemaBr",
        "Impone una nueva norma con un sistema de multas que obligue a los cuidadanos a no quemar residuos plásticos y reutilizarlos.",
        1,15,80,4,2,32,-0.25));

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en el transporte." ,
        "tasaCompraBr",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en el transporte. ",
        2,65,36,4,4,58,-1));

        misiones.Add(nuevaMision= new Mision("Asigna un impuesto sobre las emisiones de carbono en la industria en general." ,
        "tasaIndustrialBr",
        "Fija un precio a nivel nacional a la tonelada emitida de dioxido de carbono en todas las empresas. ",
        2,65,40,4,6,65,-0.08));

        misiones.Add(nuevaMision= new Mision("Prohibe la instalación de nuevos sistemas de calefacción." ,
        "tasaIndustrialBr",
        "Legisla una nueva ley que prohibe la instalación y fabricación de nuevos sistemas de calefacción a base de gásoleo. ",
        1,80,18,4,2,30,-0.03));

        misiones.Add(nuevaMision= new Mision("Favorece la compra de coches électricos",
        "tasaCompraBr",
        "Exenta del impuesto de matriculación y aumenta las primas de compra de automóviles ecológicos.",
        1,90,20,4,2,35,-0.08));

        misiones.Add(nuevaMision= new Mision("Impulsa la sivicultura",
        "tasaPlantacionBr",
        "Promueve campañas publicitarias y eventos en los que se traten actividades relacionadas con el cultivo, el cuidado y la explotación de los bosques y montes.",
        1,95,20,4,1,35,0.3));

        misiones.Add(nuevaMision= new Mision("Impuesto sobre los billetes de avión",
        "tasaCompraBr",
        "Fija un impuesto a nivel nacional en cada compra realizada por billete de avión.",
        2,70,28,4,1,40,-0.08));

         misiones.Add(nuevaMision= new Mision("Inversión en empresas que opten por el uso de energías renovables",
        "tasaCompraBr",
        "Reduce impuestos y abre oportunidades para las empresas que decidan operar con energías renovables (éolica, solar, biomasa)",
        2,68,60,4,5,100,-1));

        misiones.Add(nuevaMision= new Mision("Inversión en mejoras de la eficiencia energética.",
        "tasaIndustrialBr",
        "Promueve los estudios y proyectos científicos basados en las mejoras de la eficiencia energética.",
        -1,90,10,4,1,20,-0.02));

        misiones.Add(nuevaMision= new Mision("Campañas de reforestación.",
        "tasaPlantacionBr",
        "Impulsa la reforestación (plantación de árboles) mediante publicidad y actividades organizadas por diferentes sectores y comunidades.",
        1,96,8,4,1,18,0.15));

        misiones.Add(nuevaMision= new Mision("Impulsa las tecnologías de eliminación de CO2",
        "tasaIndustrialBr",
        "Invierte en tecnologías que busquen erradicar las emisiones de CO2, reduciendo o anulando sus impuestos.",
        2,76,26,4,3,34,-0.06));

        misiones.Add(nuevaMision= new Mision("Campañas para reducir el consumo de energía en los hogares.",
        "tasaIndustrialBr",
        "Promueve campañas publicitarias para que la población modere su consumo de energía lo mejor posible.",
        1,96,12,4,1,20,-0.01));

        misiones.Add(nuevaMision= new Mision("Aplicar impuestos a la energía nuclear.",
        "tasaIndustrialBr",
        "Fija un precio adicional a la cantidad de energía nuclear emitida por las industrias.",
        1,75,22,4,2,35,-0.03));

        misiones.Add(nuevaMision= new Mision("Suspender normativa que pretende reducir las fugas de las emisiones de los operadores de gas y petroleo." ,
        "tasaIndustrialBr",
        "Las empresas que operan mediante la géneración de emisiones basadas en gas y petroleo te ofrecen una considerable suma de dinero para suspender una normativa que les afecta.",
        -1,85,10,4,1,50,-0.02));

        misiones.Add(nuevaMision= new Mision("Asigna una nueva norma que pretende reducir las fugas de las emisiones de los operadores de gas y petroleo." ,
        "tasaIndustrialBr",
        "Las empresas que operan mediante la géneración de emisiones basadas en gas y petroleo deberán pagar multas por las fugas en sus operaciones.",
        1,82,20,4,2,28,-0.02));

        misiones.Add(nuevaMision= new Mision("Frena la norma de vertido de métales tóxicos en el país." ,
        "tasaIndustrialBr",
        "Frena una norma que controla (permite) el vertido de metales tóxicos, como ársenico y mercurio, de las centrales électricas en las vías navegables públicas.",
        2,70,28,4,2,38,-0.03));

         misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las centrales électricas del país.",
        "tasaIndustrialBr",
        "Clausura la mitad de las centrales électricas del país elegidas estrátegicamente.",
        3,70,50,4,4,80,-0.06));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las centrales électricas del país.",
        "tasaIndustrialBr",
        "¡ESTAMOS DESESPERADOS! Clausura casi todas las centrales electricas del país con tal de evitar la catástrofe ambiental.",
        4,48,80,4,5,120,-0.07));

        misiones.Add(nuevaMision= new Mision("Restringir cantidad de autos en las calles.",
        "tasaCompraBr",
        "Restringe la cantidad de autos en la calle (Mediante su matricula) para las ciudades grandes del país.",
        1,87,18,4,1,25,-0.08));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares al mes.",
        "tasaCompraBr",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día al mes.",
        -1,90,12,4,1,19,-0.03));

        misiones.Add(nuevaMision= new Mision("Día sin vehículos particulares a la semana.",
        "tasaCompraBr",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares un día a la semana.",
        2,73,25,4,1,35,-0.06));

        misiones.Add(nuevaMision= new Mision("Tres días sin vehículos particulares a la semana.",
        "tasaCompraBr",
        "Legisla una nueva ley que prohiba la circulación de todos los vehículos particulares tres días a la semana.",
        4,65,40,4,2,85,-0.12));

        misiones.Add(nuevaMision= new Mision("Clausurar el 30% de las minas de carbón del país." ,
        "tasaIndustrialBr",
        "Cierre obligatorio del 30% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        3,70,29,4,3,40,-0.03));

        misiones.Add(nuevaMision= new Mision("Clausurar el 50% de las minas de carbón del país." ,
        "tasaIndustrialBr",
        "Cierre obligatorio de la mitad de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,65,35,4,4,56,-0.05));

        misiones.Add(nuevaMision= new Mision("Clausurar el 80% de las minas de carbón del país." ,
        "tasaIndustrialBr",
        "Cierre obligatorio del 80% de las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,60,50,4,5,70,-0.08));

        misiones.Add(nuevaMision= new Mision("Clausurar TODAS de las minas de carbón del país." ,
        "tasaIndustrialBr",
        "Cierre obligatorio de todas las minas de carbón del país elegidas estrátegicamente para reducir la quema de este contaminante.",
        4,65,70,4,6,98,-1));

         misiones.Add(nuevaMision= new Mision("Prohibir la implementación de nuevas centrales électricas.",
        "tasaIndustrialBr",
        "Prohíbe la implementación de nuevas centrales électricas que funcionen con carbón en las regiones más contaminadas del país.",
        4,45,30,4,5,50,-0.05));

  



       
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

        if(PRI >=1 && PRI<1.3)
        {
          nivelActualVariable = 1;  
        }
        else if(PRI>=1.3 && PRI<1.5)
        {
          nivelActualVariable = 2;
        }
        else if(PRI>=1.5 && PRI<1.7)
        {
            nivelActualVariable = 3;
        }
        else if(PRI>=1.7)
        {
            nivelActualVariable=4;
        }
        else if(PRI<1f && PRI>0.8f)
        {
            nivelActualVariable= -1;
        }
        else
        {
            nivelActualVariable= -2;
        }


        return nivelActualVariable;
    }

    private int ComprobarNivelVariablePoblacion(double PRI)
    {
        int nivelActualVariable = 1;

        if(PRI >=1 && PRI<1.3)
        {
          nivelActualVariable = 1;  
        }
        else if(PRI>=1.3 && PRI<1.5)
        {
          nivelActualVariable = 2;
        }
        else if(PRI>=1.5 && PRI<1.7)
        {
            nivelActualVariable = 3;
        }
        else if(PRI>=1.7)
        {
            nivelActualVariable=4;
        }
        else if(PRI<=1f && PRI>0.8)
        {
            nivelActualVariable= -1;
        }
        else
        {
            nivelActualVariable = -2;
        }


        return nivelActualVariable;
    }

    private int ComprobarNivelVariableArboles(double PRI)
    {
        int nivelActualVariable = 1;
        

         if(PRI<=1 && PRI > 0.98)
        {
          nivelActualVariable = 4;  
        }
        else if(PRI<=0.98 && PRI>0.95)
        {
          nivelActualVariable = 2;
        }
        else if(PRI<=0.95 && PRI>0.93)
        {
            nivelActualVariable = 3;
        }
        else if(PRI<=0.93)
        {
            nivelActualVariable=4;
        }
        else if(PRI>1 && PRI<=1.1)
        {
            nivelActualVariable= -1;
        }
        else
        {
            nivelActualVariable = -2;
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

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de concurrencia de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaCompraCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " los nacimientos por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " las muertes por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialCh(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaQuemaCh(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaTalaCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionCh":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaPlantacionCh(misionSeleccionada.getValorExito());
                    break;
                case "tasaCompraUS":

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de concurrencia de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaCompraUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " los nacimientos por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " las muertes por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialUS(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaQuemaUS(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaTalaUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionUS":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaPlantacionUS(misionSeleccionada.getValorExito());
                    break;
                case "tasaCompraAl":

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de concurrencia de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaCompraAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " los nacimientos por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " las muertes por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialAl(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaQuemaAl(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaTalaAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionAl":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaPlantacionAl(misionSeleccionada.getValorExito());
                    break;
                case "tasaCompraAr":

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de concurrencia de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaCompraAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " los nacimientos por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " las muertes por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialAr(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaQuemaAr(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaTalaAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionAr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaPlantacionAr(misionSeleccionada.getValorExito());
                    break;
                case "tasaCompraBr":

                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de concurrencia de vehiculos a base de combustible fósil en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaCompraBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeNatalidadBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " los nacimientos por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeNatalidadBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaDeMortalidadBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " las muertes por cada mil habitantes en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaDeMortalidadBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaIndustrialBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa industrial en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaIndustrialBr(misionSeleccionada.getValorExito());
                    break;
                case "trabajadorIndustriaBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " el numero de trabajadores por industria en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTrabajadorIndustriaBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaQuemaBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de quema de plastico en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaQuemaBr(misionSeleccionada.getValorExito());
                    break;

                case "tasaTalaBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de tala de árboles en " + misionSeleccionada.getValorExito() + "."; 
                    EstadisticasController.estadisticasController.alterarTasaTalaBr(misionSeleccionada.getValorExito());
                    break;
                case "tasaPlantacionBr":
                    txt_InfoResultado.text = "Se " + comprobarAccion(misionSeleccionada.getValorExito()) + " la tasa de plantación de árboles en " + misionSeleccionada.getValorExito() + "."; 
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
