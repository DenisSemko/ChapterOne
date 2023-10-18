<img src="https://user-images.githubusercontent.com/53062219/218496976-723d1d0b-4555-476d-8ff2-d5240cf41346.png" alt="logo" title="logo" align="right" height="100" />

# ChapterOne
> A software system for automating the process of searching and selecting books to create your own online library.

## Table of Contents
* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Architecture](#architecture)
* [Features](#features)
* [Screenshots](#screenshots)
* [Setup](#setup)
* [Usage](#usage)
* [Project Status](#project-status)
* [Contact](#contact)
* [License](#license)

## General Information
In terms of the university diploma, there was developed a system that consists of back-end and front-end services.

System provides a friendly UI along with REST API requests to work with the theme about books, authors, online-library etc.

System's business logic provides a fast and comfortable algorithm to pick up the right book for the reader to buy.
The whole point is to find the book spontaneously, with the help of randomnly generated data about available readers, years of publishing, books themes and so on.
> To catch the whole idea about the algorithm and its structure, feel free to contact me [@dench327](https://linkedin.com/in/https://www.linkedin.com/in/denis-semko-551b91191).

System supports localization for two languages: 
 - Ukrainian
 - English

## Technologies Used
- ASP.NET Core 3.1
- Angular 12 & Angular Material
- C#
- TypeScript
- MSSQL
- ASP.NET Identity
- Swagger
- Unit testing with MSTest & Selenium

*Technologies versions weren't updated to the newest due to the diploma's specific conditions.*

## Architecture
Back-end project has N-Layered Architecture that consists of 5 layers:
- ChapterOne - Presentation Layer
- BLL - Business Logic Layer
- DAL - Data Access Layer
- CIL - Core Infrastructure Layer
- DIL - Dependency Injection Layer

## Features
List the ready features here:
- Authorization & Authentication with ASP.Identity and JWT token
- Admin/Reader roles
- User Account Functionality
- Manage user accounts as an *admin*
- Create and upload a data backup as an *admin*
- View system statistics as an *admin*
- Find a book using the book search and Raselection algorithm as a *reader*
- View the entire list of available books and each one separately the book in details as a *reader*
- Rate and purchase books in different formats as a *reader*
- Download e-books and audio-books according to the subscription as a *reader*

## Screenshots
<img src="https://user-images.githubusercontent.com/53062219/218501488-6c1516ff-5e06-476c-8063-5a9848ee5693.png" height="450" />
<img src="https://user-images.githubusercontent.com/53062219/218501554-1d02eaa5-c052-4ad7-9bc9-74da24d7716f.png" height="450" />
<img src="https://user-images.githubusercontent.com/53062219/218501631-2dbb5ce1-9049-456f-a972-47f276e9bc9e.png" height="450" />
<img src="https://user-images.githubusercontent.com/53062219/218501702-06ffd5e5-a22b-40be-8472-56c47e96f2cd.png" height="450" />
<img src="https://user-images.githubusercontent.com/53062219/218501881-3d2a5479-2ab6-4b99-b4d9-f66dfc466246.png" height="450" />

& many more!

## Setup
You need to download this repository and run it using Visual Studio 2019 or newer version or any other IDE that is suitable for you.
> You can run the back-end service via IIS Express!

> You can run the front-end service via installing all the dependencies with the command `npm install` and then using the command `npm start`!

**UPD:** 
> You need to run the front-end service with `ng serve --ssl --ssl-cert {path-to-your-cert} --ssl-key {path-to-your-key}` so your localhost will be HTTPS secured.

## Usage
After successful build via Visual Studio (or any other IDE compatible with .NET), you can access a back-end service as `https://localhost:yourport/swagger/index.html`
and a front-end service as `https://localhost:4200`.

## Project Status
Project is: _no longer being worked on_.

## Contact
Created by [@dench327](https://www.linkedin.com/in/denis-semko-551b91191) - feel free to contact me!

© 2022

## License
> You can check out the full license [here](https://github.com/DenisSemko/ChapterOne/blob/master/LICENSE.md)
This project is licensed under the terms of the MIT license.

