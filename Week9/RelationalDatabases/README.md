# Relational Databases - Databases for Developers
## Table of Contents
1. [Assignment](#Assignment)
2. [Where to find solutions](#Where-to-find-solutions)
4. [How to run](#How-to-run)

## Assignment
[Assignment PDF](./Assignment.pdf)

## Where to find solutions
1. Design
    - Vi har valgt at gå med Single-Table strategy
    - Pros/Cons:
        - If you require the best performance and need to use polymorphic queries and relationships, you should choose the single table strategy. But be aware, that you can’t use not null constraints on subclass attributes which increase the risk of data inconsistencies.

        - If data consistency is more important than performance and you need polymorphic queries and relationships, the joined strategy is probably your best option.

        - If you don’t need polymorphic queries or relationships, the table per class strategy is most likely the best fit. It allows you to use constraints to ensure data consistency and provides an option of polymorphic queries. But keep in mind, that polymorphic queries are very complex for this table structure and that you should avoid them.
2. Conceptual level implementation
    - [Database SQL Dump](./SQLDump/Database.sql)
    - [Data SQL Dump](./SQLDump/DataDump.sql)
3. External level implementation
    - Vi har lavet vores Views som C# EntityFramework Repositories
    - [Pet Repository](./Persistent/Repositories/PetRepository.cs)
    - [Base Repository](./Persistent/Repositories/BaseRepository.cs)
4. Interface implementation
    - Vi har brugt .NET Core API til at lave et API som kan vises gennem swagger (Se [How to run](#How-to-run))
    - [Pet API Controller](./Controllers/PetsController.cs)

## How to run
### 1. Run The 2 SQL Scripts on PostgreSQL
- [Database SQL Dump](./SQLDump/Database.sql)
- [Data SQL Dump](./SQLDump/DataDump.sql)

#### 1.1 If running Postgress as Docker:
```
docker cp <local_file> <container_name>:<container_file>
    -> docker cp "C:/Database.sql" postgres:./Database.sql
    -> docker cp "C:/DataDump.sql" postgres:./DataDump.sql

docker exec <container_name> psql <database_name> -f <file_name> -U <user_name> -h localhost
    -> docker exec postgres psql soft2021 -f ./Database.sql -U softdb -h localhost
    -> docker exec postgres psql soft2021 -f ./DataDump.sql -U softdb -h localhost
```

### 2. Change ConnectionString for PostgreSQL
- [appsettings](./appsettings.json)

### 3. Build Docker Image
```
docker build -t relationaldatabases .
```

### 4. Run Docker Image
```
docker run -it --rm -p 8080:80 --name relationaldatabases relationaldatabases
```

### 3. Open Browser and Test it out
- http://localhost:8080/swagger/index.html