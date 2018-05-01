import Immutable, { Map } from 'immutable'
import * as actionTypes from '../actions/actionTypes'

const currentUser = (state = new Map(), action) => {
  if (action.type === actionTypes.joinRequested) {
    return Immutable.fromJS(action.payload)
  }
  return state
};

const currentUserIsTyping = (state = false, action) => {
  if (action.type === actionTypes.typingStarted) {
    return true
  }
  if (action.type === actionTypes.typingStopped) {
    return false
  }
  return state
};

export {
  currentUser,
  currentUserIsTyping
}
