//50 alumnos, 10 enunciados, 1 profesor. 

int tareaTerminada[10] = ([10], 0); 
sem mutex = 1, finalizó = 0; 
cola alumnos; 

process alumno[id: 1..50]{
    int nrotarea, puntaje; 
    P(mutex)
    nroTarea = elegirTarea();  
    v(mutex); 
    //realizarTarea()
    p(mutex)
    push(alumnos,(nroTarea)); 
    v(mutex); 
    v(finalizó); 
    P(espera[nroTarea]); 
    puntaje = tareaTerminada[nroTarea]; 
}

process Profesor{
    int puntaje = 10; 
    int nroT; 
    p(finalizó); 
    v(mutex); 
    pop(alumnos, (nroT));  
    v(mutex); 
    if(tareaTerminada[nroT] == 5){
        tareaTerminada[nroT] = puntaje; 
        puntaje --;
        signal(espera[nroT]); 
    }
    else{
        tareaTerminada[nroT] += 1; 
    }
}