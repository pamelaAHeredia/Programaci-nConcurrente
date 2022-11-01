/*En una clínica existe un médico de guardia que recibe continuamente peticiones de
atención de las E enfermeras que trabajan en su piso y de las P personas que llegan a la
clínica ser atendidos.
Cuando una persona necesita que la atiendan espera a lo sumo 5 minutos a que el médico lo
haga, si pasado ese tiempo no lo hace, espera 10 minutos y vuelve a requerir la atención del
médico. Si no es atendida tres veces, se enoja y se retira de la clínica.
Cuando una enfermera requiere la atención del médico, si este no lo atiende inmediatamente
le hace una nota y se la deja en el consultorio para que esta resuelva su pedido en el
momento que pueda (el pedido puede ser que el médico le firme algún papel). Cuando la
petición ha sido recibida por el médico o la nota ha sido dejada en el escritorio, continúa
trabajando y haciendo más peticiones.
El médico atiende los pedidos dándole prioridad a los enfermos que llegan para ser atendidos.
Cuando atiende un pedido, recibe la solicitud y la procesa durante un cierto tiempo. Cuando
está libre aprovecha a procesar las notas dejadas por las enfermeras*/

process Consultorio is 
    task type enfermera;
    task type paciente;

    pacientes: array(1..P) of paciente; 
    enfermeras: array(1..E) of enfermera; 

    task medico is                                                                                                                                            
        entry solicitudP; 
        entry solicitudE; 
        entry nota(n: in text); 
    end medico; 

    task body paciente is 
        intentos: int; 
    begin 
        intentos := 0; 
        while(intentos < 3) loop
            select 
                medico.solicitudP; 
            //espera a lo sumo 5 minutos
            or delay(300); 
                //espera 10 minutos
                delay(600); 
                intentos ++;  
            end select
        end loop; 
    end paciente; 

    task body enfermera is 
    begin
        loop 
            select 
                medico.solicitudE; 
            else
                medico.nota("una nota"); 
            end select
        end loop
    end enfermera

    task body medico is 
        n: text; 
    begin
        loop 
            select 
                accept solicitudP do 
                    delay(random.int)
                end solicitudP
            or 
                when(solicitudP'count == 0) => 
                    accept solicitudE do 
                        delay(random.int)
                    end solicitudE
            or 
                when(solicitudP'count == 0 and solicitudE'count == 0) => 
                    accept nota(nota: in text) do 
                        n := nota; 
                    end nota 
            end select
        end loop
    end medico 
begin 
    null 
End Consultorio; 


    

