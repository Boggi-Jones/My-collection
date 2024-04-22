import uvicorn
from infrastructure.application_factory import create_app

app = create_app()

if __name__ == '__main__':
    uvicorn.run('startup:app', host='127.0.0.1', port=8000, reload=True)
