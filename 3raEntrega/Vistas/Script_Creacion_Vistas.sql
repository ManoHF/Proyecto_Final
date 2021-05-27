-- Creacion de vistas
	
-- Problema: medias de needs en inventario

--Vista: needs por pais
create view needs_by_country as (
	with last_updates as(
		select i2.id_hospital, extract(year from i2.last_update) as año, extract(month from i2.last_update) as mes,
		max(i2.last_update) as max_update from inventory i2
		group by i2.id_hospital, extract(year from i2.last_update), extract(month from i2.last_update)
		order by i2.id_hospital asc
		)
	select c.country, lu.año, lu.mes, avg(i.oxygen) as avg_oxygen, avg(i.antypiretic) as avg_antypiretic, 
	avg(i.anesthesia) as avg_anesthesia, avg(i.soap_alcohol_solution) as avg_soap_alcohol,avg(i.disposable_masks) as avg_disposable_masks,
	avg(i.disposable_gloves) as avg_disposable_gloves, avg(i.disposable_hats) as avg_disposable_hats, avg(i.disposable_aprons) as avg_disposable_aprons,
	avg(i.surgical_gloves) as avg_surgical_gloves,avg(i.shoe_covers) as avg_shoe_covers, avg(i.visors) as avg_visors,avg(i.covid_test_kits) as avg_covid_test_kits
	from hospital h join inventory i using (id_hospital) join last_updates lu using (id_hospital)
	join country c using (id_country)
	where i.last_update = lu.max_update
	group by c.country, lu.año, lu.mes 
	order by c.country asc);

--Vista: needs por provincia
create view needs_by_province as (
	with last_updates as(
		select i2.id_hospital, extract(year from i2.last_update) as año, extract(month from i2.last_update) as mes, 
		max(i2.last_update) as max_update from inventory i2
		group by i2.id_hospital, extract(year from i2.last_update), extract(month from i2.last_update) 
		order by i2.id_hospital asc
		)
	select p.province, c.country, lu.año, lu.mes, avg(i.oxygen) as avg_oxygen, avg(i.antypiretic) as avg_antypiretic, 
	avg(i.anesthesia) as avg_anesthesia, avg(i.soap_alcohol_solution) as avg_soap_alcohol, avg(i.disposable_masks) as avg_disposable_masks,
	avg(i.disposable_gloves) as avg_disposable_gloves, avg(i.disposable_hats) as avg_disposable_hats, avg(i.disposable_aprons) as avg_disposable_aprons,
	avg(i.surgical_gloves) as avg_surgical_gloves, avg(i.shoe_covers) as avg_shoe_covers, avg(i.visors) as avg_visors, avg(i.covid_test_kits) as avg_covid_test_kits
	from hospital h join inventory i using (id_hospital) join last_updates lu using (id_hospital)
	join country c using (id_country) join province p using(id_province)
	where i.last_update = lu.max_update 
	group by p.province, c.country, lu.año, lu.mes 
	order by c.country, p.province asc);

-- Vista: needs por hospital
create view needs_by_hospital as (
	with last_updates as(
		select i2.id_hospital, extract(year from i2.last_update) as año, extract(month from i2.last_update) as mes, 
		max(i2.last_update)  as max_update from inventory i2
		group by i2.id_hospital, extract(year from i2.last_update), extract(month from i2.last_update) 
		order by i2.id_hospital asc
		)
	select h.name_hospital, c.country, avg(i.oxygen) as avg_oxygen, avg(i.antypiretic) as avg_antypiretic, avg(i.anesthesia) as avg_anesthesia,
	avg(i.soap_alcohol_solution) as avg_soap_alcohol, avg(i.disposable_masks) as avg_disposable_masks, avg(i.disposable_gloves) as avg_disposable_gloves,
	avg(i.disposable_hats) as avg_disposable_hats, avg(i.disposable_aprons) as avg_disposable_aprons, avg(i.surgical_gloves) as avg_surgical_gloves, 
	avg(i.shoe_covers) as avg_shoe_covers, avg(i.visors) as avg_visors, avg(i.covid_test_kits) as avg_covid_test_kits
	from hospital h join inventory i using (id_hospital) join last_updates lu using (id_hospital)
	join country c using (id_country) 
	where i.last_update = lu.max_update
	group by h.name_hospital, c.country 
	order by c.country, h.name_hospital asc);

