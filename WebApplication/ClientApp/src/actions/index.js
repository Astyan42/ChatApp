import * as actionTypes from '../actions/actionTypes'
import {join as webApiJoinCall,
        getList as webApiGetList,
        sendMessage as webApiSendMessage,
        getMessageList as webApiGetMessages} from '../actions/restApi';

export function startUp () {
  // this is the redux-middleware package in action, dispatch and getState params are passed in
  return (dispatch, getState) => {
    webApiGetList();
  }
}

export function join (name) {
  return (dispatch, getState) => {
      webApiJoinCall(name);
  }
}

export function sendMessage (message) {
  return (dispatch, getState) => {
    webApiSendMessage(getState().get('currentUser'), message);
  }
}
