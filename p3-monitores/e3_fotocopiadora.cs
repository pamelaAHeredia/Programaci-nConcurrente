//N personas, una fotocopiadora. El orden no importa. Sólo puede fotocopiar una persona a la vez

monitor fotocopiadora{
    procedure entrarAcopiar(){
        fotocopiar(); 
    }
}

process persona[id: 1..N]{
    fotocopiadora.entrarAcopiar(); 
}

//2b- ídem a, respetando orden de llegada

monitor fotocopiadora(){
    cond cola; 
    boolean libre = true; 
    int esperando = 0;

    procedure entrar(){
        if (not libre){
            esperando ++; 
            wait(cola); 
        }
        else
            libre = false; 
    }

    procedure salir(){
        if (esprando > 0){
            esperando --; 
            signal(cola); 
        }
        else
        libre = true; 
    }
}

process persona[id: 1..N]{
    fotocopiadora.entrar(); 
    fotocopiar(); 
    fotocopiadora.salir(); 
}

//2c- ídem 2b, con prioridad personas mayores

monitor fotocopiadora{
    int esperando = 0; 
    boolean libre = true; 
    cond cola[N]; 
    colaOrdenada fila; 

    procedure entrar(idP, edad: in int){
        if(not libre){
            esperando ++; 
            insertarOrdenado(fila, idP, edad); 
            wait(cola[idP])
        }
        else {
            libre = false; 
        }
    }

    procedure salir(){
        int prox; 
        if(esperando > 0){
            esperando --; 
            prox = pop(fila, idP); 
            signal(cola[idP]);
        }
        else{
            libre = true; 
        }
    }
}

process persona[id: 1..N]{
    int edad; 
    fotocopiadora.entrar(id, edad); 
    fotocopiar(); 
    fotocopiadora.salir(); 
}

//2d - ídem a, pero se debe respetar, estrictamente, el orden de los id de los procesos

monitor fotocopiadora{
    int siguiente = 1, esperando = 0; 
    cond cola[N]; 
    
    procedure entrar(idP: in int){
        if (siguiente != idP){
            esperando ++; 
            wait(cola[idP]); 
        }
    }

    procedure salir(){
        siguiente ++; 
        if (esperando > 0){
            esperando --; 
            signal(cola[siguiente])
        }
    }
}

process persona[id: 1..N]{
    fotocopiadora.entrar(id); 
    fotocopiar(); 
    fotocopiadora.salir(); 
}

//2e - ídem b, pero hay un empleado que le indica a cada persona cuándo usar la fotocopiadora.

monitor fotocopiadora(){
    cond cola, hayCliente, terminó; 
    int esperando = 0; 

    procedure solicitar(){
        esperando ++; 
        signal(hayCliente); //si el emppleado aún no llegó, el aviso pasa de largo
        wait(cola; )
    }

    procedure próximo(){
        if (esperando == 0){ // si no hay nadie esperando, se duerme hasta que loo llamen 
            wait(hayCliente);
        }
        esprando --; 
        signal(cola); //despierta al sgt en la cola 
        wait(terminó) 
    }

    procedure salir(){  
        signal(terminó); 
    }
}

process empleado{
    int i; 
    for i= 1 to N do
        fotocopiadora.proximo(); 
}

process cliente[id: 1..N]{
    fotocopiadora.solicitar(); 
    fotocopiar(); 
    fotocopiadora.salir(); 
}

// 2f- ídem e, pero hay 10 fotocopiadoras. El empleado indica a los clientes cuál usar y cuándo. 

//Dudas: cómo le indico al cliente qué fotocopiadora le toca? 

monitor fotocopiadora[id: 1..10]{
    cond colaClientes, hayCliente, terminó; 
    cola libres[10]; 
    int esperando = 0; 

    procedure solicitar(idF : in int){
        int fotocopAsignada; 
        esperando ++; 
        signal(hayCliente); 
        wait(colaClientes); 
        fotocopAsignada = idF; 
    }

    procedure proximo(idF ; out int){
        int idF; 
        if(libres.empty()){
            wait(terminó); 
        }
        pop(libres, idF); 
        if(esperando == 0){
            wait(hayCliente);
        }
        esperando --; 
        signal(colaClientes); 
    }

    procedure terminó(){
        push(libres, idF); 
        signal(terminó);      
    }
}

process cliente[id:1..N]{
    fotocopiadora.solicitar(idF : in int); 
    fotocopiar(); 
    fotocopiadora.salir(); 
}

process empleado{
    int i; 
    for i:= 1..N do 
        fotocopiadora.proximo(idF : out int); 
}