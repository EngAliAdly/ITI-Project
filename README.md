
![Dashboard](https://github.com/ClinicMasterITI/ClinicMaster/blob/master/Clinic.png)

# ClinicMaster

Clinic web application  to support requirement which is that patients visit clinic and get registered, after that, they make an appointment to available doctors. By default the appointment gets a pending status because it needs to be reviewed. After that, the doctor is going to work out the patient attendance. Under the report we should have daily and monthly appointments.

# Frameworks - Libraries

1. .NET 7 
2. Entity Framework Core

# Running Project

- Open the project with Visual Studio.
- in `appsettings.json`file change the connection string according to your system.
  ```
  "DefaultConnection": "Server=Your data source;Database=ClinicMasterDb;Trusted_Connection=True;TrustServerCertificate=True"
  ```