-- Problema: implementacion de protocolos COVID

-- Vista: hospitales sin protocolos
create view hosp_graves as (
	with hosp_protocol_last_update as (
		select h2.id_hospital, max(cp2.last_update) as protocol_max_update from hospital h2 join covid_protocol cp2 using (id_hospital)
		group by h2.id_hospital 
		order by h2.id_hospital asc
		)
    select h.id_hospital, h.name_hospital, c3.country, p.p_name, p.phone, p.mail, cp.last_update as protocol_last_update
    from hospital h join hosp_protocol_last_update hp using (id_hospital) 
   	join covid_protocol cp using (id_hospital)
   	join contact c2 using (id_hospital) join person p using (id_contact)
   	join country c3 using (id_country)
    where screen_covid_patients = false and preventions_campaigns = false  and currently_ability_for_tests = false and track_regularly_cases = false
   	and cp.last_update = hp.protocol_max_update 
   	order by h.id_hospital asc);
   
-- Vista: porcentaje de implementacion de medidas
create view protocols_percentage_country as (
	with hosp_protocol_last_update as (
		select h2.id_hospital, c.country, extract(year from cp2.last_update) as año, extract(month from cp2.last_update) as mes,
		max(cp2.last_update) as protocol_max_update from hospital h2 join covid_protocol cp2 using (id_hospital)
		join country c using (id_country)
		group by h2.id_hospital, extract(year from cp2.last_update), extract(month from cp2.last_update),c.country  
		order by h2.id_hospital asc
		), counts_yes_country as (
		select hp.country as country, hp.año, hp.mes, count(case when cp3.currently_ability_for_tests = true then 1 else null end) as count_test_y, 
		count(case when cp3.preventions_campaigns = true then 1 else null end) as count_camp_y,
		count(case when cp3.screen_covid_patients = true then 1 else null end) as count_screen_y, 
		count(case when cp3.track_regularly_cases = true then 1 else null end) as count_track_y
		from covid_protocol cp3 join hosp_protocol_last_update hp using (id_hospital) 
		where cp3.last_update = hp.protocol_max_update
		group by hp.country, hp.año, hp.mes 
		order by hp.country asc
		), counts_country as (
		select hp.country as country, count(cp4.currently_ability_for_tests) as count_test, count(cp4.preventions_campaigns) as count_camp,
		count(cp4.screen_covid_patients) as count_screen, count(cp4.track_regularly_cases) as count_track
		from covid_protocol cp4 join hosp_protocol_last_update hp using (id_hospital) 
		where cp4.last_update = hp.protocol_max_update
		group by hp.country 
		order by hp.country asc
		)
   select cy.country, cy.año, cy.mes, (cy.count_test_y * 100)/cc.count_test as percentage_tests, (cy.count_camp_y * 100)/cc.count_camp as percentage_campaigns,
   (cy.count_screen_y * 100)/cc.count_screen as percentage_screen, (cy.count_track_y * 100)/cc.count_track as percentage_tracking
   from counts_yes_country cy join counts_country cc using (country));
   
--Numero de staff: medico y paramedico
  
-- Vista: staff por pais
create view staff_by_country as (
	with last_updates as(
		select s2.id_hospital, extract(year from s2.last_update) as año, extract(month from s2.last_update) as mes, 
		max(s2.last_update) as max_update from staff s2
		group by s2.id_hospital, extract(year from s2.last_update), extract(month from s2.last_update) 
		order by s2.id_hospital asc
		)
	select c.country, lu.año, lu.mes, sum(s.amount_of_doctors_in_hospital) as number_doctors, sum(s.amount_of_paramedical_staff_in_hospital) as number_paramedics
	from hospital h join staff s using (id_hospital) join last_updates lu using (id_hospital)
	join country c using (id_country)
	where s.last_update = lu.max_update
	group by c.country, lu.año, lu.mes 
	order by c.country asc);
  
