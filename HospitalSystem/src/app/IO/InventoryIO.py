
class InventoryIO:
    def __init__(self):
        """initalizing  the inventory, orders, and id which is used to id the drugs"""
        self.id = 4
        self.inventory = [
            {"id": 0,"name": "paratabs-500mg", "quantity": 200, "strength": "500mg", "price": "20"}, 
            {"id": 1, "name": "ibufen-200mg", "quantity": 20,"strength": "200mg", "price": "10"},
            {"id": 2, "name": "ibufen-400mg", "quantity": 150,"strength": "400mg", "price": "15"},
            {"id": 3, "name": "Morphine Sulphate BP","quantity": 3, "strength": "","price": "2000"},
            ]
        self.orders = []
    
    def add_to_inventory(self, drug):
        """creates dictionery from the drug object and adds id, appends the inventory and increment id"""
        res = {"id": self.id, "name": drug.name, "quantity": drug.quantity, "strength": drug.strength, "price": drug.price}        
        self.inventory.append(res)
        self.id += 1
    
    def add_to_orders(self, drug):
        """creates dictionery from the drug object and appends to orders"""
        # adds the drug to orders list
        res = {"name": drug.name, "quantity": drug.quantity, "strength": drug.strength, "price": drug.price}
        self.orders.append(res)

    def get_inventory(self):
        """simply return the inventory"""
        return self.inventory

    def __str__(self):
        res = "\n".join([str(drug) for drug in self.inventory])
        return res