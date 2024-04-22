
from pydantic import BaseModel


class CreateOrderDto(BaseModel):
    description: str
    price: float
    merchant_id: int
    buyer_id: int
