//se hacen T piezas por día. Hay E empleados, fabrican de a una pieza por vez. 

int piezasFabricadas[E] = ([E], 0); 
int totalPiezas = T, empelados = 0; 
sem mutex = 1, barrera = 0, premio[E] = ([E], 0); 

process Empleado[id: 1..E]{
    p(mutex)
    empelados ++; 
    if(empleados == E){
        for i:= 1 to E
            v(barrera); 
    }
    v(mutex); 
    P(barrera); 
    p(mutex)
    if( totalPiezas > 0){; 
        totalPiezas--; 
        p(mutex); 
        piezasFabricadas[id] += 1; 
        V(mutex); 
    }
    else{
        v(mutex); 
        p(premio[id]); 
        //revisar si hay premio
    }   
}