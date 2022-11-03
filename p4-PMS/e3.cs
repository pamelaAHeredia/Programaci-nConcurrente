/*En un examen final hay N alumnos y P profesores. Cada alumno resuelve su examen, lo
entrega y espera a que alguno de los profesores lo corrija y le indique la nota. Los
profesores corrigen los exámenes respectando el orden en que los alumnos van entregando.
a) Considerando que P=1.
b) Considerando que P>1.
c) Ídem b) pero considerando que los alumnos no comienzan a realizar su examen hasta
que todos hayan llegado al aula.
Nota: maximizar la concurrencia y no generar demora innecesaria.*/

//----- P=1 ---------

/*Cada alumno resuelve su examen, lo
entrega y espera a que alguno de los profesores lo corrija y le indique la nota.*/
process alumno[id: 1..N]{
    text examen; 
    int nota; 

    e = resolverExamen(e); 
    admin!resolución(e, id); 
    profesor?nota(n); 
}

/*corrigen los exámenes respectando el orden en que los alumnos van entregando

Necesito un proceso que mantenga el orden.*/
process admin{
    text e; 
    int idA; 
    queue cola; 

    for i:= 1..N*2 { //este loop, es así? 
        if (not empty(cola)); 
            profesor?pedido() -> pop(cola(e, idA)); profesor!siguiente(e, idA); 

        ⎕ alumno[*]?resolución(e, idA) -> push cola(e, idA); 
    }
}

process profesor{
    text e; 
    int idA, n; 

    for i:=1..N {
        admin!pedido(); 
        admin?siguiente(e, idA); 
        n = corregir(e); 
        alumno[idA]!nota(n); 
    }
}

//----- P > 1 ---------

process alumno[id: 1..N]{
    text examen; 
    int nota; 

    e = resolverExamen(e); 
    admin!resolución(e, id); 
    profesor?nota(n); 
}

process admin{
    text e; 
    int idA, idP, cantidad = N; 
    queue cola; 

    while(cant > 0 ) {
        if (not empty(cola)); 
            profesor[*]?pedido(idP) -> pop(cola(e, idA)); cant --; profesor!siguiente(e, idA); 

        ⎕ alumno[*]?resolución(e, idA) -> push cola(e, idA); 
    }
}

process profesor{
    text e; 
    int idA, n; 

    while(hay exámenes??){
        admin!pedido(id); 
        admin?siguiente(e, idA); 
        n = corregir(e); 
        alumno[idA]!nota(n); 
    }
}