# Documentación de vistas

## Vista 1: Hospitales sin protocolos

Ante la situación actual, es importante que los hospitales se encuentren preparados no solo para tratar sino para evitar o controlar de cierta forma este virus. Para eso se han implementado técnicas y medidas que buscan cumplir con esos objetivos. Por esa razón, es importante saber los hospitales que aún no tienen estas medidas y hacer planes para hacerlas llegar a ellos lo más pronto posible.

La vista nos muestra hospitales en situación grave frente a la pandemia, ya que no tiene implementadas: campañas de prevención, capacidad de hacer pruebas, un seguimiento regular de casos de COVID y cribas de pacientes. Por lo mismo tendrán todos estos atributos booleanos marcados como falso. Nos mostrará el nombre, país, nombre del contacto, teléfono y correo de los hospitales que se encuentren en esta situación. De la tabla de protocolos solo tomará en cuenta la última actualización recibida.

La vista se crea de la manera siguiente:
```
create view hosp_graves as
	with hosp_protocol_last_update as (
		select h2.id_hospital, max(cp2.last_update) as protocol_max_update from hospital h2 join covid_protocol cp2 using (id_hospital)
		group by h2.id_hospital order by h2.id_hospital asc)
    select h.id_hospital, h.name_hospital, h.country, p.p_name, p.phone, p.mail, cp.last_update as protocol_last_update
    from hospital h join hosp_protocol_last_update hp using (id_hospital) 
   	join covid_protocol cp using (id_hospital)
   	join contact c2 using (id_hospital) join person p using (id_contact)
    where screen_covid_patients = false and preventions_campaigns = false  and currently_ability_for_tests = false and track_regularly_cases = false
   	and cp.last_update = hp.protocol_max_update order by h.id_hospital asc;
```

Se llama de la siguiente forma:
```
select * from hosp_graves;
```

y se da de baja:
```
drop view hosp_graves;
```

## Vista 3: Recuperados de COVID

La cantidad de recuperados es un dato clave, para así poder conocer el estado actual del país ante los infectados. Con esta información, se puede conocer la tasa de reacción de los pacientes ante la enfermedad. Además, de poder observar cómo es el proceso de recuperación, su tiempo y su efectividad.

La vista nos muestra la cantidad de pacientes, los cuales se recuperaron del COVID-19, organizándolo por mes y por país. Es decir, la vista nos va a proporcionar país, cantidad de recuperados y fecha en la que se registraron estos datos. De igual forma queremos saber el estado actual del inventario, por lo que se toma la última actualización.

La vista se crea de la manera siguiente:
```
create view recovered_by_covid as(
with recovered_by_covid as (
	select id_hospital as id_hosp, country as countrys from hospital
	order by country asc
), sec as(
	select ps.amount_last_month_recovered_from_covid as count_cases, rc.id_hosp as id, lower(rc.countrys) as countryy,
	ps.last_update - extract(day from ps.last_update)*'1 day'::interval + '1 day'::interval as star_date_month, 
	ps.last_update - extract(day from ps.last_update)*'1 day'::interval + '1 day'::interval + 30*'1 day':: interval as month_end_date,
	extract(month from ps.last_update)
	from recovered_by_covid rc 
	join patient_statistics ps on (rc.id_hosp = ps.id_hospital)
	window w as (partition by extract(month from ps.last_update), lower(rc.countrys) order by rc.countrys)
	order by rc.countrys asc 
)
	select sum(s.count_cases) as recovered_by_covid, upper(s.countryy) as country, s.star_date_month, s.month_end_date
	from sec s
	group by s.countryy, s.star_date_month, s.month_end_date
	order by s.countryy);
```

Se llama de la siguiente forma:
```
select * from recovered_by_covid;
```

y se da de baja:
```
drop view recovered_by_covid;
```

## Vista 4:

Es muy importante conocer la cantidad de pacientes a los que se les ha detectado el virus, a través de los meses, esto es para así poder conocer la reincidencia que las personas tienen en el país. A mayor casos detectados, mayor reincidencia en el país, o por el contrario, si en el estudio de los meses se detecta menos casos cada mes, entonces, menor reincidencia. 

La vista nos muestra la cantidad de pacientes, a los cuales se les detectó el COVID-19, organizándolo por mes y por país. Es decir, la vista nos va a proporcionar país, cantidad de testeados positivos y fecha en la que se registraron estos datos.

```
create view tested_positive_to_covid as(
with tested_positive_to_covid as (
	select id_hospital as id_hosp, country as countrys from hospital
	order by country asc
), sec as(
	select ps.amount_last_month_tested_positive_covid as count_cases, tc.id_hosp as id, lower(tc.countrys) as countryy,
	ps.last_update - extract(day from ps.last_update)*'1 day'::interval + '1 day'::interval as star_date_month, 
	ps.last_update - extract(day from ps.last_update)*'1 day'::interval + '1 day'::interval + 30*'1 day':: interval as month_end_date,
	extract(month from ps.last_update)
	from tested_positive_to_covid tc 
	join patient_statistics ps on (tc.id_hosp = ps.id_hospital)
	window w as (partition by extract(month from ps.last_update), lower(tc.countrys) order by tc.countrys)
	order by tc.countrys asc 
)
	select sum(s.count_cases) as tested_positive_to_covid, upper(s.countryy) as country, s.star_date_month, s.month_end_date
	from sec s
	group by s.countryy, s.star_date_month, s.month_end_date
	order by s.countryy);
```

Se llama de la siguiente forma:
```
select * from tested_positive_to_covid;
```

y se da de baja:
```
drop view tested_positive_to_covid;
```

## Problema: Estado actual de los inventario de los hospitales

