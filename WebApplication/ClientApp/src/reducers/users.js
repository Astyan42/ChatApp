import Immutable, { List, Map } from 'immutable'
import * as actionTypes from "../actions/actionTypes";

const users = (state = new List(), action) => {
    const addUser = (user) => {
        let immutableUser = Immutable.fromJS(user);
        if (state.includes(immutableUser)) {
            return state
        }
        return state.push(immutableUser)
            .sort((user0, user1) => user0.get('name').localeCompare(user1.get('name')))
    };
    const addUsers = (users) =>{
        users.forEach(user => {
            let immutableUser = Immutable.fromJS(user);
            if (state.includes(immutableUser)) {
                return;
            }
            state = state.push(immutableUser)
                .sort((user0, user1) => user0.get('name').localeCompare(user1.get('name')))
        });
        return state;
    };
    const mapping = {
        [actionTypes.userLeft]: (state, action) => state.filter((user) => user.get('id') !== action.payload.userId),
        [actionTypes.joinRequested]: (state, action) => addUser(action.payload),
        [actionTypes.userJoined]: (state, action) => addUser(action.payload),
        [actionTypes.usersRequested]: (state, action) => addUsers(action.payload)
    };

    const handler = mapping[ action.type ];
    if (handler) {
        return handler(state, action)
    }

    return state
};

// stored as a Map since it's easier to check for individual user-ids
const userIdsTyping = (state = new Map(), action) => {
    return state
};

export {
    users,
    userIdsTyping
}
