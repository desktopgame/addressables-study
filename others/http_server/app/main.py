from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import urlparse
import os


address = ('localhost', 8080)


class MyHTTPRequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        url: str = urlparse(self.path).geturl().encode('utf-8')
        file = url[1:]
        if not os.path.exists(file):
            self.send_response(400)
            return
        self.send_response(200)
        self.send_header('Content-Type', 'text/plain; charset=utf-8')
        self.end_headers()
        with open(file, 'rb') as r:
            b = r.read(1)
            while b != b'':
                self.wfile.write(b)
                b = r.read(1)

    def do_POST(self):
        self.send_response(400)


with HTTPServer(address, MyHTTPRequestHandler) as server:
    server.serve_forever()
