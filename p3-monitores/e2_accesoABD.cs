monitor motorDB{
    cond cola; 
    int leyendo = 0, esperando = 0; 
    boolean libre = true; 

    procedure entrar(){
        if (not libre){
            if(leyendo >= 5){
                esperando ++; 
                wait(cola)
            }
        }
        else{
            libre = false;
        }
        leyendo ++;    
        
    }

    procedure salir(){
        leyendo --; 
        if(esperando > 0){
            esperando --; 
            signal(cola)
        }
        else{
            libre = true; 
        }
    }
}

process lector[id: 1..N]{
    monitor.entrar(); 
    //lee base de datos
    monitor.salir(); 
}