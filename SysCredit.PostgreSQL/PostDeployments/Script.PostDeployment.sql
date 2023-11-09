/*
Plantilla de script posterior a la implementaci�n.                         
--------------------------------------------------------------------------------------
Este archivo contiene instrucciones que se ejecutar�n despu�s de implementar la base de datos.
Utiliza la sintaxis de PostgreSQL para ejecutar archivos SQL.                  
--------------------------------------------------------------------------------------
*/

/*
psql � Terminal interactivo de PostgreSQL.
https://www.postgresql.org/docs/current/app-psql.html

Conexi�n a la base de datos.
Par�metros: nombre del host (-h), base de datos (-d), usuario (-U), puerto (-p), 
contrase�a (-W) para la ejecuci�n.
*/

--Notaci�n -f  nombre_de_archivo: para leer y ejecutar script utilizando comandos psql en el entorno de PostgreSQL.
psql -h tu_host -d tu_base_de_datos -U tu_usuario -p tu_puerto -W -f "ruta/al/archivo/Seed.sql"

--Notaci�n \i en SQL Shell (psql): \i LoanType.Seed.sql.

\i \ruta\al\archivo\LoanType.sql
\i \ruta\al\archivo\Relationship.sql
\i \ruta\al\archivo\PaymentFrequency.sql