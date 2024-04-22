@startuml
class Doctors
class Nurses
class Patients
class Appointments
class Drugs
class Rooms
class Invoices
class Inventory
class Receptionists
class Hospital_Administrators
class Medical_History
class Drug_history
class Pharmacists
class Wait_List

Receptionists -r- Invoices : Sends to patients
Invoices -- Patients : Recieves
Receptionists -- Appointments: Makes
Doctors -l- Appointments: Go to
Patients -r- Appointments: Go to
Nurses -l- Patients: Helps
Hospital_Administrators -d- Inventory: Keeps updated <
Inventory -d- Drugs: Has
Doctors -u- Rooms: Appointed
Appointments -r- Rooms: Connected too

Pharmacists -d- Drug_history: Sees
Medical_History -u- Drug_history: Has >
Medical_History -d- Patients: Has <
Wait_List -u- Patients
Wait_List -- Receptionists
Drug_history -r- Drugs
@enduml
