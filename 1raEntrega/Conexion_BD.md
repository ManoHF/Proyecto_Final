# Prueba de conexión a la base de datos

## Creación de base de datos (preeliminar)

Para la creación de la BD con tablas provisionales de hospital y telefono se utilizó el siguiente código:

```
--Se crea la base de datos y se empieza a usar
create database proyecto_final;
use proyecto_final;

--Creación de tabla preeliminar hospital: sujeta a cambios
create table hospital(
	id_hospital int primary key,
	latitud float(20),
	longitud float(20),
	altitud float(20),
	nombre varchar(200),
	distrito varchar(200),
	provincia varchar(200),
	pais varchar(200)
);

--Tabla de teléfonos, ya que se puede tener varios
create table telefono(
id_telefono int primary key,
id_hospital int references hospital,
lada numeric(3),
numero numeric(10)
);

--Inserción de datos para probar la conexión con la Web Application
insert into hospital values (1, 9.801, 50.111, 2130, 'Hospital Angeles', 'Ciudad de Mexico', 'Miguel Hidalgo', 'Mexico');
insert into hospital values (2, 9.801, 50.111, 2130, 'Hospital Medica Sur', 'Ciudad de Mexico', 'Tlalpan', 'Mexico');
```

Además, se hace la inserción de dos hospitales para poder probar una conexión entre esta BD recientemente creada y nuestra Web Application.

## Conexión con la web application

Para lograr la conexión a nuestra BD fue necesario crear una clase C# que llamaremos Conexión y incluye el siguiente código que permite abrir una conexión con nuestra BD. Esto
es posible gracias al String de conexión obtenido en SQL Server.

```
 public static SqlConnection agregarConexion()
        {
            SqlConnection conexion;
            String stringConexion = "Data Source=DESKTOP-VR2NG5E;Initial Catalog=proyecto_final;Integrated Security=True";
            try
            {
                conexion = new SqlConnection(stringConexion);
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
                SqlConnection con;
                SqlDataReader rd;
                con = Conexion.agregarConexion();
                SqlCommand cmd = new SqlCommand("select nombre from hospital", con);
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
        }
  ```
  
  Una vez que corremos nuestra aplicación Web podemos ver que efectivamente aparecen los dos hospitales dados de alta
  
  ![Prueba_conexion_ddl](https://user-images.githubusercontent.com/70402438/116169549-8894f480-a6ca-11eb-885d-07eb1933f898.png)
  
  Le añadimos en este folder del repositorio un zip con el prototipo de la aplicación web. Hay que tomar en cuenta ciertas cosas:
  * se tiene que cambiar en el string el Data Source por el nombre de su computadora
  * es necesario crear la base de datos y hacer la inserción también en su computadora
