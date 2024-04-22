from abc import ABC, abstractmethod
from typing import List

from core.entities.buyer import Buyer


class IBuyerRepository(ABC):
    @abstractmethod
    def get_all(self) -> List[Buyer]:
        pass

    @abstractmethod
    def create_buyer(self, buyer: Buyer):
        pass
