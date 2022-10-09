//3 empleados, N clientes. Por orden de llegada. 
//cliente llega, espera que lo llamen. Va con el emleado, le entrega los papeles y espera el res

process Cliente[id: 1..N]{
    int idE; 
    text papeles, resultado; 

    cola.llegada(idEmp); 
    escritorio[idEmp].entregarPapeles(papeles, resultado); 
    escritorio.proximo(); 
}

process Empleado[id 1..3]{
    text papeles, res; 
    escritorio.atender(papeles); 
    res = analizar(papeles); 
    escritorio.entregar(res);
}

monitor cola{
    cond c; 
    cola empleados; 
    int esperando = 0, empLibres = 0; 

    procedure llegada(idE: out int){
        if(empLibres == 0){
            esperando ++; 
            wait(c); 
        }
        else {
            empLibres --; 
            pop(empleados, (idE)); 
        }
    }
}

monitor escritorio[id 1..3]{
    boolean clienteListo = false; 
    cond vcCliente; 

    procedure solicitarAtencion(papeles: in int, res: out int){ //recibe los datos y los analiza
        p = papeles; 
        clienteListo = true; 
        signal(vcCliente); 
        wait(aviso); 
        res = resultado; 
        signal(aviso); 
    }

    procedure esperarDatos(p: out text){
        if(not clienteListo){
            wait(aviso)
        }
        papeles = p; 
    }

    procedure enviarResultados(res: in text){
        r = res; 
        signal(vcCliente); 
        wait(aviso); 
        clienteListo = false; 
    }
}