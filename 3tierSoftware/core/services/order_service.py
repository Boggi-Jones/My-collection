from core.dtos.order_dto import OrderDto
from core.interfaces.I_order_repository import IOrderRepository
from core.entities.order import Order
from persistence.repositories.order_repository import OrderRepository


class OrderService:
    def __init__(self, order_repository: IOrderRepository):
        self.order_repository = order_repository

    def get_all(self):
        return [OrderDto(
            id=order.id,
            description=order.description,
            price=order.price,
            merchant_id=order.merchant_id,
            merchant_name=order.merchant.name,
            buyer_name=order.buyer.name,
            buyer_id=order.buyer_id,
        ) for order in self.order_repository.get_all()]

    def create_order(self, order_dto: Order) -> Order:
        order = Order(
            description=order_dto.description,
            price=order_dto.price,
            merchant_id=order_dto.merchant_id,
            buyer_id=order_dto.buyer_id
        )
        self.order_repository.create_order(order)

        return order
