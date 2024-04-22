from dependency_injector import containers, providers
from core.services.buyer_servicve import BuyerService

from core.services.order_service import OrderService
from infrastructure.settings import Settings
from persistence.database import Database
from persistence.mappers.buyer_mapping import BuyerMapping
from persistence.mappers.order_mapping import OrderMapping
from persistence.mappers.merchant_mapping import MerchantMapping
from persistence.repositories.buyer_repository import BuyerRepository
from persistence.repositories.order_repository import OrderRepository
from persistence.repositories.merchant_repository import MerchantRepository
from core.services.merchant_service import MerchantService


class Container(containers.DeclarativeContainer):
    config: Settings = providers.Configuration()

    database = providers.Singleton(
        Database,
        db_url=config.db_connection,
        mappings=providers.List(
            providers.Factory(BuyerMapping),
            providers.Factory(MerchantMapping),
            providers.Factory(OrderMapping)
        ),
    )

    merchant_repository = providers.Factory(
        MerchantRepository,
        session_factory=database.provided.session,
    )

    order_repository = providers.Factory(
        OrderRepository,
        session_factory=database.provided.session,
    )

    buyer_repository = providers.Factory(
        BuyerRepository,
        session_factory=database.provided.session,
    )

    order_service = providers.Factory(
        OrderService,
        order_repository=order_repository,
    )

    merchant_service = providers.Factory(
        MerchantService,
        merchant_repository=merchant_repository,
    )

    buyer_service = providers.Factory(
        BuyerService,
        buyer_repository=buyer_repository,
    )
