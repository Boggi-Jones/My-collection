from dataclasses import dataclass


@dataclass
class Merchant:
    name: str
    email: str
    phone: str
    id: int = None
