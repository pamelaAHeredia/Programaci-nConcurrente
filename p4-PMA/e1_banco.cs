//N clientes, un solo empleado que atiende por orden de llegada

chan cola(int)
chan turno[1..N](int)

process persona[id: 1..N]{
    int nro; 
    while(true)[
        send cola(id); 
        receive turno[id](nro); 
    ]
}

process Empleado{
    int nro, idP; 
    while(true){
        receive cola(idP); 
        nro = siguienteNumero()
        send turno[idP](nro); 
    }
}

//idem a, pero con 2 clientes. La solución es la misma. 

//ídem a, pero si no hay empelados para atender, los empleados realizan tareas administrativas. 
//Para evitar que el proceso se quede bloqueado esperando, si la cola tiene un solo elemento y los dos proesos 
//consultaron al mismo tiempo pero sólo uno de ellos pudo acceder, necesito un proceso coordinador. 

chan cola(int)
chan turno[1..N](int)
chan pedido(int)
chan proximo[1..2](int)

process Persona[id: 1..N]{
    while(true){
        int idE; 
        //llega
        send cola(id)
        receive turno[id](idE); 
    }
}

process Coordinador{
    while(true){
        int idE, idP; 
        receive pedido(idE); 
        if (empty(cola)){
            idP = -1; 
        }
        else{
            receive cola(idP)
        }
        send proximo[idE](idP); 
    }

}

process Empleado[id: 1..2]{
    int idP; 
    while(true){
        send pedido(id)
        receive proximo[id](idP)
        if (proximo[id] <> -1){
            send turno[idP](id)
        }
        else{
            delay(900)
        } 
    }
}