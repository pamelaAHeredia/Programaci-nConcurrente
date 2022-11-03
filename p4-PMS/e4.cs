/*En una exposición aeronáutica hay un simulador de vuelo (que debe ser usado con
exclusión mutua) y un empleado encargado de administrar su uso. Hay P personas que
esperan a que el empleado lo deje acceder al simulador, lo usa por un rato y se retira. El
empleado deja usar el simulador a las personas respetando el orden de llegada. Nota: cada
persona usa sólo una vez el simulador.*/

process empleado {
    queue cola; 
    boolean libre = true; 

    while(true){
        if persona[*]?pedido(idP) -> if (libre) {persona[idP]!turno; libre = false} else {push(cola(idP))}; 

        ⎕ persona[*]?fin() -> if (empty(cola)){libre = true} else {pop(cola(idP)); persona[idP]!turno()}
    }   
}

process persona[id: 1..P]{

    empleado!pedido(id); 
    empleado?turno(); 
    //usa el simulador
    empleado!fin(); 
}