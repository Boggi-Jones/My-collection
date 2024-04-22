from pydantic import BaseModel


class CreateBuyerDto(BaseModel):
    name: str
    email: str
    phone: str
