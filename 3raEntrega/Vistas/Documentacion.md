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

## Vista 2: Inventarios por hospital

Sabemos que nuestros recursos no son ilimitados, por lo que los hospitales en situaciones de pandemia pueden llegar a encontrarse en situaciones de escasez en cuanto a ciertos artículos o equipos. Esto nos lleva a pensar que es importante tener presente la disponibilidad de cada artículo en el inventario de cada hospital. De esa forma, se podría saber que zonas o países necesitan recursos para poder seguir dando sus servicios de manera satisfactoria.

La vista nos muestra el inventario (en otras palabras, sus atributos) de todos los hospitales de Kenya (lógicamente se puede aplicar a cualquier país). Se muestra el nombre del hospital, provincia, distrito, todos los materiales con sus cantidades, y la fecha de su última actualización.

La vista se crea de la manera siguiente:
```
create view inventario_Kenya as
	with hosp_inventory_last_update as (
		select h2.id_hospital, max(i2.last_update) as inventory_max_update from hospital h2 join inventory i2 using (id_hospital)
		group by h2.id_hospital order by h2.id_hospital asc)
	select h.id_hospital, h.name_hospital, h.district , h.province, i.oxygen, i.antypiretic, i.anesthesia, i.soap_alcohol_solution, i.disposable_masks, i.disposable_gloves, i.disposable_hats, i.disposable_aprons, i.surgical_gloves, i.shoe_covers, i.visors, i.covid_test_kits, i.last_update as ultima_actualizacion
	from hospital h join hosp_inventory_last_update hi using (id_hospital)
    join inventory i using (id_hospital)
	where lower(country)='kenya' and i.last_update = hi.inventory_max_update order by h.id_hospital asc;
```

Se llama de la siguiente forma:
```
select * from inventario_Kenya;
```

y se da de baja:
```
drop view inventario_Kenya;
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

## Vista N:
