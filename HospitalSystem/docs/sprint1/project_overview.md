## 1. What is part of your system? What is outside?
### What the system does:
 - Store data about patients and staff members
 - Patient’s medical history & basic information
 - Keeps track of appointments
 - Keeps track of hospital inventory
 - Order drugs from pharmacies
 - Keeps track of order history
 - Keeps track of room availability
 - Keeps track of staff work schedules
 - Send prescription order to pharmacies
 - Keeps track of surgery waitlists
 - Calculates invoices and sends them to patients.
### What the system doesn't do
- Does not allow patients to book an appointment directly
- Does not manage staff work schedules
- Does not handle staff salaries.
- Does not manage insurance


## 2. What relevant stakeholders/personas exist?
- On call doctors
- Appointment doctors
- Surgeons
- Nurses
- Hospital administrators
- Pharmacists
- Receptionists

# Scenarios
### Scenario 1 - As a nurse, I want to see my work schedule, so that I can know when I work - User story #22

Nurse Luke has forgotten if he was working tomorrow or not. So he decides to login to the hospital system and then open his work schedule.

### Scenario 2 - As a doctor, i want to be able to see a patients medical history so that, i can treat them accordingly. - User story #1
Doctor Jebodiah has an appointment with a patient so he opens the system on his work computer and clicks "patients record tab" and enters the patients social security number in the system and is displayed the patients ailments, allergies and comments from other doctors, he uses the information to better treat the patient.

### Scenario 3 - As a hospital administrator, I want to be able to see the hospital's drug inventory, so that I can ensure the hospital never runs out of drugs. - As a hospital administrator, I want to be able to order drugs from a pharmacy, so that the hospital can keep its drug inventory stocked up. - As a hospital administrator I want to be able to filter the hospital drug inventory by drugs that are low on stock, so that I can order more so that the hospital does not run out of drugs. User stories = [#9, #13, #29]
Hospital administrator Gwendolyn has just finished her meeting but remembers that she still has to check the inventory for the day since it’s every week on Tuesday. She rushes to her office to put the order in because she has to pick up the kids from school. She opens up her computer, opens up the hospital system, and navigates to a tab where she can see the hospital’s drug inventory. Gwendolyn filter’s out drugs that have enough stock.  With the filtered results she notices they are missing Benazepril. She then sends a request to the pharmacy for some Benazepril. Finally she logs out and turns off the computer.

## Scenario 4 - As a receptionist, I want to be able to create and send invoices, so that the patients can be billed. User story = #23
A patient just finished an appointment with Dr. Jebodiah. Receptionist Gladys is notified so she starts to make an invoice for the appointment. She opens up the system, goes into the patient tab and opens up the profile for the patient. She locates the invoice tab, clicks “new invoice” and fills in the correct information for the invoice.

## scenario 5 - As a receptionist, I want to see patient history, so that I can see if the patient has any allergies to certain medicine.
A patient comes in for a procedure and needs to be medicated. He does not know if he has any allergies so Gledys the receptionist opens the system, navigates to the patient tab, opens the patient's profile and sees a list of all his allergies.

# Domain model
[link to domain model](https://drive.google.com/file/d/1pQ7PiM6LVDQHsy2sJmntSfuI84WJAR1-/view?usp=sharing)
![DomainModel.drawio](/uploads/21e0be09209d8953f06a820a5627b868/DomainModel.drawio.png)
