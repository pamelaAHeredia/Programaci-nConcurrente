Process Corredor[id 1..C]{
    Carrera.iniciar(); 
    --terminó de correr 
    Cola.EsperarTurno(); 
    Expendedora.TomarBotella(); 
}

//lo necesito para permitir que los procesos se encolen aunque la expendedora esté ocupada
Monitor Cola{
    cond colaProcesos; 
    int esperando = 0; 
    libre = true; 

    procedure EsperarTurno(){
        if (not libre){
            esperando ++; 
            wait(colaProcesos); 
        }
        else{
            libre = false; 
        }
    }

    proedure proximo(){
        if (esperando > 0){
            esperando --; 
            signal(colaProcesos); 
        }
        else{
            libre = true; 
        }
    }
}

//necesito que los procesos entren de a uno
Monitor Expendedora{
    boolean stock = true;
    int botellas = 20; 
    cond vacío, lleno; 

    procedure entregarBotella(b: out Botella){  
        if(not stock){
            stock = false; 
            signal(vacío); 
            wait(lleno); 
        }
        botellas --; 
        b = botella; 
    }

    procedure reponerStock(){
        if (stock){
            wait(vacío); 
        }
        botellas = 20; 
        stock = true; 
        signal(lleno); 
    }
}

Process Repositor(){
    while(true){
        Expendedora.reponerStock(); 
    }
}