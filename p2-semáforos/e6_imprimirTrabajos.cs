/*N personas deben imprimir un trabajo cada una*/

//a) No importa el orden. Hay una única impresora y se accede de a una persona a la vez. 


sem mutex = 1; 

process Persona[id: 1..N]{
    P(mutex); 
    --imprimirDocumento(); 
    v(mutex); 
}

//b) - ídem a, pero se respera el orden de llegada
//recuros a proteger: la cola, porque pueden llegar varios procesos al mismo tiempo, deben encolarse de a uno. 


sem mutex = 1, esperando[N] = ([N], 0); 
cola C; 
boolean libr e= true; 

process Persona[id: 1..N]{
    int prox; 
    p(mutex); 
    if(libre){
        libre = false; 
        v(mutex); 
    }
    else{
        push(c,(id)); 
        v(mutex); 
        p(esperando[id]); 
    }
    //imprimirDocumento(); 
    P(mutex)
    if(empty(C)){
        libre = true; 
    }
    else{
        prox = pop(C,(id)); 
        v(esperando[prox]); 
    }  
    v(mutex);  
}

//c)- ídem a, pero se debe respetar, estrictamente, el orden de los id de los procesos. 

sem mutex = 1, espera[N] = ([N], 0)); 
int sig = 1; 

process Persona[id: 1..N]{
    P(mutex); 
    if (sig != id){
        P(espera[id]); 
    }     
    v(mutex); 
    //imprimirDocumento();  
    sig++; 
    p(mutex)
    if(not empty(C)){ 
        V(espera[sig]); 
    }
    v(mutex); 
}

//d)- ídem b, importa el orden de llegada, pero hay un coordinador que le idica a cada proceso en qué momento usar la cola 

cola C; 
sem mutex = 1, espera[N] = ([N], 0), impLibre = 1; 

process coordinador{
    int sig; 
    P(hayCliente); 
    P(mutex); 
    pop(C, (sig));
    v(mutex) 
    v(espera[sig]); 
    P(impLibre)

}

process persona[id: 1..N]{
    P(mutex);
    push(C, (id)); 
    v(mutex); 
    v(hayCliente)
    P(espera[id]); 
    //imprimirDocumento();
    v(impLibre); 
}

//ídem d, pero hay 5 impresoras

cola C; 
cola impresoras[5]; 
Impresora asignadas[N]; 
int libre = 0, ocup = 0; 
sem mutex= 1, mutexImp, espera[N]= ([N], 0), impLibre= 5; 

process coordinador{
    int sig; 
    P(impLibre);
    P(mutexImnp); 
    impresora = impresoras[ocup];
    ocup = (ocup +1) mod 5;  
    v(mutexImp); 
    p(hayCliente); 
    p(mutex); 
    pop(C,(sig)); 
    v(mutex); 
    asignadas[sig] = impresora; 
    v(espera[sig]); 
}

process Persona[id: 1..N]{
    P(mutex); 
    push(C,(id)); 
    v(mutex); 
    v(hayCliente); 
    P(espera[id]); 
    impresora = asignadas[id]; 
    //imprimirDocumento(); 
    P(mutexImp); 
    impresoras[libre] = impresora; 
    libre = (libre + 1) mod 5; 
    v(mutexImp); 
    v(impresoraLibre); 
}