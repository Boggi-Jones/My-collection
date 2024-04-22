# from Models.nurseclass import Nurse
import json

class NurseFunctions:

    def __init__(self):
        self.nurse = {}
        self.nurses = []

    def add_nurses(self, data):
        ''' Take in information from user, (need to insert name, age and phone) and add to the list'''
        nurse_data = json.loads(data)
        self.nurse[int(nurse_data["id"])] = [nurse_data["name"], nurse_data["phone"]]

    def get_nurse(self, data):
        data_val = json.loads(data)
        ssn = int(data_val["ssn"])
        for nurse in self.nurses:
            if int(ssn) in nurse:
                return_val = json.dumps({"ssn": ssn, "name": nurse[ssn][0], "phone": nurse[ssn][1]})
                return return_val
        return '{"msg":"This nurse is not in the system"}'

    def update_nurse(self, data):
        pass

    def remove_nurse(self, id):
        '''Check if the id is in and remove the nurse if it is'''
        nurse_id = json.loads(id)
        try:   
            del self.nurse[nurse_id["id"]]
        except KeyError:
            print("Id not in the list")

    def __str__(self):
        ret_str = ""
        for i, j in self.nurse.items():
            ret_str += str(i) + ": "
            ret_str += str(j[0]) + " "
            ret_str += str(j[1]) + ",\n "
        return "Nurses: \n {}".format(ret_str)


# Dummy data:
nurses = NurseFunctions()
nurses.add_nurses('{"id": 1234872384, "name": "Alice", "phone": 9873453}')
nurses.add_nurses('{"id": 1642345642, "name": "Ben", "phone": 1254367}')
nurses.add_nurses('{"id": 2739482738, "name": "Luke", "phone": 7654567}')
nurses.add_nurses('{"id": 5313456328, "name": "Kathrine","phone": 1262367}')
nurses.add_nurses('{"id": 2812374392, "name": "Bridget", "phone": 1234489}')
# nurses.add_nurses(2812374392, "Bridget", 1234489)
print(nurses)
nurses.remove_nurse('{"id": 1234872384}')
print(nurses)
nurses.remove_nurse('{"id": 1234456384}')
print(nurses)