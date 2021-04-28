# User Story: VOXMAPP

## Problem Domain

Voxmapp es una empresa social que busca mejorar la governancia mediante el planteamiento de tecnologías y recolección de datos. La empresa VOXMAPP quiere mejorar la recolección, análisis y presentación de datos mediante la automatización de estos procesos. Esta mejora tiene el objetivo de acortar el "time-to-decision". Esto es sumamente importante, ya que VOXMAPP trabaja principalmente en medio oriente, donde muchos países no se encuentran dentro de las mejores circunstancias.

En nuestro caso, nos centraremos en la recolección de datos sobre hospitales relacionados con el COVID-19. Dada la grave situación actual de esta pandemia, es muy valioso tener un proceso de recolección de información que sea amigable para los entrevistados, rápido, pero que nos dé información utilizable y sustancial de la situación actual de cada hospital. El trabajo se puede dividir en tres partes principales: captura de información y proceso hacia la base de datos, limpieza de los datos, y análisis y presentación de los datos.

Para realizar la captura de información se utiliza una entrevista dividida en varias partes. Esta entrevista tiene que estar redactada y limpia de tal manera que tanto el entrevistado como el usuario que la realiza, la pueden utilizar de la manera más eficiente. Además, se tiene que crear una base de datos que reciba la información recolectada en la entrevista.

Sin embargo, la inserción de los datos no es tan sencilla, por eso la segunda parte incluye su limpieza. En los llenados de entrevistas, por lo general, ocurren muchos errores humanos que pueden alterar la información. Dentro de estos errores podemos ver malos formatos de teléfonos, espacios en blanco, o información incorrecta. Ésto tiene que ser corregido en el proceso hacia la base de datos o se tiene que evitar dándole cierto formato a la entrevista.

Para finalizar es necesario realizar el análisis y la presentación de los datos. Esto es muy importante, ya que Voxmapp tiene objetivos cívicos, por lo que tener la información nada más guardada no sirve de nada. Es necesario diseñar la base de datos de manera que podamos obtener vistas sustanciosas y relevantes para sus tablas. Mediante estas vistas presentadas, en herramientas como Tableau, para que otras personas puedan ver la información.

## User Story

Sabemos que la empresa tiene un registro de hospitales a los cuales entrevista. La empresa lo denomina como "base line" y son datos estáticos de cada hospital entre los cuales
están incluidos: ID único, datos geográficos (latitud, longitud, altitud), nombre, distrito, provincia y tipo de hospital (estatal, distrito, ...). A este registro, posteriormente, se le van agregando otros hospitales.
* Un hospital puede tener múltiples teléfonos, pero estarán en constante cambio, ya que en muchos hospitales no hay teléfonos fijos, por lo que usan particulares.
* Un hospital tiene múltiples doctores asociados a alguna facultad del hospital; no son necesariamente fijos de alguna y pueden cambiar de hospital

A cada una de estos hospitales se les realizará una entrevista mensual, la cual se reflejara como un "update" en nuestra base de datos. Cada uno de estos updates será registrado por un usuario de Voxmapp y constestado por algún contacto del hospital del cual se recuperarán sus datos de contacto.

Un hospital va a tener múltiples cuestionarios, ya que busca dar un monitoreo por mes de cada uno, por lo que habría un registro mes por mes. Cada una de esas entrevistas tiene asociado un usuario encargado de hacer la baseline y un contacto que contestó la entrevista. Ambos pueden ser distintos cada mes. El cuestionario pueden recibirse en tres estados: completo, parcialmente completo, y no completo. Se pueden observar tanto cuestionarios "limpios" como "sucios". Hay distintas razones por las cuales un cuestionario esté incompleto. Dependiendo de la razón, se le asigna una acción correspondiente para pasar el estado a cuestionario completo.
 * Existe una distinción entre hospitales con entrevistas recolectadas y los que faltan por recolectar
 * Los hospitales poseen un almacén de artículos médicos específicos para COVID, dentro de él se encuentran cosas como reservas de oxígeno, paracetamos, máscaras, tests de COVID, entre otros
 * Dentro de la maquinaria de los hospitales se posee un número de ventiladores activos/inactivos y funcionantes/no-funcionantes
 * Los hospitales pueden o no tener un protocolo de cribado de COVID a la llegada de nuevos pacientes

Cabe añadir que toda estos artículos, materiales y equipos tienen un registro que nos indica cuántos días con reservas tiene cada uno. Se pueden tener reservas por 30, 15, 7 o
ningún día. Esto nos indica con cuántos recursos está trabajando el hospital.

Los pacientes pueden haber testeado positivo o negativo en la prueba de COVID. No necesariamente llegaron todos al hospital por síntomas de COVID. Las defunciones pueden ser por 
COVID u otras causas. Existe un número de pacientes recuperados del COVID.
