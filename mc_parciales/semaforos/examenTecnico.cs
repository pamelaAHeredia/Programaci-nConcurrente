/*Resolver con SEMÁFOROS el siguiente problema. Simular un examen técnico para concursos Nodocentes
en la Facultad, en el mismo participan 100 personas distribuidas en 4 concursos (25 personas en cada
concurso) con un coordinador en cada una de ellos. Cada persona ya conoce en que concurso participa. El
coordinador de cada concurso espera hasta que lleguen las 25 personas correspondientes al mismo, les
entrega el examen a resolver (el mismo para todos los de ese concurso), y luego corrige los exámenes de esas
25 personas de acuerdo al orden en que van entregando. Cada persona al llegar debe esperar a que su
coordinador (el que corresponde a su concurso) le dé el examen, lo resuelve, lo entrega para que su
coordinador lo evalúe y espera hasta que le deje la nota para luego retirarse. Nota: maximizar la concurrencia;
sólo usar los procesos que representes a las personas y a los coordinadores; todos los procesos deben
terminar.*/

queue examen[100];
int cantAlumnos[4] = ([4],0); 
Array terminado[1..4] of queue; 
sem mutex[1..4] = ([4], 1), llegaron[1..4] = ([4], 0), avisoFinalizado[1..4] = ([4], 0), espera[1..4] = ([4], 0); 
sem esperaPrivado[100] = ([100], 0); 

process alumno[id: 1..100]{
    int nroC; 
    text e; 

    nroC = asignarConcurso();
    P(mutexcC[nroC]); 
    cantAlumnos[nroC] ++ ; 
    if (cantAlumnos[ntoC] == 25){
        V(llegaron[nroC]); 
    }
    V(mutexC[nroC]); 
    P(espera[nroC]); 
    //recibe
    //resuelve
    examen[id] = e; //entrega
    v(mutex[nroC]); 
    push(terminado[nroC], (id)); 
    v(mutexC); 
    v(avisoFinalizado[nroC]); 
    P(espera[id]); 
    e = examen[id]; //revisa la nota
    //se retira
}

process coordinador[id: 1..4]{
    P(lleagron[id])
    for i:= 1..25{
        v(espera[id]); 
        //entregaExamen
    }
    for i:= 1..25{
        P(avisoFinalizado[id]); 
        pop(terminado[id], (idA)); 
        examen[idA] = e; 
        //corrige
        examen[idA] = e; //entrega
        v(espera[idA]); 
    }     
}