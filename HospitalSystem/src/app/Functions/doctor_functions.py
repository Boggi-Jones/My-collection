import json

from app.hospitalserver_client import doctor_appointments

class DoctorFunctions:
    def __init__(self) -> None:
        """initialize a dictionary which will store our doctors and have their ssn as the ID for them"""
        self.doctor_data = {1111111111:["jonss","surgeon"]}

    def add_doctors(self, data):
        """load in a new doctor take the first parameter which should be a ssn and use it to be its key
        to be a able to look them up easily and have no conflicts of doctors because no two persons
        have the same ssn. From the ssn we can get their name and occupation at the hospital"""
        new_doctor = data
        self.doctor_data[int(new_doctor["ssn"])] = [new_doctor["name"], new_doctor["occupation"]]
        
    def update_doctors(self, data):
        """updated the name and occipation of a doctor"""
        doctor = self.doctor_data[int(data["ssn"])]
        doctor[0] = data["name"]
        doctor[1] = data["occupation"]
        

    def find_doctor(self, ssn):
        """Helper function to check if doctor is in our data"""
        if ssn in self.doctor_data:
            return True
        return False

    def remove_doctors(self, doc_obj):
        """take a doctor in the system search him by his ssn and removes him from our system"""
        del self.doctor_data[int(doc_obj["ssn"])]

    def __str__(self):
        ret_str = ""
        for i, j in self.doctor_data.items():
            ret_str += str(i) + ":"
            ret_str += j[0] + " "
            ret_str += j[1] + ",\t"
        return "All doctors: {}".format(ret_str)


if __name__ == "__main__":
    ss = DoctorFunctions()
    print(ss)
    ss.add_doctors('{"ssn":"2222222222", "name":"skarpi", "occupation":"surgeon"}')
    print(ss)
    ss.remove_doctors('{"ssn":"2222222222"}')
    print(ss)