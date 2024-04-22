from abc import ABC, abstractmethod
from typing import List

from core.entities.order import Order


class IOrderRepository(ABC):
    @abstractmethod
    def get_all(self) -> List[Order]:
        pass

    @abstractmethod
    def create_order(self, order: Order):
        pass
