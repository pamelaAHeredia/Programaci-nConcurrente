//hay E empleados y N clientes


//para maximixar la concurrencia, necesito que los clientes se puedan ir encolando 
//cuando los empleados estén ocupados, así que voy a usar un monitor para la cola
monitor cola{
    cond turno; 
    cola eLibres; 
    int esperando = 0, libres = 0; 

    procedure solicitarAtencion(idE: out int){
        if (libres == 0){
            esperando ++; 
            wait(turno)
        }
        else{
            libres --; 
        }
        pop(eLibres, (idE)); 
    }   

    procedure proximo(idE: in int){
        push(eLibres, (idE)); 
        if (esperando > 0){
            signal(turno); 
        }
        else{
            libres ++; 
        }
    }
}


//tengo un monitor por cada empleado
monitor atencion[id: 1..E]{
    cond vcEmpleado, vcCliente; 
    boolean clienteListo = false; 

    procedure entregarListado(L: out text, C: in text){
        L = listado; 
        clienteListo = true; 
        signal(vcEmpleado); 
        wait(vcCliente); 
        //recibe comprobante
        signal(vcEmpelado); //gracias, vuelva prontos
    }

    procedure recibirListado(L: in text){
        if (not clienteListo){
            wait(cvEmpelado); 
        }
        listado = L; 
    }

    procedure entregarComprobante(C: out text){
        C = generarComprobante(listado); 
        signal(vcCliente); 
        wait(vcEmpeplado);
        clienteListo = false;  
    }
}

process Cliente[id: 1..N]{
    int empAsignado; 
    text listado, comprobante; 

    cola.solicitarAtencion(empAsignado); 
    atencion[empAsignado].entregarListado(listado, comprobante); 
}

process Empleado[id: 1..E]{
    text listado, comprobante; 

    cola.proximo(id); 
    atencion[id].recibirListado(listado); 
    comprobante = generarComprobante(listado); 
    atencion[id].entregarComprobante(comprobante); 
}