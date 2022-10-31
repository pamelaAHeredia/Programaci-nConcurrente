/*Se quiere modelar el funcionamiento de un banco, al cual llegan clientes que deben realizar
un pago y retirar un comprobante. Existe un único empleado en el banco, el cual atiende de
acuerdo con el orden de llegada. Los clientes llegan y si esperan más de 10 minutos se
retiran sin realizar el pago.*/

procedure Banco is 
    Task empleado is 
        entry solicitud(p: in real, c: out text)
    end empleado 

    Task type cliente; 

    clientes: array(1..N) of cliente;

    Task body empleado  is 
    begin
        loop
            accept solicitud(p: in real, c: out text) do 
                c = procesarPago(p)
            end solicitud
        end loop
    end empleado 

    Task body empleado is 
        p: real, c: text; 

    begin 
        select 
            empleado.solicitud(p: out real, c: in text)
        or delay 600
            null
        end select
    end empleado; 

begin 
    null
End Banco; 