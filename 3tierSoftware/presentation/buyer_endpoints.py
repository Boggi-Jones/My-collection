from fastapi import APIRouter, Depends
from dependency_injector.wiring import inject, Provide
from core.services.buyer_servicve import BuyerService
from core.dtos.create_buyer_dto import CreateBuyerDto

from infrastructure.container import Container
from core.entities.buyer import Buyer


router = APIRouter(
    prefix='/buyers',
)


@router.get('/')
@inject
def get_buyers(merchant_service: BuyerService = Depends(Provide[Container.buyer_service])):
    return merchant_service.get_all()


@router.post('/')
@inject
def create_buyer(request: CreateBuyerDto, buyer_service: BuyerService = Depends(Provide[Container.buyer_service])):
    return buyer_service.create_buyer(request)
