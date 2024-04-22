Gott að hafa í huga að models ætti eflaust mest eiga að heima undir business og dtos ætti mest eiga að heima undir persistence en þetta er algengt með 3-tier að hafa þetta svona flatt.

Alembic commands: 

- alembic revision --autogenerate -m "init"
- alembic upgrade head