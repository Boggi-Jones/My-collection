import unittest
import json
##go one direction up before importing

import os, sys
dir_path = os.path.dirname(os.path.realpath(__file__))
parent_dir_path = os.path.abspath(os.path.join(dir_path, os.pardir))
sys.path.insert(0, parent_dir_path)


from app.Functions.patient_functions import PatientFunction

class TestPatientFunctions(unittest.TestCase):
    def setUp(self) -> None:
        self.patient_functions = PatientFunction()
        
    def test_adding_valid_patient(self):
        patient_list = self.patient_functions.patient_list

        json_string = json.dumps({'ssn':'2106002130','name':'Ivan','allergies':'apple'})
        self.patient_functions.create_patient(json_string)

        the_patient = patient_list[2106002130]
        self.assertEqual(the_patient[0],"Ivan")
        self.assertEqual(the_patient[1],"apple")

    def test_adding_double_patient(self):
        patient_record_list = self.patient_functions.patient_records

        json_string = json.dumps({'ssn':'2106002130','date':'21/09/2021','description':'just a small headache, gave him a perscription for ibufen'})
        self.patient_functions.add_patient_record(json_string)
        the_record = patient_record_list[2106002130]
        self.assertEqual(the_record[0][0],"21/09/2021")
        self.assertEqual(the_record[0][1],"just a small headache, gave him a perscription for ibufen")

    def test_get_patient_success(self):
        the_patient = self.patient_functions.get_patient('{"ssn": 1234567890}')
        the_patient = json.loads(the_patient)
        self.assertEqual(int(the_patient["ssn"]),1234567890)
        self.assertEqual(the_patient["name"], "thomas")
        self.assertEqual(the_patient["allergies"], "seafood")
            
    def test_get_patient_records_success(self):
        the_patient_record = self.patient_functions.get_patient_record('{"ssn": 1234567890}')
        the_patient_record = json.loads(the_patient_record)
        self.assertEqual(the_patient_record["data"][0][0],"17/9/2021")
        self.assertEqual(the_patient_record["data"][0][1], "He came in with a back problem, i checked up on him and there was nothnig specifically wrong he problay just had stressed his back a lot in his job. I told him to watch out when lifting heavy objects and gave him a perscription for some ibufen.")
        
    def test_readAll_patient(self):
        the_list = self.patient_functions.readAll_patient()
        print(the_list)

    def tearDown(self):
        print("Test done")

if __name__ == "__main__":
    unittest.main()