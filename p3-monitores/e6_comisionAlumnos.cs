//50 alumnos realizan tareas de a pares, que son corregidas por un jtp. 
// cuando llegan todos, el jtp les asigna un nro de grupo a cada uno (1..25) 
// alumnos: llegar, hacer fila. esperar nro de grupo, realizar la tarea, entregar, y esperar el resultado. 

Process Alumno[id: 1..50]{
    int grupo; 
    text tarea; 

    Admin.Llegada(nro)
    grupo = nro; 
    tarea = resolver()
    Admin.Resultado(grupo)
}

Process Jtp{
    int grupo, puntaje = 25; 
    admin.iniciar(grupo); 
    for i:= 1..50{
        admin.corregir(puntaje); 
    }
}

Monitor Admin{
    int cantidad = 0; 
    cond estánTodos, espera[25], cola, leído, terminó; 
    int tarea[25]; 

    procedure llegada(nro: in int){
        cantidad ++
        if(cantidad == 50){
            signal(estánTodos); 
        }
        wait(cola);
        num = nro;  
        signal(leído)
    }

    procedure iniciar(nro: out int){
        if(cantidad < 50){
            wait(estánTodos)
        }
        for i:= 1..50{
            nro = Asignargrupo(); 
            signal(cola); 
            wait(leído); 
        }
    }

    procedure entregarTarea(nro: in int, puntaje: in int){
        push(fila(nro)); 
        tareas[nro]+=1; 
        signal(terminó); 
        wait(espera[nro])); 
        puntaje = tarea[nro]; 
    }

    procedure corregir(puntaje: in-out int){  
        int nro; 
        if(empty(fila)){
            wait(terminó); 
        }
        pop(fila, (grupo))
        nro = grupo; 
        if(tareas[nro] = 2){
            puntaje --; 
            tarea[nro] = puntaje;  
            signal_all(espera[nro]);    
        }       
    }
}