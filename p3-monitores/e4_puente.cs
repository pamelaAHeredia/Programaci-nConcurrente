// N vehículos, un puente. Los vehículos pasan por orden de llegada. El puente soporta, máx, 50000kg. 

monitor Puente{
    cond cola[N]; 
    int esperando = 0, peso = 0; 
    boolean libre = true; 
    cola fila; 

    procedure entrar(kg: in int){
        if(not libre){
            if (peso +ḱg > 50000){
                esperando++                                                                                                                                                                                                                                                                                                                         
                push(fila(kg)); 
                wait(cola); 
                esperando--; 
            }
        }
        else{
            libre = false; 
        } 
        peso += kg; 
    }

    procedure salir(kg: in int){
        peso -= kg; 
        if (esperando > 0){
            if(peso + fila.top().kg <= 50000){
                pop(fila (idP, kg)); 
                signal(cola);
            }
        }
        else{
            libre = true; 
        }
    }
}

process Auto[id: 1..N]{
    int peso; 
    Puente.entrar(kg);
    --cruza el puente
    Puente.salir(kg);  
}