/*3 impresoras, N usuarios, 1 director*/

chan director(text)
chan general(text)
chan impresora_libre(int)
chan imprimir[3](text)

process Impresora[id: 1..3]{
    text doc; 

    while(true){
        send impresora_libre(id); 
        receive imprimir[id](doc)
    }
}

process Coordinador{
    tect doc; 
    int idImp; 

    while(true){

        if (empty(director)){
            if (not empty(general)){
                recieve general(doc); 
                recieve impresora_libre(idImp); 
                send imprimir[id](doc); 
            }
            else{
                recieve director(doc); 
                recieve impresora_libre(idImp); 
                send imprimir[idImp](doc); 
            }
        }
    }
}

process director{

}

process Usuario[id:1..N]{

}

