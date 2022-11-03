/*En un estadio de fútbol hay una máquina expendedora de gaseosas que debe ser usada por
E Espectadores de acuerdo al orden de llegada. Cuando el espectador accede a la máquina
en su turno usa la máquina y luego se retira para dejar al siguiente. Nota: cada Espectador
una sólo una vez la máquina*/

process espectador[id: 1..E]{
     int sgt; 

     admin!solicitud(id); 
     admin?turno(); 
     //usa la máquina
     admin!fin(); 

}

process admin{
    boolean libre; 
    queue cola; 
    int idE, sgt; 
    while(true){
        if espectador[*]?solicitud(idE) -> if (libre){espectador[idE]!turno()} else {push cola(idE)}; 
        
        ⎕ espectador[*]?fin() -> if (not empty(cola))
            {(pop(cola(sgt))); 
            espectador[sgt]!turno} 
            else {libre = true}
    }
}