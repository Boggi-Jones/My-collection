from abc import ABC, abstractmethod
from typing import List

from core.entities.merchant import Merchant


class IMerchantRepository(ABC):
    @abstractmethod
    def get_all(self) -> List[Merchant]:
        pass

    @abstractmethod
    def create_merchant(self, merchant: Merchant):
        pass
