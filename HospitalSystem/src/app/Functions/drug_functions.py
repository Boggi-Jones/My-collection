
from app.Models.drugclass import Drug
from app.Models.drug_inventoryclass import DrugInventory
from app.IO.InventoryIO import InventoryIO
import json



class DrugFunctions:
    def __init__(self) -> None:
        """
        initalize the inventory class, so we have access to the drug inventory
        """
        self.inventory = InventoryIO()
    
    def get_drug_inventory(self):
        """uses the inventory object to get the invetory and returns the inventory model class"""
        inventory = self.inventory.get_inventory()
        return DrugInventory(inventory).get_inventory()
    
    def add_drug_to_inventory(self, drug: Drug):
        """calls the inventoryIo object to add a drug to the inventory """
        self.inventory.add_to_inventory(drug)
    
    def order_drugs(self, drug):
        """adds drugs to orders list in the inventoryIO object"""
        self.inventory.add_to_orders(drug)
        
if __name__ == "__main__":
    df = DrugFunctions()
    for drug in df.get_drug_inventory():
        print(drug)
    