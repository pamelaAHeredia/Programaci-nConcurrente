/*Resolver con MONITORES la siguiente situación. Simular la atención en un
corralón de materiales donde hay 4 empleados para atender a N clientes de acuerdo al
orden de llegada. Cuando el cliente llega espera a que alguno de los empleados lo
llame, se dirige hasta el escritorio del mismo y le entrega el listado de materiales que
debe comprar, luego queda esperando a que terminen de atenderlo y el empleado le
entregue la factura. Nota: maximizar la concurrencia; suponga que existe una función
HacerFactura()llamada por el empleado que simula la atención.*/

monitor cola{
    cond c, hayCliente; 
    cola  clientes, empeladoAsignado[N]; 
    
    //  Cuando el cliente llega espera a que alguno de los empleados lo llame
    procedure solicitarAtencion(idC: in int, idEmp: out int){
        push(clientes, (idC)); 
        signal(hayCliente); 
        wait(c); 
        idEmp = empeladoAsignado[idC]; 
    }

    procedure proximo(idE: in int){
        if(clientes.length == 0){
            wait(hayCliente); 
        }
        pop(clientes, (idC)); 
        empeladoAsignado[idC] = idE
        singal(c)
    }
}

monitor escritorio[id:1..4]{
    cond vcEmpleado, vcCliente; 
    boolean clienteEsperando = false; 
    
    // se dirige hasta el escritorio del mismo y le entrega el listado de materiales, luego queda esperando a que terminen de atenderlo
    procedure entregarListado(L: in text, F: in text){
        lista = L; 
        clienteEsperando = true; 
        signal(vcEmpleado); 
        wait(vcCliente); 
        factura = F; 
        signal(vcEmpleado); 
    }

    procedure recibirListado(L: out text){
        if (not clienteEsperando){
            wait(vcEmpelado)
        }
        listado = L; 
    }

    // empleado le entrega la factura
    procedure entregarFactura(F in: text){
        factura = F; 
        signal(vcCliente); 
        wait(vcEmpleado); 

    }
}

process Cliente[id: 1..N]{
    int idEmp; 
    text factura, listado; 
    cola.solicitarAtencion(id, idEmp); 
    escritorio.entregarListado(listado, factura); 
}

process Empleado[id: 1..4]{
    text factura, listado;
    cola.proximo(id); 
    escritorio.recibirListado(listado); 
    factura = HacerFactura(lisatdo); 
    escritorio.entregarFactura(factura); 
}