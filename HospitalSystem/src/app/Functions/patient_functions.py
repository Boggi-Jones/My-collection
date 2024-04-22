import json

class PatientFunction:
    def __init__(self) -> None:
        self.patient_list = {1234567890:["thomas", "thomas@thomas.net", "seafood", "null", "null"], 9876543210:["jonathan", "j@j.j", "nuts", "null", "null"]}
        self.patient_records = {1234567890:[["17/9/2021","He came in with a back problem, i checked up on him and there was nothnig specifically wrong he problay just had stressed his back a lot in his job. I told him to watch out when lifting heavy objects and gave him a perscription for some ibufen."]]}

    def readAll_patient(self):
        retlist = []
        for key, data in self.patient_list.items():
            retdict = {
                "name": data[0],
                "email": data[1],
                "note": data[2],
                "username": key,
                "doctor_id": data[3],
                "nurse_id": data[4]
            }
            retlist.append(retdict)
        return retlist

    def get_patient_record(self, data):
        """Gets an ssn from the server and gives back summary of all appointments"""
        patient = json.loads(data)
        try:
            return_list = self.patient_records[patient["ssn"]]
            return json.dumps({"data": return_list})
        except KeyError:
            return '{"msg":"This patient has no previous appointments"}'

    def delete_patient(self,patient_object):
        del self.patient_list[int(patient_object["ssn"])]

    def get_patient(self, data):
        """Gets a ssn from the server and returns info on that patient if he is in the system"""
        data_val = json.loads(data)
        try:
            the_patient = self.patient_list[data_val["ssn"]]
            return_val =  json.dumps({"ssn": data_val["ssn"],"name": the_patient[0], "allergies": the_patient[1]})
            return return_val
        except KeyError:
            return '{"msg":"This patient is not in the system"}'
    
    def create_patient(self,patient_object):
        """takes in json object and converts it to pythion dict and adds it to the in memory patient list(dict)"""
        self.patient_list[int(patient_object["ssn"])] = [patient_object["name"],patient_object["allergies"]]
        
    def add_patient_record(self,record_object):
        """takes in json object and converts it to python dict and adds sit to the patient record dictionary"""

        if int(record_object["ssn"]) not in self.patient_records:
            self.patient_records[int(record_object["ssn"])] = [[record_object["date"],record_object["description"]]]
        else:
            old_records = self.patient_records[int(record_object["ssn"])]
            old_records.append([record_object["date"],record_object["description"]])
            self.patient_records[record_object["ssn"]] = old_records
        

