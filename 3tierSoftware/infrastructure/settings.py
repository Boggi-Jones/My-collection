from pydantic import BaseSettings


class Settings(BaseSettings):
    db_connection: str

    class Config:
        env_file_encoding = "utf-8"
