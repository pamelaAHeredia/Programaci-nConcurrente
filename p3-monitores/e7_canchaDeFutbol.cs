//20 jugadores y 4 grupos de 5. 
//llegan los jugadores, conocen a su equipo. Cuando llegaron todos, deben enfrentarse a otro equipo, también listo

Monitor Equipo[id: 1..4]{
    int cancha, jugadores = 0; 

    procedure armar(nroCancha: out int){
        jugadores ++;
        if(jugadores < 5){
            wait(hayEquipo); 
        }
        else{
            AdministradorCanchas.asignarCancha(cancha); 
            nroCancha = cancha; 
        }
    }
}

Monitor AdministradorCanchas{
    int posición = 0; 

    pocedure asignarCancha(nroCancha: out int){
        posición ++; 
        if(posición < 3){
            nroCancha = 1; 
        }
        else{
            nroCancha = 2; 
        }
    }
}

Monitor Cancha[id: 1..2]{
    int jugadores = 0; 

    procedure jugarPartido(){
        jugadores ++; 
        if (jugadores < 10){
            wait (finalizar)
        }
        else{
            delay(50 minutos); 
            signal_all(finalizar); 
        }
    }
}

Process Jugador[id 1..20]{
    int nroCancha, nroEquipo; 
    nroEquipo = DarEquipo(); 
    Equipo[nroEquipo].armar(nroCancha); 
    Cancha[nroCancha].jugarPartido(); 
}