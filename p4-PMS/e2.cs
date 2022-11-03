/*En un laboratorio de genética veterinaria hay 3 empleados. 
El primero de ellos continuamente prepara las muestras de ADN; cada vez que termina, 
se la envía al segundo empleado y vuelve a su trabajo. 

El segundo empleado toma cada muestra de ADN
preparada, arma el set de análisis que se deben realizar con ella y espera el resultado para
archivarlo. 

Por último, el tercer empleado se encarga de realizar el análisis y devolverle el
resultado al segundo empleado.*/

process empleado1{
    Muestra m; 
    while(true){
        m = prepararMuestra(); 
        admin!preparado(m); 
    }
}
// para maximizar la concurrencia necesito que alguien reciba y envía las muestras. 
process admin{
    cola muestras; 
    Muestra m; 

    while(true){
        if (not empty(muestras)); 
            empleado2?.siguiente() -> empleado2?.muestra(pop(muestras(m))); 

        ⎕ empleado1?preparado(m) -> push(muestras(m)); 
    }
}

/*El segundo empleado toma cada muestra de ADN
preparada, arma el set de análisis que se deben realizar con ella y espera el resultado para
archivarlo. */

process empleado2{
    Muestra m; 
    Set s; 
    text r; 

    while(true){
        admin!siguiente(); 
        admin?muestra(m); 
        s = armarSet(m); 
        empleado3!set(s); 
        empelado3?resultado(r); 
        archivar(r); 
    }
}

/*Por último, el tercer empleado se encarga de realizar el análisis y devolverle el
resultado al segundo empleado.*/

process empelado3{
    text r; 
    Set s; 

    while(true){
        empleado2?set(s); 
        r = analizar(s); 
        empleado2!resultado(r); 
    }
}