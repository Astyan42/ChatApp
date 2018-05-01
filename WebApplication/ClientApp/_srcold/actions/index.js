import {SEND_MESSAGE} from "./types";
import io from 'socket.io-client';

export const emailChanged = (text) => {
    return {
        type:SEND_MESSAGE,
        payload: text
    };
};

export const passwordChanged = (text) => {
    return {
        type:PASSWORD_CHANGED,
        payload: text
    };
};

export const loginUser = ({email, password}) =>{
    return (dispatch) =>{
        dispatch({
            type:LOGIN_USER
        });
        firebase.auth().signInWithEmailAndPassword(email, password)
            .then(user => loginUserSuccess(dispatch, user))
            .catch(()=>{
                firebase.auth().createUserWithEmailAndPassword(email,password)
                    .then(user => loginUserSuccess(dispatch, user))
                    .catch(() => loginUserFail(dispatch))
            });
    };
};

const loginUserFail = (dispatch) => {
    dispatch ({type: LOGIN_USER_FAIL});
};

const loginUserSuccess = (dispatch, user) =>{
    dispatch({
        type: LOGIN_USER_SUCCESS,
        payload: user
    });

    Actions.main();
};