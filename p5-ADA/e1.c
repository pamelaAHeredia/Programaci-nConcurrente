/*Se requiere modelar un puente de un único sentido que soporta hasta 5 unidades de peso.
El peso de los vehículos depende del tipo: cada auto pesa 1 unidad, cada camioneta pesa 2
unidades y cada camión 3 unidades. Suponga que hay una cantidad innumerable de
vehículos (A autos, B camionetas y C camiones). Analice el problema y defina qué tareas,
recursos y sincronizaciones serán necesarios/convenientes para resolver el problema.
a. Realice la solución suponiendo que todos los vehículos tienen la misma prioridad.
b. Modifique la solución para que tengan mayor prioridad los camiones que el resto de los
vehículos.*/

//a- todos los vehículos tienen la misma prioridad

procedure Puente is

    task Admin is 
        Entry solicitudA()
        Entry solicitudB()
        Entry solicitudC()
        Entry salidaA()
        Entry salidaB()
        Entry salidaC()
    end Admin

    task type vehículo; 

    vehículos: array(1..N) of vehículo; 

    task body vehículo is 
    begin
        if ("es auto") then 
            Admin.solicitudA()
            Admin.salidaA()
        elsif ("es camioneta") then 
            Admin.solicitudB()
            Admin.salidaB()
        else 
            Admin.solicitudC()
            Admin.salidaC()
        endif
            
    end vehículo

    task body Admin is
        peso: int 

    begin 
        peso := o; 

        loop
            select 
                when(peso < 5 ) => 
                    accept solicitudA() do 
                        peso += 1
                    end solicitudA
            or 
                when(peso < 4) => 
                    accept solicitudB() do 
                        peso += 2
                    end solicitudB
            or 
                when(peso < 3) => 
                    accept solicitudC() do  
                        peso += 3
                    end solicitudC
            or 
                accept salidaA() do 
                    peso -= 1
                end salidaA
            or 
                accept salidaB() do 
                    peso -= 2
                end salidaB
            or 
                accept salidaC() do 
                    peso -= 3 
                end salidaC
            end select
        end loop
    end Admin 
begin 
    null 
end Puente; 


/* b. Modifique la solución para que tengan mayor prioridad los camiones que el resto de los
vehículos.*/

process Puente is 
    task type vehículo; 
    vehículos: array (1..N) of vehículo; 

    task Admin is 
        Entry solicitudA()
        Entry solicitudB()
        Entry solicitudC()

        Entry salidaA()
        Entry salidaB()
        Entry salidaC()
    end Admin 

    task body vehículo is 
    begin   
        if ("es auto") then 
            Admin.solicitudA()
            Admin.salidaB()
        elsif (" es camioneta") then 
            Admin.solicitudB()
            Admin.solicitudC()
        else 
            Admin.solicitudC()
            Admin.salidaC()
        endif 
    end Vehículo; 

    task body Admin is 
        peso: int; 
    begin   
        peso = 0; 

        loop    
            select
                when (peso < 2 ) => 
                    accept solicitudC() do 
                        peso += 3
                    end solicitudC
                or 
                    when (solicitudC'count == 0 and peso < 5) => 
                        accept solicitudA() do 
                            peso += 1
                        end solicitudA
                or 
                    when (solicitudC'count == 0 and peso < 4) => 
                        accept solicitudB() do 
                            peso += 2
                        end solicitudB
                or 
                    accept salidaA() do 
                        peso -= 1
                    end salidaA
                or 
                    accept salidaB() do 
                        peso -= 2
                    end salidaB
                or 
                    accept salidaC() do 
                        peso -=3 
                    end salidaC
            end select
        end loop
    end Admin 
begin 
    null 
end Puente; 



              



