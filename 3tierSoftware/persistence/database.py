from typing import Callable, List
from sqlalchemy import create_engine, orm
from contextlib import contextmanager, AbstractContextManager
from sqlalchemy.orm import Session, registry
from persistence.mappers.mapping import Mapping


class Database:
    def __init__(self, db_url: str, mappings: List[Mapping]) -> None:
        self.__engine = create_engine(
            db_url, echo=True
        )

        self.__session_factory = orm.scoped_session(
            orm.sessionmaker(
                autocommit=False,
                autoflush=False,
                bind=self.__engine,
            ),
        )

        self.mapper_registry = registry()
        for mapping in mappings:
            mapping.add_mapping(self.mapper_registry)

    @contextmanager
    def session(self) -> Callable[..., AbstractContextManager[Session]]:
        session: Session = self.__session_factory()
        try:
            yield session
        except Exception:
            session.rollback()
            raise
        finally:
            session.close()
