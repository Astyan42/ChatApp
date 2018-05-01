import {getList} from "./restApi";
import {eventReceived} from "./enums/eventReceived";

const messageTypes = {
    Text: 0,
    MethodInvocation : 1,
    ConnectionEvent : 2,
    MethodReturnValue : 3    
};

const connectionUrl = "ws://localhost:5000/notifications/";
let socket;
let id;
const init = (store) => {
  socket = new WebSocket(connectionUrl);
  socket.onmessage = (message) => {
      const data = JSON.parse(message.data);
      if (data.messageType === messageTypes.ConnectionEvent) {
          // Receiving the socketId;
          id = data.data.$value;
      }
      else if (data.messageType === messageTypes.Text) {
          let values = JSON.parse(data.data.$value);
          let eventReceivedElement = eventReceived[values.type];
          store.dispatch({type: eventReceivedElement, payload: values.payload});
      }
  }
  // add listeners to socket messages so we can re-dispatch them as actions
  //Object.keys(messageTypes)
  //  .forEach(type => socket.on(type, (payload) => store.dispatch({ type, payload })))
};

const emit = (type, payload) => {
  if (socket.readyState === WebSocket.OPEN)
    socket.send(JSON.stringify({type:type, payload:payload}));
};

export {
    id as socketId,
    init,
    emit
}
