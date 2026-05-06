# Prueba Tecnica - API de Empleados

## Contexto general
El area de Recursos Humanos cuenta con una API interna para consultar y administrar informacion de empleados.

El proyecto fue iniciado por otro desarrollador y paso por QA, donde se detectaron comportamientos que no cumplen con lo esperado por negocio.

El objetivo de esta prueba no es solo "hacer que corra", sino evaluar la capacidad de analisis, diagnostico y correccion sobre una base de codigo existente.

## Objetivo
Dejar la API en un estado funcional, consistente y lista para una nueva validacion de QA.

## Escenario del sistema
La API expone operaciones para:
- consultar listado de empleados
- consultar un empleado por ID
- registrar empleados
- actualizar empleados
- calcular bono anual

## Situaciones reportadas por QA y negocio
Debes revisar y corregir los siguientes puntos:

1. **Estabilidad general del proyecto**
   - Despues de cambios recientes, el sistema no quedo estable desde el punto de vista tecnico.
   - Antes de validar negocio, la solucion debe ejecutar correctamente.

2. **Listado general incorrecto para RRHH**
   - El listado principal debe mostrar unicamente empleados vigentes (activos).

3. **Consulta individual inconsistente**
   - Al consultar IDs inexistentes, la API no responde de forma controlada.

4. **Registros duplicados**
   - El alta de empleados permite duplicados en un campo que debe ser unico (email).

5. **Calculo de bono anual incorrecto**
   - La regla de negocio es:
     - mas de 5 años: **10%** del salario
     - entre 1 y 5 años: **5%** del salario
     - menos de 1 año: **0%**

## Que se espera de tu revision
Analiza el proyecto y corrige los problemas funcionales y tecnicos reportados, sin sobrearquitectura ni cambios fuera de alcance.

## Entregable esperado
- proyecto ajustado y ejecutable
- API lista para volver a ser probada por QA
- breve explicacion de:
  - hallazgos encontrados
  - cambios realizados
  - decisiones tecnicas tomadas

## Criterios de evaluacion
- comprension del problema antes de codificar
- capacidad de diagnostico tecnico y funcional
- consistencia de las correcciones
- respeto de reglas de negocio
- claridad del codigo entregado
- estado final listo para pruebas

## Consideraciones tecnicas
- usa `Scripts/init.sql` para crear base, tabla y datos semilla
- configura conexion SQL Server en `appsettings.json`
- asegurate de usar un modo de autenticacion coherente con la configuracion elegida
- no se espera agregar frameworks ni capas fuera del alcance

## Endpoints disponibles
- `GET /api/employees`
- `GET /api/employees/{id}`
- `POST /api/employees`
- `PUT /api/employees/{id}`
- `GET /api/employees/{id}/bonus`
