# RapidPay Service Api

This Api service handles payments cards and registers payments transactions.
Is divided in two main parts:
    1. Card management which includes payment card registration and get card balance.
    2. Payment registration which allows register different payments to an existing card and list all registrations done.

# Technical features

    1. Entity Framework Core is used as ORM
    2. Sqlite is used for data persistence provider
    3. Fluent validations to check all main operations
    4. Universal Fee Exchange generates a new fee every hour.
    5. Automapper is used to generate DTO responses

# Security

This project implements JWT validation. Also Swagger UI is configured to enable user authentication.

# User credentials

As this is a test project there is only one user to allow access.

* Username: admin
* Password: RapidPay123!