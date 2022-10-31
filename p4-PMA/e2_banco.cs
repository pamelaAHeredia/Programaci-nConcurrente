/*Modelar el funcionamiento de un banco.
Tiene 5 cajas. Hay P clientes que quieren usarlas. 
Cada cliente selecciona la caja que tiene menos personas esperando y espera ser atendido. 
En cada caja, los clientes se atienden por orden de llegada. 
Luego del pago, se les entrega el comprobante.*/

chan cola[1..5](int); 
chan turno[P](int); 
chan comprobante[P](int)
chan pago[5](real)

process Persona[id: 1..P]{
    int caja; 
    txt c, msj; 
    real dinero; 

    caja = minimo(cola); 
    send cola[caja](id); 
    recieve turno[id](msj); 
    send pago[caja](dinero)
    recieve comprobante[id](c); 
}

Process caja[id: 1..5]{
    int idP; 
    txt comp; 
    real dinero; 

    while(true){
        recieve cola[id](idP); 
        send turno[idP](); 
        receive pago[id](dinero)
        comp = generarComprobante();
        send comprobante[idP](comp); 
    }
}