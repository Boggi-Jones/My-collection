from dataclasses import dataclass


@dataclass
class Buyer:
    name: str
    email: str
    phone: str
    id: int = None
