export const Logger = {
    Sources: {
        None: 0,
        Websocket: 1
    },
    Info: function(message, source = 0) {
        if(source == 0) {
            console.log(message);
        } else if(source == 1) {
            console.log("Websocket:", message);
        }
    }

}