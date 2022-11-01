/*Se dispone de un sistema compuesto por 1 central y 2 procesos periféricos, que se
comunican continuamente. Se requiere modelar su funcionamiento considerando las
siguientes condiciones:
- La central siempre comienza su ejecución tomando una señal del proceso 1; luego
toma aleatoriamente señales de cualquiera de los dos indefinidamente. Al recibir una
señal de proceso 2, recibe señales del mismo proceso durante 3 minutos.
- Los procesos periféricos envían señales continuamente a la central. La señal del
proceso 1 será considerada vieja (se deshecha) si en 2 minutos no fue recibida. Si la
señal del proceso 2 no puede ser recibida inmediatamente, entonces espera 1 minuto y
vuelve a mandarla (no se deshecha)*/

procedure Sistema 

    task central is 
        entry proc1; 
        entry porc2; 
        fin; 
    end central

    task Cronómetro is 
        entry iniciar; 
    end Cronómetro

    task type periférico; 

    p1, p2 : periférico; 

    task body central is 
        iniciado: boolean; 
    begin   
        accept proc1() //comienza su ejecución tomando una señal del proceso 1

        loop
            select 
                accept proc1()
            or  //Al recibir una señal de proceso 2, recibe señales del mismo proceso durante 3 minutos.
                accept proc2;  
                    Cronómetro.iniciar; 
                    iniciado = true; 
                while(iniciado) loop
                    select 
                        accept fin; 
                        iniciado = false
                    or 
                        when (fin'count == 0) => 
                            accept proc2
                    end select
                end loop
            end select
        end loop
    end central 

    //son dos tareas de diferente tipo? 
    task body periférico is
        ok: boolean; 
    begin 
        ok := false; 
        //Los procesos periféricos envían señales continuamente a la central
        loop
            if ("proceso 1") then 
                //La señal del proceso 1 será considerada vieja (se deshecha) si en 2 minutos no fue recibida.
                select 
                    central.proc1
                or delay(120)
                    null 
                end select
            else
                //Si la señal del proceso 2 no puede ser recibida inmediatamente, entonces espera 1 minuto y vuelve a mandarla
                while (not ok) loop
                    select
                        central.proc2
                        ok := true
                    or delay(60)
                        null 
                    end select
                end loop; 
        end loop
    end periférico

    task body Cronómetro
    begin 
        accept iniciar;
        delay(180); 
        central.fin; 
    end Cronómetro