//4 procesos, N fallos 

//a)  imprimir en pantalla los id de todos los errores críticos
//para maximizar la concurrencia, lo mejos es que cada proceso recorra una parte del vector y busque errores críticos
//la función de asingación de inicio y fin les asigna un intervalo del vector a cada proceso

cola fallos[N]; 

process Proceso[iid: 1..4]{
    int inicio, fin; 
    sem pantalla = 1; 

    inicio = asignarInicio(fallos, N); 
    fin = asignarFin(fallos, N); 

    for i= inicio to fin {
        if(fallos[i].nivel = 4){
            P(pantalla); 
            --imprimir
            v(pantalla); 
        }
    }
}

// b) - calcular cantidad de fallos por nivel de gravdedad
// recurso compartido: contador de fallos

cola fallos[N]; 
int contadorDeFallos[4] = ([4], 0); 
sem contador[4] = ([4], 1); 
int nivel; 

process Proceso[id: 1..4]{
    int inicio, fin; 

    inicio = asignarInicio(); 
    fin = asignarFin(); 

    for i:= inicio to fin{
        nivel = fallos[i].nivel; 
        P(contador[nivel])
        contadorDeFallos[nivel] += 1; 
        V(contador[nivel])
    }
}

//c) - idem b, pero cada proceso se encarga de contar los fallos de un determinado nivel de gravedad
//sólo hay que contar, no modifica nada

cola fallos[N]; 
int contadorDeFallos[4] = ([4], N); 

process Proceso[id: 1..4]{
    int cantidad = 0; 

    for i:= 1 to N {
        if(fallos[i] == id){
            cantidad ++; 
        }
    }
    contadorDeFallos[id] = cantidad; 
}

