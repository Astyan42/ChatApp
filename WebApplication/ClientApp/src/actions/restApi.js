import axios from 'axios';
import {socketId} from "./websocket";

const connectionUrl = "http://localhost:5000/";
let socket;

const myAxios = axios.create({
    baseURL: connectionUrl
});

function join (userName) {
    myAxios.post("/User/Join?socketId="+socketId,{name:userName})
        .catch(reason => {console.log(reason)});
}

function getList() {
    myAxios.get("/User/GetList?socketId="+socketId)
        .catch(reason => {console.log(reason)});
}

function getMessageList() {
    myAxios.get("/Message/GetList?socketId="+socketId)
        .catch(reason => console.log(reason));
}

function sendMessage(user, contentMessage) {
    myAxios.post("/Message/Send",{sender:user, content:contentMessage})
        .catch(reason => console.log(reason));
}

export {join, getList, getMessageList, sendMessage}
