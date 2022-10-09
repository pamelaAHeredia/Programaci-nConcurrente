/*Resolver con SEMÁFOROS el siguiente problema. Simular un examen escrito que deben rendir 60
alumnos repartidos en 3 aulas (20 alumnos en cada aula) con un docente en cada una de ellas. Cada alumno
ya tiene asignado el aula en la que debe rendir. El docente de cada aula espera hasta que sus 20 alumnos
hayan llegado para darles el enunciado del examen (el mismo para todos los alumnos), y luego les corrige el
examen de acuerdo al orden en que van entregando. Cada alumno cuando llega debe esperar a que su docente
(el correspondiente a su aula) le dé el enunciado del examen, lo resuelve, lo entrega para que el mismo
docente lo corrija y le deje la nota. Cuando el alumno ya tiene su nota se retira. Nota: maximizar la
concurrencia; sólo usar los procesos que representes a los alumnos y a los docentes; todos los procesos
deben terminar.*/

cantAlumnos[1..3] = ([3], 0); 
array alumnos[1..3] of queue; 
text examen[60]; 
sem 
    aviso[1..3] = ([3], 0),
    mutex [1..3] = ([3], 1),
    espera [1..3] = ([3], 0),
    esperaPrivado [1..60] = ([60], 0); 

process alumno[id: 1..60]{
    text e;
    int aula; 

    aula = asignarAula(); 
    p(mutex[aula]); 
    cantAlumnos[aula] ++; 
    if (cantAlumnos[aula] == 20){
        v(aviso[aula]); 
    }
    v(mutex[aula]); 
    p(espera[aula]); 
    //recibe examen
    e = resolverExamen(); 
    examen[id] = e; 
    p(mutex[aula]); 
    push(alumnos[aula], (id)); 
    v(mutex[aula]); 
    v(aviso[aula]); 
    p(espera[id]); 
    e = examen[id]; //recibe la nota 
    //se retira
}

process Profesor[id: 1..3]{
    text e; 
    int idA; 

    P(aviso[id]); 
    for i:= 1..20{
        v(espera[id]); 
        //entrega examen
    }

    for i:= 1..20{
        P(aviso[id]); 
        pop(alumnos[id], idA); 
        e = examen[idA]; //recibe
        //corrige
        examen[idA] = e //devuelve
        v(espera[idA]); //avisa que está la nota y que se puede retirar
    }
}