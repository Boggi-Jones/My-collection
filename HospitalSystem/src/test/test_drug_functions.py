import unittest
import json
##go one direction up before importing
import sys
sys.path.append("../")

from app.Models.drugclass import Drug
from app.Functions.drug_functions import DrugFunctions

class TestDrugFunctions(unittest.TestCase):
    def setUp(self) -> None:
        self.drug_functions = DrugFunctions()
        

    def test_add_drug_to_inventory(self):
        # make the inventory empty
        self.drug_functions.inventory.inventory = []

        drug_dict = {"name": "ritalin", "strength": "400mg", "price": 200, "quantity": 20}
        drug = Drug(drug_dict)
        self.drug_functions.add_drug_to_inventory(drug)
        
        drug_in_inventory = self.drug_functions.inventory.inventory[0]

        ##check if the last item in the inventory is equal to the new drug that was added
        self.assertEqual(drug_in_inventory["name"],drug_dict["name"])
        self.assertEqual(drug_in_inventory["strength"],drug_dict["strength"])
        self.assertEqual(drug_in_inventory["price"],drug_dict["price"])
        self.assertEqual(drug_in_inventory["quantity"],drug_dict["quantity"])
    
    def test_order_drugs(self):
        # make the orders empty
        self.drug_functions.inventory.orders = []
        drug_dict = {"name": "ritalin", "strength": "400mg", "quantity": 20}
        drug = Drug(drug_dict)
        self.drug_functions.order_drugs(drug)
        orders = self.drug_functions.inventory.orders
        self.assertEqual(orders[0]["name"],"ritalin")
        self.assertEqual(orders[0]["strength"],"400mg")
        self.assertEqual(orders[0]["quantity"],20)

    def test_get_drug_inventory(self):
        
        self.drug_functions.inventory.inventory = [{"id": 0, "name": "ritalin", "strength": "400mg", "price": 200, "quantity": 20}]
        
        ret = self.drug_functions.get_drug_inventory()
        ret = json.loads(ret)
        self.assertEqual(ret[0]["id"], 0)
        self.assertEqual(ret[0]["name"], "ritalin")
        self.assertEqual(ret[0]["quantity"], 20)
        
    def tearDown(self):
        print("Test done")

if __name__ == "__main__":
    unittest.main()