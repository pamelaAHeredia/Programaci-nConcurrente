/*Resolver con MONITORES la siguiente situación. En la guardia de traumatología de
un hospital trabajan 5 médicos y una enfermera. A la guardia acuden P Pacientes que
al llegar se dirigen a la enfermera para que le indique a que médico se debe dirigir y
cuál es su gravedad (entero entre 1 y 10); cuando tiene estos datos se dirige al médico
correspondiente y espera hasta que lo termine de atender para retirarse. Cada médico
atiende a sus pacientes en orden de acuerdo a la gravedad de cada uno. Nota:
maximizar la concurrencia.*/

monitor enfermera{
    cola medicosLibres; 
    int cantLibres = 0; 

    // al llegar se dirigen a la enfermera para que le indique a que médico se debe dirigir
    procedure solicitarAtencion(idM: out int, gravedad: out int){
        idM = asignarMedico(); 
        gravedad = determinarGravedad(); 
    }

}

//en un principio puse todo en el monitor del médico, pero no maximiza la concurrencia, así que necesito un monitor para la cola
monitor cola[id: 1..5]{
    cond espera[N], hayPaciente; 
    cola pacientes; 

    // cuando tiene estos datos se dirige al médico correspondiente 
    procedure llegada(idP, gravedad: in int){
        pacientes.insertarOrdenado(idP); 
        signal(hayPaciente); 
        wait(espera[idP]); 
    }

    procedure proximo(){
        if(pacientes.length == 0){
            wait(hayPaciente)
        }
        pop(pacientes[idP]); 
        signal(espera[idP]); 
    }
}

monitor consultorio[id: 1..5]{
    boolean llegóP = false; 
    cond vcMedico, vcPaciente; 
    
    procedure esperaConsulta(){
        llegó = true; 
        signal(vcMedico); 
        //en atención
        wait(vcPaciente); 
    }

    procedure atenderPaciente(){        
        if (no llegó){
            wait(vcMedico)
        }
        //atender
        signal(vcPaciente); 
        llegó = false; 
    }

}

process Paciente[id: 1..N]{
    int gravedad, idM; 
    enfermera.solicitarAtencion(idM, gravedad); 
    cola[idM].llegada(id, gravedad); 
    consultorio.iniciarConsulta(id); 
}

process Medico[id: 1..4]{
    int idP; 
    while(true){
        cola.proximo(idP); 
        //atenderPaciente(); 
        consultorio.finalizar(idP); 
    }

}