<!-- TITLE: ORM Discussion -->
<!-- SUBTITLE: The Doomsday Scenario -->

# What We Want
## Flexibility
- Not to be tied to a single ORM implementation
- Ability to switch away from SQL Server if/when necessary

## Mapping
- Fairly simple-to-design mapping patterns
- Single-design maps that can be easily called to do CRUD actions between DTOs/POCOs and the Database
- Handle nested pocos, such as `IEnumerable` collections

## LINQ/C#-Method Queries
- Allow simple C# methods to construct SQL queries
- Handle main use cases
	- Select
	- Where
	- In
	- Simple Joins
	- OrderBy

## Raw SQL Access
- Continue to allow raw SQL/ADO access for unique scenarios that don't fit the ORM

# Decision Process
## Study and Apply Available ORMs
Multiple Puffins will each research a single ORM, and then use it to implement a code refactoring of the same class/repository.
- **Tim Jay**: *NPoco*
- **Joe Kausits**: ?
- **Tim Purdum**: ?
- ???

## Application/ORM Review
Puffins will review the comments and implementation from each ORM, and select the one that best fits our needs.

## Wrap it Up
If possible/applicable, we will create an IDN-ORM wrapper around the chosen ORM, to select exactly what features we want to expose.

## Final Review
Code review of the wrapper project, and commit to master.

## Documentation/Training
Write documentation and have meetings to ensure that everyone is aware of how to use the new ORM tools.