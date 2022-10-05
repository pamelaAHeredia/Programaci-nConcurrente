//un SO mantiene 5 instancias de un recurso almacenadas en una cola. 
//Hay P proecsos que necesitan usar una instancia del recurso. 
//Deben sacar la instancia del recurso de la cola, usarla y volver a ponerla. 

//pueden pasar 5 procesos y de a a uno van sacando los recursos

sem  recurso = 5; 
sem cola = 1; 
Cola colaRecursos[5]; 

process Proceso[id: 1..P]{
    P(recurso)
    p(cola)
    pop(colaRecursos)}
    v(cola)
    //usar
    p(cola)
    push(colaRecursos)
    v(cola)
    v(recurso)
}