--Creación de BD VOXMAPP--

--Creación de la tabla Hospital--
create table hospital(
	id_hospital serial not null, 
	latitude varchar (30) not null,
	lengths varchar (30) not null,
	altitude varchar (30) not null,
	name_hospital varchar(200) not null,
	district varchar(200) not null,
	province varchar(200) not null,
	country varchar(200) not null,
	type_of_hospital varchar(200),
	last_update timestamp NOT NULL DEFAULT now(),
	CONSTRAINT hospital_pkey PRIMARY KEY (id_hospital)
);

--Creación de la tabla user--
create table users(
id_users serial not null,
u_name varchar(200) not null,
u_position varchar(200) not null,
phone bigint not null,
mail varchar(50) not null,
last_update timestamp not null default now(),
constraint users_pkey primary key (id_user)
);

--Creación de tabla Interview--
create table interview(
	id_interview serial not null,
	id_hospital int4 not null,
	id_user int4 not null,
	moph int default null,
	status varchar(100) not null,
	problems varchar(100),
	actions varchar(100),
	last_update timestamp NOT NULL DEFAULT now(),
	CONSTRAINT interview_pkey PRIMARY KEY (id_interview),
	CONSTRAINT interview_id_hospital_fkey FOREIGN KEY (id_hospital) REFERENCES hospital(id_hospital) ON UPDATE CASCADE ON DELETE restrict,
	CONSTRAINT interview_id_users_fkey FOREIGN KEY (id_users) REFERENCES users(id_users) ON UPDATE CASCADE ON DELETE RESTRICT
);

--Creación de la tabla teléfono--
create table telephone(
	id_telephone serial not null,
	id_hospital int4 not null,
	lada integer not null,
	phone_numb bigint not null,
	last_update timestamp not null default now(),
	constraint telephone_pkey primary key (id_telephone),
	CONSTRAINT telephone_id_hospital_fkey FOREIGN KEY (id_hospital) REFERENCES hospital(id_hospital) ON UPDATE CASCADE ON DELETE RESTRICT
);

--Creación de la tabla contacto--
create table contact(
	id_contact serial not null,
	id_hospital int4 not null,
	last_update timestamp not null default now(),
	constraint contact_pkey primary key (id_contact),
	CONSTRAINT contact_id_hospital_fkey FOREIGN KEY (id_hospital) REFERENCES hospital(id_hospital) ON UPDATE CASCADE ON DELETE RESTRICT
);

--Creación de la tabla persona--
create table person( 
	id_person serial not null,
	id_contact int4 not null,
	p_name varchar (200) not null,
	p_position varchar(200) not null,
	phone bigint not null,
	mail varchar(200) not null,
	last_update timestamp not null default now(),
	constraint person_pkey primary key (id_person),
	CONSTRAINT person_id_contact_fkey FOREIGN KEY (id_contact) REFERENCES contact(id_contact) ON UPDATE CASCADE ON DELETE RESTRICT
);

--Creación de la tabla inventario--
create table inventory(
	id_inventory serial not null,
	id_hospital int4 not null,
	oxygen int,
	antypiretic int,
	anesthesia int,
	soap_alcohol_solution int,
	disposable_masks int,
	disposable_gloves int,
	disposable_hats int,
	disposable_aprons int,
	surgical_gloves int,
	shoe_covers int,
	visors int,
	covid_test_kits int,
	last_update timestamp not null default now(),
	constraint inventory_pkey primary key (id_inventory),
	CONSTRAINT inventory_id_hospital_fkey FOREIGN KEY (id_hospital) references hospital(id_hospital) ON UPDATE CASCADE ON DELETE RESTRICT
);

--Creación de la tabla staff--
create table staff(
	id_staff serial not null, 
	id_hospital int4 not null,
	amount_of_doctors_in_hospital varchar(20),
	amount_of_paramedical_staff_in_hospital varchar(20),
	last_update timestamp NOT NULL DEFAULT now(),
	CONSTRAINT staff_pkey PRIMARY KEY (id_staff),
	CONSTRAINT staff_id_hospital_fkey FOREIGN KEY (id_hospital) references hospital(id_hospital) ON UPDATE CASCADE ON DELETE RESTRICT
);

--Creación de la tabla Protocolo Covid--
create table covid_protocol(
	id_covid_protocol serial not null,
	id_hospital int4 not null,
	screen_covid_patients boolean, 
	preventions_campaigns boolean,
	currently_ability_for_tests boolean,
	resources_for_covid varchar (50),
	track_regularly_cases boolean,
	report_covid_result_to_MOPH_days varchar (50),
	last_update timestamp NOT NULL DEFAULT now(),
	CONSTRAINT covid_protocol_pkey PRIMARY KEY (id_covid_protocol),
	CONSTRAINT covid_protocol_id_hospital_fkey FOREIGN KEY (id_hospital) references hospital(id_hospital) ON UPDATE CASCADE ON DELETE RESTRICT
);

--Creación de la tabla estadisticas paciente--
create table patient_statistics(
	id_patient_statistics serial not null,
	id_hospital int4 not null,
	amount_last_month_Covid_symptoms varchar(20),
	amount_last_month_tested_positive_covid varchar(20),
	amount_last_month_in_intensive_care_wCovid varchar(20),
	amount_last_month_deaths_by_covid varchar(20),
	amount_last_month_deaths_non_covid varchar(20),
	amount_last_month_recovered_from_covid varchar(20),
	last_update timestamp NOT NULL DEFAULT now(),
	CONSTRAINT patients_statistics_pkey PRIMARY KEY (id_patient_statistics),
	CONSTRAINT patient_statistics_id_hospital_fkey FOREIGN KEY (id_hospital) references hospital(id_hospital) ON UPDATE CASCADE ON DELETE RESTRICT
);

--Creación de la tabla Estructura Hospital--
create table hospital_structure (
	id_hospital_structure serial not null,
	id_hospital int4 not null,
	id_inventory int4 not null,
	id_patients_statistics int4 not null,
	id_covid_protocol int4 not null,
	id_staff int4 not null,
	last_update timestamp NOT NULL DEFAULT now(),
	CONSTRAINT hospital_structure_pkey PRIMARY KEY (id_hospital_structure),
	CONSTRAINT hospital_structure_id_hospital_fkey FOREIGN KEY (id_hospital) REFERENCES hospital(id_hospital) ON UPDATE CASCADE ON DELETE RESTRICT,
	CONSTRAINT hospital_structure_id_inventory_fkey FOREIGN KEY (id_inventory) REFERENCES inventory(id_inventory) ON UPDATE CASCADE ON DELETE RESTRICT,
	CONSTRAINT hospital_structure_id_patient_statistics_fkey FOREIGN KEY (id_patient_statistics) REFERENCES patient_statistics(id_patient_statistics) ON UPDATE CASCADE ON DELETE RESTRICT,
	CONSTRAINT hospital_structure_id_covid_protocol_fkey FOREIGN KEY (id_covid_protocol) REFERENCES covid_protocol(id_covid_protocol) ON UPDATE CASCADE ON DELETE restrict,
	CONSTRAINT hospital_structure_id_staff_fkey FOREIGN KEY (id_staff) REFERENCES staff(id_staff) ON UPDATE CASCADE ON DELETE RESTRICT
);
