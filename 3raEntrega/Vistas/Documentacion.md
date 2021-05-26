# Documentación de vistas

## Problema: Estado actual de los inventario de los hospitales

Sabemos que nuestros recursos no son ilimitados, por lo que los hospitales en situaciones de pandemia pueden llegar a encontrarse en situaciones de escasez en cuanto a ciertos artículos o equipos. Esto nos lleva a pensar que es importante tener presente la disponibilidad de cada artículo en el inventario de cada hospital. De esa forma, se podría saber que zonas o países necesitan recursos para poder seguir dando sus servicios de manera satisfactoria.

Para eso es necesario conocer el promedio de dias disponibles para cada uno de los artículos necesarios para el correcto funcionamiento. Esto es necesario hacerlo a nivel país y provincia con los promedios de los hospitales en cada región. Además tampoco está de más saber en promedio cómo se encuentra cada hospital individualmente, ya que nos indicará como ha estado trabajando ese hospital mes por mes.

### Promedio de inventario por país y por mes (Vista 1)

La vista primero obtiene las última actualización de inventario de cada hospital individualmente. Despúes trabaja con los inventarios (verificando igualdad de fechas) de esa fecha para obtener el promedio de cada uno de los artículos agrupándolos por el país y ordenándolos alfabéticamente.

La vista se crea de la manera siguiente:
```
create view needs_by_country as (
	with last_updates as(
		select i2.id_hospital, extract(year from i2.last_update) as año, extract(month from i2.last_update) as mes, max(i2.last_update) as max_update from inventory i2
		group by i2.id_hospital, extract(year from i2.last_update), extract(month from i2.last_update) order by i2.id_hospital asc)
	select lower(h.country), lu.año, lu.mes, avg(i.oxygen) as avg_oxygen, avg(i.antypiretic) as avg_antypiretic, avg(i.anesthesia) as avg_anesthesia, avg(i.soap_alcohol_solution) as avg_soap_alcohol, 
	avg(i.disposable_masks) as avg_disposable_masks, avg(i.disposable_gloves) as avg_disposable_gloves, avg(i.disposable_hats) as avg_disposable_hats, avg(i.disposable_aprons) as avg_disposable_aprons, avg(i.surgical_gloves) as avg_surgical_gloves, 
	avg(i.shoe_covers) as avg_shoe_covers, avg(i.visors) as avg_visors, avg(i.covid_test_kits) as avg_covid_test_kits
	from hospital h join inventory i using (id_hospital) join last_updates lu using (id_hospital)
	where i.last_update = lu.max_update
	group by lower(h.country), lu.año, lu.mes order by lower(h.country) asc);
```

Se llama de la siguiente forma:
```
select * from needs_by_country;
```

y se da de baja:
```
drop view needs_by_country;
```

### Promedio de inventario por provincia y por mes (Vista 2)

La vista sigue el mismo procedimiento que la de país, sin embargo, ahora el criterio de agrupación usado para los promedios es el de provincia. También se decidió seguir mostrando el país, ya que es bueno saber a donde pertenece cada provincia

La vista se crea de la manera siguiente:
```
create view needs_by_province as (
	with last_updates as(
		select i2.id_hospital, extract(year from i2.last_update) as año, extract(month from i2.last_update) as mes, max(i2.last_update) as max_update from inventory i2
		group by i2.id_hospital, extract(year from i2.last_update), extract(month from i2.last_update) order by i2.id_hospital asc)
	select lower(h.province), upper(h.country), lu.año, lu.mes, avg(i.oxygen) as avg_oxygen, avg(i.antypiretic) as avg_antypiretic, avg(i.anesthesia) as avg_anesthesia, avg(i.soap_alcohol_solution) as avg_soap_alcohol, 
	avg(i.disposable_masks) as avg_disposable_masks, avg(i.disposable_gloves) as avg_disposable_gloves, avg(i.disposable_hats) as avg_disposable_hats, avg(i.disposable_aprons) as avg_disposable_aprons, avg(i.surgical_gloves) as avg_surgical_gloves, 
	avg(i.shoe_covers) as avg_shoe_covers, avg(i.visors) as avg_visors, avg(i.covid_test_kits) as avg_covid_test_kits
	from hospital h join inventory i using (id_hospital) join last_updates lu using (id_hospital)
	where i.last_update = lu.max_update
	group by lower(h.province), upper(h.country), lu.año, lu.mes order by upper(h.country), lower(h.province) asc);
```

Se llama de la siguiente forma:
```
select * from needs_by_province;
```

y se da de baja:
```
drop view needs_by_province;
```

### Promedio de inventario por hospital (Vista 3)

Para está vista se cambio la dinámica previamente usada. Aquí queremos ver en promedio como ha estado trabajando el hospital según las actualizaciones recibidas mes por mes. Por esa razón, queremos obtener la última actualización de cada mes por hospital y de ahí obtener el promedio de cada uno. Además, es importante considerar agrupar por año, ya que en un futuro, con mayores waves, se empezarán a repetir los meses.

