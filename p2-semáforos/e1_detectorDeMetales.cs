//N personnas, 1 detector de metales. 

//si el detector está libre, las personas deben pasar

program e1A{
    sem detector = 1; 

    Process Persona[id: 1..N]{
    P(detector); 
    --pasar
    v(detector); 
    }
}


//hay 3 detectores
sem detector = 3; 

Process Pesrona[id: 1..N]{
    P(detector); 
    --pasar
    v(detector); 
}