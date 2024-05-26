# Tecnologías
- .Net Core (3.1)

## Nuggets
- Moq 4.20.70

- - - - - -

# Estructura Proyecto
Se tomó la decisión de utilizar tres proyectos dentro de la solución para cumplir con la solicitud requerida.

| **Proyecto** | **Descripción** |
| :--------- | :--------- |
| Institute.Core | Concentra la lógica funcional para cada módulo, interviniendo con servicios de tercero, como la persistencia de datos. |
| Institute.Infrastructure | Se encarga del CRUD para cada una de las entidades de la base de datos. |
| Institute.Models | Se encarga de tener todos los modelos de transferencia entre las distintas capas de nuestra solución (DTO). Este proyecto no debe, ni tendrá un modelo que represente la base de datos, ya que el modelo de dominio se tendrá en otro proyecto específico para dicho fin. |
| Institute.Test | Encontraremos todos los test correspondientes a nuestro modelo de negocio situados en el proyecto “Institute.Core”. |

## Decisiones Técnicas
- En los proyectos "Core" y "Infrastructure" se implemento el patrón de diseño **Result** con el fin de optimizar la memoria y eliminar el majo de excepción mediante un try catch.
