import os, sys
import json
import unittest
dir_path = os.path.dirname(os.path.realpath(__file__))
parent_dir_path = os.path.abspath(os.path.join(dir_path, os.pardir))
sys.path.insert(0, parent_dir_path)


from app.Functions.doctor_functions import DoctorFunctions


class TestDoctor(unittest.TestCase):
    def setUp(self) -> None:
        self.doctor_functions = DoctorFunctions()

    def test_add_doctor(self):
        doctor_list = self.doctor_functions.doctor_data
        string = {"ssn":"2222222222", "name":"skarpi", "occupation":"surgeon"}
        self.doctor_functions.add_doctors(string)

        doctor_in_system = doctor_list[2222222222]
        self.assertEqual(doctor_in_system[0], "skarpi")
        self.assertEqual(doctor_in_system[1], "surgeon")

    def test_rem_doctor(self):
        doctor_list = self.doctor_functions.doctor_data
        string = json.dumps({"ssn":"3333333333", "name":"Jebadiah", "occupation":"soap opera doctor"})
        self.doctor_functions.add_doctors(string)

        self.doctor_functions.add_doctors(string)
    def test_update_doctor(self):
        self.doctor_functions.doctor_data = {2910992629: ["aron bergur", "surgeon"]}
        self.doctor_functions.update_doctors({"ssn":"2910992629", "name":"tomas", "occupation":"intern"})
        name =self.doctor_functions.doctor_data[2910992629][0]
        occupation =self.doctor_functions.doctor_data[2910992629][1]
        self.assertEqual(name, "tomas")
        self.assertEqual(occupation, "intern")



    def tearDown(self) -> None:
        print("Test done")

if __name__ == "__main__":
    unittest.main()