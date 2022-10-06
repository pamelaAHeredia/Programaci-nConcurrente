//3 puestos para hisopar, 150 pasajeros. 
//los pasajeos se atienden de acuerdo al orden de llegada. 

cola puesto[3] = ([3], 0); 
sem hayPaciente[3] = ([3],0), espera[150] = ([150],0), finalizado[150] = ([150],0); 
sem mutex = 1, mutexPuesto[3] = ([3],1);
int totalAtendidos = 0; 

process Paciente[id: 1..150]{
    int nroPuesto; 
    P(mutex); 
    if(puesto[1].length < puesto[2].length && puesto[2].length < puesto[3].length){
        nroPuesto = 1; 
    }
    else if (puesto[2].length < puesto[1].length && puesto[1].length < puesto[3].length){
        nroPuesto = 2; 
    }
    else nroPuesto = 3; 
    push(puesto[nroPuesto], (id));  
    v(mutex); 
    v(hayPaciente[nroPuesto]); 
    p(espera[id]); 
    v(finalizado[id]); 
}

process Enfermera[id: 1..3]{
    while(totalAtendidos < 150){
        P(hayPaciente[id]); 
        P(mutexPuesto[id]); 
        pop(puesto[id], idP); 
        totalAtendidos ++; 
        v(mutexPuesto[id]); 
        v(espera[idP]); 
        //hisopar[]
        v(finalizado[idP]); 

    }
}