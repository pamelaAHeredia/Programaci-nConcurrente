sem mutex = 1, hayCinco = 0, finalizado[50] = ([50],0), barrera[50] = ([50],0); 
int total = 0; 
cola c, vacunados; 

process Paciente[id: 1..50]{
    p(mutex)
    push(c,(id)); 
    total++; 
    if(total == 5){
        total= 0; 
        V(hayCinco); 

    }
    v(mutex)
    P(barrera[id]); 
    P(finalizado[id]); 
}


process empleado{
    int vacunados = 0; 
    while(vacunados < 50){
        P(hayCinco);  
        for i := 1 to 5{
            p(mutex);
            pop(C,(id)); 
            v(mutex); 
            V(barrera[id]);
            //vacunarPersona(); 
            vacunados++; 
            push(vacunados,(id)); 
        }
        for i:= 1 to 5{
            pop(vacunados,(id)); 
            v(finalizado[id]); 
        }
    }
    
}