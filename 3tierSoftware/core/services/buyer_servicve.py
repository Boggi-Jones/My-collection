from core.dtos.create_buyer_dto import CreateBuyerDto
from core.interfaces.I_buyer_repository import IBuyerRepository
from core.entities.buyer import Buyer
from persistence.repositories.buyer_repository import BuyerRepository


class BuyerService:
    def __init__(self, buyer_repository: IBuyerRepository):
        self.__buyer_repository = buyer_repository

    def get_all(self):
        return self.__buyer_repository.get_all()

    def create_buyer(self, buyer_dto: CreateBuyerDto) -> Buyer:
        buyer = Buyer(
            name=buyer_dto.name,
            email=buyer_dto.email,
            phone=buyer_dto.phone
        )
        self.__buyer_repository.create_buyer(buyer)

        return buyer
