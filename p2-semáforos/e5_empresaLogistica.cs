//N contenedores. Cada uno almacena un paquete

//a) Hay 2 empleados: un preparador y un entregador. 
Paquete contenedor[N]; 
sem lugarLibre = N, hayPaquete = 0; 
int libre = 0, ocupado = 0; 

process Peparador{
    while(true){
        paquete = armarPaquete(); 
        P(lugarLibre); 
        contenedor[libre] = paquete; 
        libre = (libre + 1) mod N; 
        v(hayPaquete)
    }
}

process entregador{
    while(true){
        P(hayPaquete); 
        paquete = contenedor[ocupado]; 
        ocupado = (ocupado + 1) mod N; 
        v(lugarLibre); 
    }
}

//b) - ídem a, pero hay P preparadores
/* hay que proteger el recurso contador de lugares libres, más d eun preparador podría tratar de acceder a modificar 
la variable al mismo tiempo, dejando inconsistente el recurso. */ 

Paquete contenedor[N]; 
int libre = 0, ocupado = 0; 
sem hayPaquete = 0, lugarLibre = N, mutex = 1; 

process Preparador[id: 1..P]{
    while(true){
        paquete = prepararPaquete(); 
        p(lugarLibre); 
        p(mutex)
        contenedor[libre] = paquete; 
        libre = (libre + 1) mod N;
        v(mutex)
        v(hayPaquete)
    }

}

process entregador{
    while(true){
        P(hayPaquete)
        paquete = contenedor[ocupada]; 
        ocupado = (ocupado + 1) mod N; 
        v(lugarLibre); 
        //entregar
    }

}

/* c)- ídem a, pero hay E entregadores*/

Paquete contenedor[N]; 
int libre = o, ocupado = 0; 
sem lugarLibre = N, hayPaquete = 0, mutex = 1; 

process Preparador{
    while(true){
        paquete = prepararPaquete(); 
        P(lugarLibre); 
        contenedor[libre] = paquete; 
        libre = (libre +1) mod N; 
        v(hayPaquete); 
    }
}

process entregador[id: 1..E]{
    while(true){
        P(hayPaquete); 
        P(mutex); 
        paquete = contenedor[ocupado]; 
        ocupado = (ocuapdo +1) mod N; 
        v(mutex)
        //entregar
    }
}

/*d)- hay P preparadores y E entregadotes*/
Paquete contenedor[N]; 
int libre = 0, ocpado = 0; 
sem lugarLibre = N, hayPaquete = 0, mutexGuardar = 1, mutexRetirar = 1; 

process Preparador[id: 1..P]{
    while(true){
        paquete = prepararPaquete(); 
        P(lugarLibre); 
        P(mutexGuardar); 
        contenedor[libre] = paquete; 
        libre = (libre +1) mod N; 
        v(mutexGuardar)
        V(hayPaquete); 
    }
}

process entregador[id: 1..N]{
    while(true){
        P(hayPaquete); 
        P(mutexRetirar); 
        paquete = contenedor[ocupado]; 
        ocuapdo = (ocupado +1) mod N; 
        v(mutexRetirar); 
        v(lugarLibre); 
        //entregar
    }
}