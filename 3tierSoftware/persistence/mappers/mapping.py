from abc import ABC, abstractmethod, abstractproperty
from sqlalchemy import MetaData, Table
from sqlalchemy.orm import registry


class Mapping(ABC):
    def add_mapping(self, registry: registry) -> None:
        table = self.create_table(registry.metadata)
        registry.map_imperatively(
            self.entity, table, properties=self.properties()
        )

    @ abstractmethod
    def create_table(self, metadata: MetaData) -> Table:
        pass

    @ abstractproperty
    def entity(self) -> type:
        pass

    def properties(self) -> dict:
        return {}
