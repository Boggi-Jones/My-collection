

class Drug:
    def __init__(self, kwargs) -> None:
        """initialize some variables from kwargs"""
        self.id = kwargs["id"] if "id" in kwargs else None
        self.name = kwargs["name"] if "name" in kwargs else ""
        self.quantity = kwargs["quantity"] if "quantity" in kwargs else None
        self.strength = kwargs["strength"] if "strength" in kwargs else ""
        self.price = kwargs["price"] if "price" in kwargs else None

    def __str__(self):
        """to help debuginng"""
        return f"{self.name}, quantity: {self.quantity}"

if __name__ == "__main__":
    d = Drug({ "name": "paratabs-500mg", "quantity": 200})
    print(d)