La vista se crea de la manera siguiente:
```
create view needs_by_hospital as (
	with last_updates as(
		select i2.id_hospital, extract(year from i2.last_update) as año, extract(month from i2.last_update) as mes, max(i2.last_update)  as max_update from inventory i2
		group by i2.id_hospital, extract(year from i2.last_update), extract(month from i2.last_update) order by i2.id_hospital asc)
	select h.name_hospital, lower(h.country), avg(i.oxygen) as avg_oxygen, avg(i.antypiretic) as avg_antypiretic, avg(i.anesthesia) as avg_anesthesia, avg(i.soap_alcohol_solution) as avg_soap_alcohol, 
	avg(i.disposable_masks) as avg_disposable_masks, avg(i.disposable_gloves) as avg_disposable_gloves, avg(i.disposable_hats) as avg_disposable_hats, avg(i.disposable_aprons) as avg_disposable_aprons, avg(i.surgical_gloves) as avg_surgical_gloves, 
	avg(i.shoe_covers) as avg_shoe_covers, avg(i.visors) as avg_visors, avg(i.covid_test_kits) as avg_covid_test_kits
	from hospital h join inventory i using (id_hospital) join last_updates lu using (id_hospital)
	where i.last_update = lu.max_update
	group by h.name_hospital, lower(h.country) order by lower(h.country), h.name_hospital asc);
```

Se llama de la siguiente forma:
```
select * from needs_by_hospital;
```

y se da de baja:
```
drop view needs_by_hospital;
```

## Problema: Implementación de protocolos COVID

Ante la situación actual, es importante que los hospitales se encuentren preparados no solo para tratar sino para evitar o controlar de cierta forma este virus. Para eso se han implementado técnicas y medidas que buscan cumplir con esos objetivos. Por esa razón, es importante saber los hospitales que aún no tienen estas medidas y hacer planes para hacerlas llegar a ellos lo más pronto posible. Este análiis puede ser muy variado, ya que se puede analizar que tengan todos los puntos, solo unos cuantos o realizarle operaciones estadísticas a los datos. Nosotros nos centraremos en las siguientes:

### Hospitales en situación de "gran riesgo" (Vista 4)

Nosotros consideramos que los hospitales están en gran riesgo si no han implementado ninguna de las medidas siguientes: campañas de prevención, capacidad de hacer pruebas, seguimiento regular a casos COVID y cribas de pacientes. Estos atributos, en general, muestran la preparación del hospital frente a la pandemia. Sin embargo, los criterios pueden cambiar y se puede considerar una situación de grave riesgo ante la falta de algún requisito, no todos necesariamente. Por eso, es fácil de editar para satisfacer los criterios propios. Teniendo esta información se pueden asistir a los hospitales correspondientes en su implementación.

La vista nos muestra los hospitales que tengan las medidas previamente mencionadas, las cuales son atributos booleanos, marcados como falso. Nos mostrará el nombre, país, nombre del contacto, teléfono y correo de los hospitales que se encuentren en esta situación. De la tabla de protocolos solo tomará en cuenta la última actualización recibida.

La vista se crea de la manera siguiente:
```
create view hosp_graves as (
	with hosp_protocol_last_update as (
		select h2.id_hospital, max(cp2.last_update) as protocol_max_update from hospital h2 join covid_protocol cp2 using (id_hospital)
		group by h2.id_hospital order by h2.id_hospital asc)
    select h.id_hospital, h.name_hospital, lower(h.country), p.p_name, p.phone, p.mail, cp.last_update as protocol_last_update
    from hospital h join hosp_protocol_last_update hp using (id_hospital) 
   	join covid_protocol cp using (id_hospital)
   	join contact c2 using (id_hospital) join person p using (id_contact)
    where screen_covid_patients = false and preventions_campaigns = false  and currently_ability_for_tests = false and track_regularly_cases = false
   	and cp.last_update = hp.protocol_max_update order by h.id_hospital asc);
```

Se llama de la siguiente forma:
```
select * from hosp_graves;
```

y se da de baja:
```
drop view hosp_graves;
```

### Porcentajes de medidas adoptadas por país y por mes (Vista 5)

De la misma forma resulta útil ver, por cada país, el porcentaje de implementación de cada una de las medidas o protocolos. De esa forma podríamos ver que necesita cada país y si se necesitan nuevas estrategias para hacer llegar ciertas medidas dada la particular situación de cada uno.

La vista como las demás obtiene las últimas actualizaciones que son las que realmente cuentan. Posteriormente realiza un conteo por cada medida si está tiene un valor verdadero. Para finalizar obtiene los porcentajes de cada una dividiendolo por el conteo sin ninguna condición y multiplicándolo por cien. Nos muestra el país con sus respectivos porcentajes en cada uno de los aspectos.

