from dataclasses import dataclass


@dataclass
class OrderDto:
    description: str
    price: float
    merchant_id: int
    merchant_name: str
    buyer_id: int
    buyer_name: str
    id: int
