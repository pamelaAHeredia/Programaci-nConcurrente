/*1. Suponga que existe un antivirus distribuido que se compone de R procesos robots
Examinadores y 1 proceso Analizador. 
Los procesos Examinadores están buscando
continuamente posibles sitios web infectados; cada vez que encuentran uno avisan la
dirección y luego continúan buscando. 
El proceso Analizador se encarga de hacer todas laspruebas necesarias con cada uno de los sitios encontrados por los robots para determinar si
están o no infectados.
a) Analice el problema y defina qué procesos, recursos y comunicaciones serán
necesarios/convenientes para resolver el problema.
b) Implemente una solución con PMS.*/


process admin{
    queue cola; 
    Sitio witio_web; 

    while(true){
        if (not empty(cola))
            Analizador?pedido() -> analizador!sitio(pop(cola(sitio_web))); 

        ⎕ examinador[*]? sitio(sitio_web) -> push(cola(sitio_web))
    }
}

process analizador{
    Sitio sitio_web; 
    text resultado; 

    while(true){
        admin!pedido(); 
        admin?sitio(sitio_web); 
        resultado = analizar(sitio_web); 
    }
}

process examinador{
    Sitio: sitio_web; 
    while(true){
        sitio_web = buscar(); 
        admin!sitio(sitio_web); 
    }
}