La vista se crea de la manera siguiente:
```
create view protocols_percentage_country as (
	with hosp_protocol_last_update as (
		select h2.id_hospital, h2.country, extract(year from cp2.last_update) as año, extract(month from cp2.last_update) as mes, max(cp2.last_update) as protocol_max_update from hospital h2 join covid_protocol cp2 using (id_hospital)
		group by h2.id_hospital, extract(year from cp2.last_update), extract(month from cp2.last_update)  order by h2.id_hospital asc)
	, counts_yes_country as (
		select lower(hp.country) as country, hp.año, hp.mes, count(case when cp3.currently_ability_for_tests = true then 1 else null end) as count_test_y, 
		count(case when cp3.preventions_campaigns = true then 1 else null end) as count_camp_y,
		count(case when cp3.screen_covid_patients = true then 1 else null end) as count_screen_y, 
		count(case when cp3.track_regularly_cases = true then 1 else null end) as count_track_y
		from covid_protocol cp3 join hosp_protocol_last_update hp using (id_hospital) 
		where cp3.last_update = hp.protocol_max_update
		group by lower(hp.country), hp.año, hp.mes order by lower(hp.country) asc)
	, counts_country as (
		select lower(hp.country) as country, count(cp4.currently_ability_for_tests) as count_test, count(cp4.preventions_campaigns) as count_camp,
		count(cp4.screen_covid_patients) as count_screen, count(cp4.track_regularly_cases) as count_track
		from covid_protocol cp4 join hosp_protocol_last_update hp using (id_hospital) 
		where cp4.last_update = hp.protocol_max_update
		group by lower(hp.country) order by lower(hp.country) asc)
   select cy.country, cy.año, cy.mes, (cy.count_test_y * 100)/cc.count_test as percentage_tests, (cy.count_camp_y * 100)/cc.count_camp as percentage_campaigns,
   (cy.count_screen_y * 100)/cc.count_screen as percentage_screen, (cy.count_track_y * 100)/cc.count_track as percentage_tracking
   from counts_yes_country cy join counts_country cc using (country));
```

Se llama de la siguiente forma:
```
select * from protocols_percentage_country;
```

y se da de baja:
```
drop view protocols_percentage_country;
```

## Número de staff médico y parámedico

De la misma forma que no tenemos un inventario ilimitado, en épocas de pandemia, el staff de un hospital no va a poder atender a todas las personas al mismo tiempo. Se ve a tener una disponibilidad limitada mientras más lleno se encuentre el hospital. Eso nos lleva a preguntarnos cuántos doctores y cuántos paramédicos se tienen en ciertas regiones.

### Staff por país (Vista 6)

La cantidad de personal médico por país es un índice importante a tomar en cuenta, en especial, porque nos da la idea de cómo es que está el sector de salud en el país. También, nos comparte la información, de cuantos personal médico hay para atender todos los casos de COVID-19 y con eso, poder corroborar, si el sistema de salud está colapsado o no. Con esa información, doctores de otros países pueden ser enviados como ayuda a los más necesitados.  

La vista se crea de la manera siguiente:
```
create view staff_by_country as (
	with last_updates as(
		select s2.id_hospital, extract(year from s2.last_update) as año, extract(month from s2.last_update) as mes, max(s2.last_update) as max_update from staff s2
		group by s2.id_hospital, extract(year from s2.last_update), extract(month from s2.last_update) order by s2.id_hospital asc)
	select lower(h.country), lu.año, lu.mes, sum(s.amount_of_doctors_in_hospital) as number_doctors, sum(s.amount_of_paramedical_staff_in_hospital) as number_paramedics
	from hospital h join staff s using (id_hospital) join last_updates lu using (id_hospital)
	where s.last_update = lu.max_update
	group by lower(h.country), lu.año, lu.mes order by lower(h.country) asc);
```

Se llama de la siguiente forma:
```
select * from staff_by_country;
```

y se da de baja:
```
drop view staff_by_country;
```

### Staff por provincia (Vista 7)

Al igual que analizamos los doctores por país, es importante analizar las provincias. Tanto entre países, como dentro de ellos, en sus provincias, va a existir una heterogeniedad que tenemos que tomar en cuenta. Ciertas áreas de un país pueden estar en peor situación que otras, y esto es importante saberlo.

La vista se crea de la manera siguiente:
```
create view staff_by_province as (
	with last_updates as (
		select s2.id_hospital, extract(year from s2.last_update) as año, extract(month from s2.last_update) as mes, max(s2.last_update) as max_update from staff s2
		group by s2.id_hospital, extract(year from s2.last_update), extract(month from s2.last_update) order by s2.id_hospital asc)
	select lower(h.province), upper(h.country), lu.año, lu.mes, sum(s.amount_of_doctors_in_hospital) as number_doctors, sum(s.amount_of_paramedical_staff_in_hospital) as number_paramedics
	from hospital h join staff s using (id_hospital) join last_updates lu using (id_hospital)
	where s.last_update = lu.max_update
	group by lower(h.province), upper(h.country), lu.año, lu.mes order by upper(h.country) asc);
```

Se llama de la siguiente forma:
```
select * from staff_by_province;
```

y se da de baja:
```
drop view staff_by_province;
```

## Afectación del COVID

### Número de positivos por país y por provincia