Sabemos que nuestros recursos no son ilimitados, por lo que los hospitales en situaciones de pandemia pueden llegar a encontrarse en situaciones de escasez en cuanto a ciertos artículos o equipos. Esto nos lleva a pensar que es importante tener presente la disponibilidad de cada artículo en el inventario de cada hospital. De esa forma, se podría saber que zonas o países necesitan recursos para poder seguir dando sus servicios de manera satisfactoria.

Para eso es necesario conocer el promedio de dias disponibles para cada uno de los artículos necesarios para el correcto funcionamiento. Esto es necesario hacerlo a nivel país y provincia con los promedios de los hospitales en cada región. Además tampoco está de más saber en promedio cómo se encuentra cada hospital individualmente, ya que nos indicará como ha estado trabajando ese hospital mes por mes.

### Promedio de inventario por país

La vista primero obtiene las última actualización de inventario de cada hospital individualmente. Despúes trabaja con los inventarios (verificando igualdad de fechas) de esa fecha para obtener el promedio de cada uno de los artículos agrupándolos por el país y ordenándolos alfabéticamente.

La vista se crea de la manera siguiente:
```
create view needs_by_country as (
	with last_updates as(
		select i2.id_hospital, max(i2.last_update) as max_update from inventory i2
		group by i2.id_hospital order by i2.id_hospital asc)
	select h.country, avg(i.oxygen) as avg_oxygen, avg(i.antypiretic) as avg_antypiretic, avg(i.anesthesia) as avg_anesthesia, avg(i.soap_alcohol_solution) as avg_soap_alcohol, 
	avg(i.disposable_masks) as avg_disposable_masks, avg(i.disposable_gloves) as avg_disposable_gloves, avg(i.disposable_hats) as avg_disposable_hats, avg(i.disposable_aprons) as avg_disposable_aprons, avg(i.surgical_gloves) as avg_surgical_gloves, 
	avg(i.shoe_covers) as avg_shoe_covers, avg(i.visors) as avg_visors, avg(i.covid_test_kits) as avg_covid_test_kits
	from hospital h join inventory i using (id_hospital) join last_updates lu using (id_hospital)
	where i.last_update = lu.max_update
	group by h.country order by h.country asc);
```

Se llama de la siguiente forma:
```
select * from needs_by_country;
```

y se da de baja:
```
drop view needs_by_country;
```

### Promedio de inventario por provincia

La vista sigue el mismo procedimiento que la de país, sin embargo, ahora el criterio de agrupación usado para los promedios es el de provincia. También se decidió seguir mostrando el país, ya que es bueno saber a donde pertenece cada provincia

La vista se crea de la manera siguiente:
```
create view needs_by_province as (
	with last_updates as(
		select i2.id_hospital, max(i2.last_update) as max_update from inventory i2
		group by i2.id_hospital order by i2.id_hospital asc)
	select h.province, h.country , avg(i.oxygen) as avg_oxygen, avg(i.antypiretic) as avg_antypiretic, avg(i.anesthesia) as avg_anesthesia, avg(i.soap_alcohol_solution) as avg_soap_alcohol, 
	avg(i.disposable_masks) as avg_disposable_masks, avg(i.disposable_gloves) as avg_disposable_gloves, avg(i.disposable_hats) as avg_disposable_hats, avg(i.disposable_aprons) as avg_disposable_aprons, avg(i.surgical_gloves) as avg_surgical_gloves, 
	avg(i.shoe_covers) as avg_shoe_covers, avg(i.visors) as avg_visors, avg(i.covid_test_kits) as avg_covid_test_kits
	from hospital h join inventory i using (id_hospital) join last_updates lu using (id_hospital)
	where i.last_update = lu.max_update
	group by h.province, h.country order by h.country, h.province asc);
```

Se llama de la siguiente forma:
```
select * from needs_by_province;
```

y se da de baja:
```
drop view needs_by_province;
```

### Promedio de inventario por hospital

Para está vista se cambio la dinámica previamente usada. Aquí queremos ver en promedio como ha estado trabajando el hospital según las actualizaciones recibidas mes por mes. Por esa razón, queremos obtener la última actualización de cada mes por hospital y de ahí obtener el promedio de cada uno. Además, es importante considerar agrupar por año, ya que en un futuro, con mayores waves, se empezarán a repetir los meses.

La vista se crea de la manera siguiente:
```
create view needs_by_hospital as (
	with last_updates as(
		select i2.id_hospital, extract(year from i2.last_update) as año, extract(month from i2.last_update) as mes, max(i2.last_update)  as max_update from inventory i2
		group by i2.id_hospital, extract(year from i2.last_update), extract(month from i2.last_update) order by i2.id_hospital asc)
	select h.name_hospital, h.country, avg(i.oxygen) as avg_oxygen, avg(i.antypiretic) as avg_antypiretic, avg(i.anesthesia) as avg_anesthesia, avg(i.soap_alcohol_solution) as avg_soap_alcohol, 
	avg(i.disposable_masks) as avg_disposable_masks, avg(i.disposable_gloves) as avg_disposable_gloves, avg(i.disposable_hats) as avg_disposable_hats, avg(i.disposable_aprons) as avg_disposable_aprons, avg(i.surgical_gloves) as avg_surgical_gloves, 
	avg(i.shoe_covers) as avg_shoe_covers, avg(i.visors) as avg_visors, avg(i.covid_test_kits) as avg_covid_test_kits
	from hospital h join inventory i using (id_hospital) join last_updates lu using (id_hospital)
	where i.last_update = lu.max_update
	group by h.name_hospital, h.country order by h.country, h.name_hospital asc);
```

Se llama de la siguiente forma:
```
select * from needs_by_hospital;
```

y se da de baja:
```
drop view needs_by_hospital;
```
