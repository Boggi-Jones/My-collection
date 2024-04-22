import json


class DrugInventory:
    def __init__(self, inventory) -> None:
        """stores the inventory"""
        self.inventory = inventory
    
    def get_inventory(self):
        """creates simplified list of all the drugs in inventory and returns as json"""
        res = [{"id": drug["id"], "name": drug["name"], "quantity": drug["quantity"]} for drug in self.inventory]
        return json.dumps(res)
    
