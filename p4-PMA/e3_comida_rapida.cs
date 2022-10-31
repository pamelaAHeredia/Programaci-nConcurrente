/*2 cocineros y 3 vendedores. C clientes.*/

chan pedidos(int, text)
chan vendedor_libre(int)
chan cocinero_libre(int)
chan entrega[c](Comida)
chan para_cocinero[2](int, text)
chan para_vendedor[3](int, text)

process Cliente[id: 1..C]{
    text p; 
    Comida c;  
    send pedidos(id, p); 
    receive entrega[id](c); 
}

process Coordinador{
    int idV, idP; 
    text pedido; 
    while(true){
        receive vendedor_libre(idV)
        if (empty(pedidos)){
            idP = -1; 
            pedido = "vacío"
        }
        else{
            receive pedidos(idP, pedido); 
        }
        send para_vendedor[idV](idP, pedido); 
    }
}

process Vendedor[id: 1..3]{
    int idP, idC; 
    text pedido; 
    while(true){
        send vendedor_libre(id); 
        receive para_vendedor[id](idP, pedido); 
        if (idP <> -1){
            receive cocinero_libre(idC)
            send para_cocinero[idC](idP, pedido); 
        }
        else{
            delay(random(60..180))
        }
    }
}

process Cocinero[id: 1..2]{
    int idP; 
    text pedido; 
    Comida c; 
    while(true){
        send cocinero_libre(id); 
        receive para_cocinero(idP, pedido); 
        c = cocinar(pedido); 
        send entrega[idP](c); 
    }
}