/*N clientes atendidos por un empleado. 10 cabinas*/

chan solicitud_cabina(int)
chan cola_cobro(int, Cabina)
chan turno[int](Cabina)
chan monto_llamada[N](real)
chan pago_llamada(real)
chan comprobante[N](real)
queue cabinas

process Cliente[id: 1..N]{
    Cabina cabina;  
    real monto, dinero; 
    text ticket; 

    while(true){
        send solicitud_cabina(id)
        receive turno[id](cabina)
        //usar cabina
        send cola_cobro(id, cabina)
        receive monto_llamada[id](monto)
        send pago_llamada(dinero)
        receive comprobante[id](ticket)
    }

}

process Empleado{
    int cabina, idP; 
    real monto, dinero; 
    text ticket; 

    while(true){
        if (empty(cola_pago)){
            if (not empty(cabinas)){
                receive sol_cabina(idP)
                pop(cabinas(cabina))
                send turno[idP](cabina)
            }
            else{
                receive cola_pago(idP, cabina)
                monto = calcular(cabina)
                send monto_llamada[idP](monto)
                receive pago_llamada(dinero)
                ticket = cobrar(dinero)
                send comprobante[idP](ticket)
                push(cabinas(cabina))
            }
        }
    }
}