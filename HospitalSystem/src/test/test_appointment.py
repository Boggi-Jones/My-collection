import os, sys
import json
import unittest
import time
import datetime
dir_path = os.path.dirname(os.path.realpath(__file__))
parent_dir_path = os.path.abspath(os.path.join(dir_path, os.pardir))
sys.path.insert(0, parent_dir_path)


from app.Functions.appointment_functions import AppointmentFunctions


class TestDoctor(unittest.TestCase):
    def setUp(self) -> None:
        self.appointment_functions = AppointmentFunctions()

    def test_add_appointment(self):
        doctor_id = '1111111111'
        patient_id = '2222222222'
        appointment_date = datetime.date(2022,1,1)
        appointment_time = datetime.time(0,0,0)
        self.appointment_functions.create_appointment(
            doctor_id,
            patient_id,
            appointment_date,
            appointment_time
        )
        self.assertEqual(self.appointment_functions.appointments[doctor_id][0].patient_id, '2222222222')
        self.assertEqual(self.appointment_functions.appointments[doctor_id][0].date, datetime.date(2022,1,1))
        self.assertEqual(self.appointment_functions.appointments[doctor_id][0].time, datetime.time(0,0,0))


    def tearDown(self) -> None:
        print("Test done")

if __name__ == "__main__":
    unittest.main()