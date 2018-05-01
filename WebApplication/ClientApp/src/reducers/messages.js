import Immutable, { List } from 'immutable'
import * as actionTypes from '../actions/actionTypes';

const messages = (state = new List(), action) => {
   if (action.type === actionTypes.messageAdded) {
     return state.push(Immutable.fromJS(action.payload))
   }
   if (action.type === actionTypes.messageHistory){
       action.payload.forEach(message => state = state.push(Immutable.fromJS(message)));
       return state;
   }
   return state
};

export {
  messages
}
