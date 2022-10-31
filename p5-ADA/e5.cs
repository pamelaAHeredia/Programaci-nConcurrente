/*En un sistema para acreditar carreras universitarias, hay UN Servidor que atiende pedidos
de U Usuarios de a uno a la vez y de acuerdo con el orden en que se hacen los pedidos.
Cada usuario trabaja en el documento a presentar, y luego lo envía al servidor; espera la
respuesta de este que le indica si está todo bien o hay algún error. Mientras haya algún error,
vuelve a trabajar con el documento y a enviarlo al servidor. Cuando el servidor le responde
que está todo bien, el usuario se retira. Cuando un usuario envía un pedido espera a lo sumo
2 minutos a que sea recibido por el servidor, pasado ese tiempo espera un minuto y vuelve a
intentarlo (usando el mismo documento).*/ 

procedure sistema is
    task servidor is 
        entry pedido(d: in text, ok: out boolean); 
    end servidor

    task type usuario; 

    usarios: array(1..N) of usuario;

    task body servidor is 
    begin
        loop 
            accept pedido(doc, ok) do 
                ok = corregir(doc);     
                endif 
            end pedido 
        end loop
    end servidor 

    task body usuario is 
        ok, correcto: boolean; 
        doc: text;
    begin 
        ok := false
        corecto := false 
        doc := generarDocumento(); 
        while (not ok) loop
            select 
                servidor.pedido(documento, correcto); 
                if (correcto) then  
                    ok := true; 
                else 
                    documento := corregirDocumento(documento); 
                endif 
            or delay(120); 
                delay(60); 
            end select; 
        end loop
    end usuario; 

begin 
    null 
end sistema; 