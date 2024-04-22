from core.interfaces.I_merchant_repository import IMerchantRepository
from core.entities.merchant import Merchant
from persistence.repositories.merchant_repository import MerchantRepository


class MerchantService:
    def __init__(self, merchant_repository: IMerchantRepository) -> None:
        self.__merchant_repository = merchant_repository

    def get_all(self):
        return self.__merchant_repository.get_all()

    def create_merchant(self, merchant_dto: Merchant) -> Merchant:
        merchant = Merchant(
            name=merchant_dto.name,
            email=merchant_dto.email,
            phone=merchant_dto.phone
        )
        self.__merchant_repository.create_merchant(merchant)
        return merchant
