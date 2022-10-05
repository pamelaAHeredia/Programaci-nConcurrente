//una BD. La pueden usar 6 usuarios al mismo tiempo 
// los usuarios se clasifican en: prioridad alta y baja. 
// no pueden haber más de 4 usuarios de prioridad alta al mismo tiempo
// no puedn haber más de 5 usuarios de prioridad baja al mismo tiempo

var
    sem usuarios = 6; 
    sem alta = 4, baja = 5; 

procedure priridadAlta[id: 1..L]{
    sem(alta); 
    sem(usuarios); 
    //usar BD
    sem(usuarios); 
    sem(alta); 
}

procedure priridadBaja[id: 1..K]{
    sem(baja); 
    sem(usuarios); 
    //usar BD
    sem(usuarios); 
    sem(baja); 
}