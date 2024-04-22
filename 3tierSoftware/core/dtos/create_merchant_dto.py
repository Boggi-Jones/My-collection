from pydantic import BaseModel


class CreateMerchantDto(BaseModel):
    name: str
    email: str
    phone: str
