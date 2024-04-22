
from dataclasses import dataclass
from core.entities.buyer import Buyer

from core.entities.merchant import Merchant


@dataclass
class Order:
    description: str
    price: float
    merchant_id: int
    buyer_id: int
    merchant: Merchant = None
    buyer: Buyer = None
    id: int = None
