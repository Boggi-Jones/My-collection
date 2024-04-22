from infrastructure.settings import Settings
import presentation.buyer_endpoints as buyer_endpoints
import presentation.merchant_endpoints as merchant_endpoints
import presentation.order_endpoints as order_endpoints
from fastapi import FastAPI

from infrastructure.container import Container


def __get_endpoint_modules():
    return [buyer_endpoints, merchant_endpoints, order_endpoints]


def create_app() -> FastAPI:
    settings = Settings("./infrastructure/.env")
    container = Container()
    container.config.from_pydantic(settings)
    endpoint_modules = __get_endpoint_modules()
    container.wire(modules=endpoint_modules)

    app = FastAPI()
    app.container = container
    for module in endpoint_modules:
        app.include_router(module.router)
    return app
