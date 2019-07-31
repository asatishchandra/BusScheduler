export class WebSocketHTTPClass {
    constructor(uri) {
        this.uri = uri;
        this.socket = new WebSocket(uri);
    }

    get Uri() {
        return this.uri;
    }

    set Uri(uri) {
        this.uri = uri;
    }

    get Socket() {
        return this.socket;
    }
}


