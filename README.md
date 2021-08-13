# FacturacionBack

### IMPORTANTE 
-se agrega este nuevo repositorio debido a que para poder desplegar la UI se estaban presentando problemas.\
-aqui podras ver las apis creadad y probar su funcionamiento https://facturacionback20210813172116.azurewebsites.net/swagger/index.html


## para tener encuenta 
-la api para factura aun no tiene metodo terminado para editar ni eliminar.\
-base de datos utilizada :  SqlServer

### explicacion de lo creado en backend

-se crea modelos(tablas) cliente, producto, factura.\
-se crean Dtos copias de los modelos para no afectar directamente las tablas.\
-se agrega mapeo para que cuando operemos dentro de un Dto podamos pasar esos datos actualizado al modelo o sabe de datos real.\
-se agrega contexto que permite realizar la relacion entre base de datos a modelos de .net.\
-se realizan migraciones desde .net a base de datos sqlServer mediante codigo.\
-se realizan controllers para cada modelo(tabla) para sus diferentes metodos get, post, put, delete, getById.\
se realiza deploy para ser consumida desde servidor externo.


