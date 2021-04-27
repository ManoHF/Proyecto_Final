# User Story: VOXMAPP

La empresa VOXMAPP quiere mejorar la recolección, análisis y presentación de datos mediante la automatización de estos procesos. Esta mejora tiene el objetivo de acortar el
"time-to-decision". El trabajo se puede dividir en tres partes principales:

1) Captura de información y proceso hacia la base de datos
    - Incluye limpieza y redacción de una entrevista de recolección de datos
    - Creación de una base de datos que reciba lo obtenido en la entrevista
2) Limpieza de los datos
    - Insertar los datos en la BD corrigiendo "errores de escritura" o evitándolos: malos formatos de teléfonos, espacios en blanco, entre otros
3) Análisis y presentación de los datos
    - Crear la BD de manera que podamos obtener las vistas desde su tablas

Sabemos que la empresa tiene un registro con los hospitales a los que entrevista. Para un hospital se registra: ID único, datos geográficos (latitud, longitud, altitud), 
teléfonos (pueden ser múltiples), nombre, distrito, provincia, tipo de hospital (estatal, distrito, ...), fecha de creación. A este registro "base line" se le van agregando progresivamente nuevos hospitales

Un hospital va a tener múltiples cuestionarios, ya que busca dar un monitoreo por mes de cada uno, por lo que habría un registro mes por mes. Cada una de esas entrevistas tiene asociado un usuario encargado de hacer la baseline y un contacto que contestó la entrevista. Ambos pueden ser distintos cada mes. El cuestionario pueden recibirse en tres estados: completo, parcialmente completo, y no completo. Se pueden observar tanto cuestionarios "limpios" como "sucios". Hay distintas razones por las cuales un cuestionario esté incompleto. Dependiendo de la razón, se le asigna una acción correspondiente para pasar el estado a cuestionario completo.
 * Existe una distinción entre hospitales con entrevistas recolectadas y los que faltan por recolectar
 * Los hospitales poseen un almacén de artículos médicos específicos para COVID, dentro de él se encuentran cosas como reservas de oxígeno, paracetamos, máscaras, tests de COVID, entre otros
 * Dentro de la maquinaria de los hospitales se posee un número de ventiladores activos/inactivos y funcionantes/no-funcionantes
 * Los hospitales pueden o no tener un protocolo de cribado de COVID a la llegada de nuevos pacientes

Cabe añadir que toda estos artículos, materiales y equipos tienen un registro que nos indica cuántos días con reservas tiene cada uno. Se pueden tener reservas por 30, 15, 7 o
ningún día. Esto nos indica con cuántos recursos está trabajando el hospital.

Los pacientes pueden haber testeado positivo o negativo en la prueba de COVID. No necesariamente llegaron todos al hospital por síntomas de COVID. Las defunciones pueden ser por 
COVID u otras causas. Existe un número de pacientes recuperados del COVID.

El teléfono del hospital es un atributo muy importantes, ya que es su forma de contacto. Sin embargo, también está en constante cambio debido a que en muchos hospitales no hay 
teléfonos fijos, por lo que usan particulares. Todos los doctores pertenecen a alguna facultad del hospital, no necesariamente están todos fijos en alguna y estos pueden también
cambiar de hospital.
