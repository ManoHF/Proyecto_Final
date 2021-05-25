# Documentación de vistas

## Vista 1: Hospitales sin protocolos

Ante la situación actual, es importante que los hospitales se encuentren preparados no solo para tratar sino para evitar o controlar de cierta forma este virus. Para eso se han implementado técnicas y medidas que buscan cumplir con esos objetivos. Por esa razón, es importante saber los hospitales que aún no tienen estas medidas y hacer planes para hacerlas llegar a ellos lo más pronto posible.

La vista nos muestra hospitales en situación grave frente a la pandemia, ya que no tiene implementadas: campañas de prevención, capacidad de hacer pruebas, un seguimiento regular de casos de COVID y cribas de pacientes. Por lo mismo tendrán todos estos atributos booleanos marcados como falso. Nos mostrará el nombre, país, nombre del contacto, teléfono y correo de los hospitales que se encuentren en esta situación.

La vista se crea de la manera siguiente:
```
create view hosp_graves as
    select name_hospital, country, p_name, phone, mail
    from hospital  
   	 join covid_protocol cp using (id_hospital)
   	 join contact c2 using (id_hospital) join person p using (id_contact)
    where screen_covid_patients = false and preventions_campaigns = false  and currently_ability_for_tests = false and track_regularly_cases =false;
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
select name_hospital, h.district , h.province, oxygen, antypiretic, anesthesia, soap_alcohol_solution, disposable_masks, disposable_gloves, disposable_hats, disposable_aprons, surgical_gloves, shoe_covers, visors, covid_test_kits, h.last_update as ultima_actualizacion
from hospital h
    join inventory i using (id_hospital)
where lower(country)='kenya';
```

Se llama de la siguiente forma:
```
select * from inventario_Kenya;
```

y se da de baja:
```
drop view inventario_Kenya;
```

## Vista N:
