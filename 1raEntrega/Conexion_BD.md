# Prueba de conexión a la base de datos

## Creación de base de datos (preeliminar)

Primeramente, se hace la creación de una nueva base de datos en PostgreSql y añadimos un schema. Dentro del schema, añadimos las tablas provisionales de hospital y telefono con el siguiente script:

![creacion_bd](https://user-images.githubusercontent.com/70402438/116436783-5b535e00-a812-11eb-9942-e4dcdb4520f7.png)

```
--Creamos una base de datos llamada: proyecto final

--Creación de tabla preeliminar hospital: sujeta a cambios
create table hospital(
	id_hospital serial primary key,
	latitud double precision,
	longitud double precision,
	altitud double precision,
	nombre varchar(200),
	distrito varchar(200),
	provincia varchar(200),
	pais varchar(200)
);

--Tabla de teléfonos, ya que se puede tener varios
create table telefono(
id_telefono serial primary key,
id_hospital serial references hospital(id_hospital),
lada numeric(3),
numero numeric(10)
);

--Inserción de datos para probar la conexión con la Web Application
insert into hospital (latitud, longitud, altitud, nombre, distrito, provincia, pais) 
values(9.801, 50.111, 2130.2, 'Hospital Angeles', 'Ciudad de Mexico', 'Miguel Hidalgo', 'Mexico'),
(9.801, 50.111, 2130.2, 'Hospital Medica Sur', 'Ciudad de Mexico', 'Tlalpan', 'Mexico');
```

Además, se hace la inserción de dos hospitales para poder probar una conexión entre esta BD recientemente creada y nuestra Web Application.

## Conexión con la web application

Para lograr la conexión fue necesario insertar un paquete (Npgsql) que permitiera la conexión entre ASP.NET y PostgreSql. Esto se hace desde Tools > Nuget Package Manager, desde ahí hay 
dos opciones:
1) Instalar el paquete desde la consola que ahí proveen
2) Buscar el paquete en Manage Packages e instalarlo en nuestro proyecto

Posteriormente, es necesario agregar: 
```
using Npgsql;
```

A las distintas páginas de código de nuestro proyecto para usar todo lo que tiene integrado. UNa vez realizado esto, se paso a hacer el código para abrir la conexión
mediante un string de conexión:

```
public static NpgsqlConnection agregarConexion()
        {
            NpgsqlConnection conexion = new NpgsqlConnection();
            try
            {
                conexion.ConnectionString = "Host=localhost;Username=postgres;Password=admin;Database=proyecto_final";
                conexion.Open();
            }
            catch (Exception ex)
            {
                conexion = null;
            }
            return conexion;
        }
  ```
  
  Posteriormente probamos que nuestra conexión fue exitosa mediante otro código que tiene el objetivo de llenar un dropdownlist con los nombres de los hospitales
  registrados. Aquí es donde hacemos uso de la inserción que previamente hicimos.
  
  ```
   public static void llenarHospital(DropDownList dd)
        {
            try
            {
                NpgsqlConnection con;
                NpgsqlDataReader rd;
                con = Conexion.agregarConexion();
                NpgsqlCommand cmd = new NpgsqlCommand("select nombre from voxmapp.hospital", con);
                rd = cmd.ExecuteReader();
                dd.Items.Add("[Nombre del hospital]");
                while (rd.Read()) 
                {
                    dd.Items.Add(rd.GetString(0));  
                }
                dd.SelectedIndex = 0; 
                rd.Close();
                con.Close();
            }
            catch (Exception ex)
            {

            }
  ```
  
  Una vez que corremos nuestra aplicación Web podemos ver que efectivamente aparecen los dos hospitales dados de alta.
  
  ![conexion_1](https://user-images.githubusercontent.com/70402438/116438341-f7ca3000-a813-11eb-8cf8-5abbe8c9fde7.png)

  Además se añadieron unos textboxes y un button que permiten la creación de un hospital en nuestra BD. Para realizarlo fue necesario crear una clase hospital wue en sus 
  atributos tuviera los de la BD y al momento de dar click en el botón "Registrar", se instanciara obteniendo sus datos de las textboxes, se abriera una conexión a la BD y
  se hiciera la inserción:
  
  ```
  public class Hospital
    {
        private double latitud;
        private double longitud;
        private double altura;
        private string nombre;
        private string distrito;
        private string provincia;
        private string pais;
        Hospital h;

        public Hospital(double latitud, double longitud, double altura, string nombre, string distrito, string provincia, string pais)
        {
            this.latitud = latitud;
            this.longitud = longitud;
            this.altura = altura;
            this.nombre = nombre;
            this.distrito = distrito;
            this.provincia = provincia;
            this.pais = pais;
        }

        public int registraHospital()
        {
            NpgsqlCommand cmd;
            NpgsqlConnection con;
            int res;
            String query = "insert into voxmapp.hospital (latitud, longitud, altitud, nombre, distrito, provincia, pais) values (" +
                latitud + ", " + longitud + ", " + altura + ", " + "'" + nombre + "', '" + distrito + "', '" + provincia + "', '" + pais + "')";
            try
            {
                con = Conexion.agregarConexion();
                cmd = new NpgsqlCommand(query, con);
                res = cmd.ExecuteNonQuery();
                con.Close();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
  ```
    
   El proceso visto desde la aplicación es el que sigue:
   
   1) Tenemos dos datos en nuestra BD
 
   ![conexion_2 1](https://user-images.githubusercontent.com/70402438/116439085-c69e2f80-a814-11eb-88f1-5eb0d74e2b60.png)
 
   2) Hacemos la inserción en la página, si fue correcta, nos limpia las textbox, nos marca éxito y desactiva el botón

   ![conexion_2 2](https://user-images.githubusercontent.com/70402438/116439541-4af0b280-a815-11eb-9215-1b09288267e6.png)

   5) La inserción se ve reflejada en nuestra BD

   ![conexion_2 3](https://user-images.githubusercontent.com/70402438/116439815-92773e80-a815-11eb-8963-b1eb62f93d03.png)

    
