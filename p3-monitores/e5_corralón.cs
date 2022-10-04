// Hay N clientes y un solo empelado para atenderlos por orden de llegada. 

monitor corralon{
    cond cola, hayCliente, list, comp; 
    int espera = 0; 
    boolean listo = false; 

    procedure solicitarAtencion(){
        esperando++; 
        signal(hayCliente); 
        wait(cola); 
    }

    procedure proximo(){
        if(espera == 0){
            wait(hayCliente); 
        }
        esperando --; 
        signal(cola); 
    }

    procedure atencion(l: in text, c: out text){ 
        lista = l;  
        listo = true;  
        signal(list); 
        wait(comp); 
        comprobante = c; 
        signal(fin);  
    }

    procedure esperarLista(l: out txt){
        if(not listo){
            wait(list); 
        }
        lista = l; 
    }

    procedure entergarComprobante(c: in text){
        comprobante = c; 
        signal(comp); 
        wait(fin); 
        listo = false; 
    }
}

process cliente[id: 1..N]{
    //llega
    corralon.solicitarAtencion(); 
    corralon.atencion(); 
}

process empleado{
    txt : lista; 
    int i; 
    for i := 1..N do 
        corralon.proximo(); 
        corralon.esperarLista(lista); 
        comp = generar comprobante para el cliente; 
        corralon.entergarComprobante(comp); 
    end;     
}