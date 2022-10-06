//se hacen T piezas por día. Hay E empleados, fabrican de a una pieza por vez. 

int piezasFabricadas[E] = ([E], 0); 
int totalPiezas = T, empelados = 0; 
sem mutex = 1, barrera = 0, premio[E] = ([E], 0); 

process Empleado[id: 1..E]{
    p(mutex)
    if(empleados < E){
        empleados ++; 
        v(mutex)
        v(barrera)
    }
    v(mutex); 
    for i := 1 to E{
        p(barrera)
    }
    p(mutex)
    if( totalPiezas > 0){; 
        totalPiezas--; 
        p(mutex); 
        piezasFabricadas[id] += 1; 
    }
    else{
        v(mutex); 
        p(premio[id]); 
    }   
}