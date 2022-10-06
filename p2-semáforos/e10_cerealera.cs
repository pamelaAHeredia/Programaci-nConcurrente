//sólo 7 camiones pueden descargar a la vez, y no pueden ser del mismo tipo

sem cerealera = 7, maiz = 5, trigo = 5; 

procedure Maiz[id: 1..M]{
    P(maiz)
    P(cerealera)
    //descargar
    V(cerealera)
    V(maiz)
}

procedure Trigo[id: 1..T]{
    P(trigo)
    P(cerealera)
    //descargar
    V(cerealera)
    V(trigo)
}