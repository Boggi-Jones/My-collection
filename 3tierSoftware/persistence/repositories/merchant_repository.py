from contextlib import AbstractContextManager
from typing import Callable
from sqlalchemy.orm import Session

from core.interfaces.I_merchant_repository import IMerchantRepository
from core.entities.merchant import Merchant


class MerchantRepository(IMerchantRepository):
    def __init__(self, session_factory: Callable[..., AbstractContextManager[Session]]):
        self.__session_factory = session_factory

    def get_all(self):
        with self.__session_factory() as session:
            return session.query(Merchant).all()

    def create_merchant(self, merchant: Merchant):
        with self.__session_factory() as session:
            session.add(merchant)
            session.commit()
            session.refresh(merchant)