-- Vista: staff por provincia
create view staff_by_province as (
	with last_updates as (
		select s2.id_hospital, extract(year from s2.last_update) as año, extract(month from s2.last_update) as mes, 
		max(s2.last_update) as max_update from staff s2
		group by s2.id_hospital, extract(year from s2.last_update), extract(month from s2.last_update) 
		order by s2.id_hospital asc
		)
	select p.province, c.country, lu.año, lu.mes, sum(s.amount_of_doctors_in_hospital) as number_doctors, 
	sum(s.amount_of_paramedical_staff_in_hospital) as number_paramedics
	from hospital h join staff s using (id_hospital) join last_updates lu using (id_hospital)
	join country c using (id_country) join province p using (id_province)
	where s.last_update = lu.max_update
	group by p.province, c.country, lu.año, lu.mes 
	order by c.country asc);

-- Problema: afectacion del COVID en países y provincias

-- Vista: muertes por pais y provincia
create view covid_stats as (
	with last_updates as (
		select ps.id_hospital, p.province as province, c.country as country, extract(year from ps.last_update) as año,
		extract(month from ps.last_update) as mes, max(ps.last_update) as max_update 
		from patient_statistics ps join hospital h2 using (id_hospital)
		join country c using (id_country) join province p using (id_province)
		group by ps.id_hospital, extract(year from ps.last_update), extract(month from ps.last_update), p.province, c.country 
		order by ps.id_hospital asc
		)
	select lu.province, lu.country, lu.año, lu.mes, sum(ps2.amount_last_month_deaths_by_covid) as covid_deaths, sum(ps2.amount_last_month_recovered_from_covid) as covid_recovered,
	sum(ps2.amount_last_month_tested_positive_covid) as covid_positive, sum(ps2.amount_last_month_in_intensive_care_wcovid) as covid_intensive_care
	from hospital h join patient_statistics ps2 using (id_hospital)
	join last_updates lu using (id_hospital) where ps2.last_update = lu.max_update
	group by rollup (lu.country, lu.province, lu.año, lu.mes) 
	order by country asc);

-- Vista: comparacion de muertes covid y no covid por pais
create view deaths_stats_country as(
with last_updates as(
		select ps.id_hospital, extract(year from ps.last_update) as año, extract(month from ps.last_update) as mes,
		max(ps.last_update) as max_update from patient_statistics ps
		group by ps.id_hospital, extract(year from ps.last_update), extract(month from ps.last_update)
		order by ps.id_hospital asc
		)
	select c.country, lu.año as years, lu.mes as months, sum(ps.amount_last_month_deaths_by_covid) as number_of_deaths_by_covid,
	sum(ps.amount_last_month_deaths_non_covid) as number_of_deaths_non_covid, (sum(ps.amount_last_month_deaths_by_covid) - sum(ps.amount_last_month_deaths_non_covid)) as diference
	from hospital h join patient_statistics ps using (id_hospital) join last_updates lu using (id_hospital)
	join country c using (id_country)
	where ps.last_update = lu.max_update
	group by c.country, lu.año, lu.mes 
	order by c.country asc);

-- Vista: comparacion de muertes covid y no covid por provincia
create view deaths_stats_by_hospital as(
with last_updates as(
		select ps.id_hospital, extract(year from ps.last_update) as año, extract(month from ps.last_update) as mes,
		max(ps.last_update) as max_update from patient_statistics ps
		group by ps.id_hospital, extract(year from ps.last_update), extract(month from ps.last_update)
		order by ps.id_hospital asc
		)
	select p.province, c.country, h.name_hospital, lu.año as years, lu.mes as months, sum(ps.amount_last_month_deaths_by_covid) as number_of_deaths_by_covid,
	sum(ps.amount_last_month_deaths_non_covid) as number_of_deaths_non_covid, ((ps.amount_last_month_deaths_by_covid*100)/(ps.amount_last_month_deaths_by_covid+ps.amount_last_month_deaths_non_covid)) 
	as percentage_of_covid_deaths from hospital h join patient_statistics ps using (id_hospital) join last_updates lu using (id_hospital)
	join country c using (id_country) join province p using (id_province)
	where ps.last_update = lu.max_update
	group by p.province,c.country, h.name_hospital, lu.año, lu.mes,((ps.amount_last_month_deaths_by_covid*100)/(ps.amount_last_month_deaths_by_covid+ps.amount_last_month_deaths_non_covid)) 
	order by c.country asc);







