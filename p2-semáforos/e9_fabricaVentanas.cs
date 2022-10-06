//7 empelados: 4carp, 1 vidriero, 2 armadores. 

Marcos contMarcos[30]; 
Vidrios conVidrios[50]; 
sem marcos = 30, vidrios = 50; 
sem hayVidrio = 0, hayMarco = 0; 
sem mutexM = 1, muteV = 1; 

process Carpintero[id: 1..4]{
    while(true){
        marco = armarMarco(); 
        P(marcos); 
        p(mutexM); 
        push(contMarcos,(marco)); 
        v(mutexM); 
        P(hayMarco); 
    }
}

process Vidriero{
    while(true){
        vidrio = armarVidrio(); 
        P(vidrios); 
        P(mutexV); 
        push(contVidrios,(vidrio)); 
        V(mutexV); 
        V(hayVidrio); 
    }
}

process Armador[id:1..2]{
    while(true){
        P(hayMarco); 
        P(mutexM); 
        pop(contMarcos,(marco)); 
        V(mutexM); 
        V(marcos); 
        P(hayVidrio); 
        P(mutexV); 
        pop(contVidrios,(vidrio)); 
        V(mutexV); 
        V(vidrios); 
        ventana = armarventana(marco, vidrio); 
    }
}