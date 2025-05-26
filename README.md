<p align="center">
  <img src="https://www.uit.edu.vn/sites/vi/files/banner_uit.png" alt="Trường Đại học Công nghệ Thông tin | University of Information Technology">
</p>

# Garage Management Project
We have developed a MAUI cross-platform application for managing all garage business services and human resources. Following my teacher's instruction, this app has been built that meets four key software requirement: Storage, Retrieval, Extraction, Evolution
## Tech 
In this project, we use: 
- .NET MAUI (Multi-platform Application UI) for data representation and user interaction 
- .NET Api with Entity Framework Core (.NET based ORM) for querying data from Database and handling bussiness logics 
- Microsoft SQL Server for data storage 

> As .NET enthusiasts, we crafted the entire project on Microsoft ecosystem (e.g., NET Framework and SqlServer)  

| Name | Student ID | Duty|
| ------ | ------ |------|
| [To Phu Quy](https://github.com/seven-up-seven) | 23521320 | Refactored API endpoints, implemented JWT authentication services, provided dynamic authorization, complete UIs and logics behind.|
| [Nguyen Minh Quang](https://github.com/Whiteknight12) | 23521286 |Created the base project structure, including MAUI pages, API endpoints, data models, and client API service library. Completed UIs and logic behind.|
| [Phan Chi Cuong]() |23520207 |Complete UIs and logics behind, tested the application, and fixed UI bugs.|
| [Nguyen Khanh Linh]() |22520769 |	Designed the project structure and wrote the documentation.|
| [Pham Thi Cam Tien]()| 22521473|	Designed the project structure and wrote the documentation.|


## Installation (3 steps)

### 1. Download 
Download this project file directly [here](https://github.com/Whiteknight12/GarageManagement/archive/refs/heads/main.zip) or ``` git clone https://github.com/Whiteknight12/GarageManagement```
### 2. Install Sql Server Management Studio
Download via [here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) and enter Server name as _localhost_
![](https://i.ibb.co/mFr1GmDZ/Screenshot-2025-05-25-115516.png)
### 3. Create database 
Open Visual Studio and run ```update-database``` on Packet Manager Console 
![](https://i.ibb.co/ymdKDNSX/Screenshot-2025-05-25-113406.png)

Or change direction to the solution folder in terminal and install EF Core CLI ``` dotnet tool install --global dotnet-ef
``` then run ```dotnet ef database update```

![](https://i.ibb.co/0pwMWNLC/Screenshot-2025-05-25-114458.png)
