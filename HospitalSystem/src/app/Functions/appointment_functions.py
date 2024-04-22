import time
import datetime
import os, sys
import random
import os, sys
from app.Models.appointmentclass import Appointment
class AppointmentFunctions:
    def __init__(self):
        self.appointments = {}

    def create_appointment(self, doctor_id, patient_id, date, time):
        '''
        Creates an appointment and adds it into an appointment database.
        '''
        appointment = Appointment(
            doctor_id,
            patient_id,
            date,
            time
        )
        self.add_appointment_to_server(appointment)

    def add_appointment_to_server(self, appointment):
        '''
        Adds appointment to server
        '''
        if appointment.doctor_id in self.appointments:
            self.appointments[appointment.doctor_id].append(appointment)
        else:
            self.appointments[appointment.doctor_id] = [appointment]

    def add_dummy_data(self):
        '''
        Creates dummy data to that tests the functions
        '''
        doctor_ids = [str(i)*10 for i in range(10)]
        for id in doctor_ids:
            for i in range(10):
                patient_id = str(random.randint(1000000000,9999999999))
                date = datetime.date(2022,1,1)
                time = datetime.time(i,0,0)
                self.create_appointment(id, patient_id, date, time)

    def view_appointments(self, doctor_id):
        '''
        Returns appointments for a specific doctor as a string.
        '''
        for appointment in self.appointments[doctor_id]:
            print(appointment)

if __name__ == "__main__":

    appointments = AppointmentFunctions()

    appointments.add_dummy_data()

    appointments.view_appointments('1111111111')
