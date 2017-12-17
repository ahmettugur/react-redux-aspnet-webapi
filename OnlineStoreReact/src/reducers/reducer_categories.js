import {
    FETCH_CATEGORY_LIST,
    CATEGORY_DETAIL,
    CATEGORY_DELETE,
    CATEGORY_UPDATE,
    CATEGORY_CREATE
} from "../actions/index"

const INITIAL_STATE = {
    categories: [],
    category: null,
    message: '',
    status: 0,
    statusClass: ''
}

export default function (state = INITIAL_STATE, action) {
    switch (action.type) {
        case `${FETCH_CATEGORY_LIST}_PENDING`:
            return { ...state };
            break;
        case `${FETCH_CATEGORY_LIST}_FULFILLED`:
            return {
                ...state,
                categories: action.payload.data.Result,
                category: null,
                message: action.payload.statusText,
                status: action.payload.data.StatusCode,
                statusClass: 'ok'
            }
            break;
        case `${FETCH_CATEGORY_LIST}_REJECTED`:
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.StatusCode,
                statusClass: 'error'
            }
            break;
        case `${CATEGORY_DETAIL}_PENDING`:
            return { ...state };
        case `${CATEGORY_DETAIL}_FULFILLED`:
            return {
                ...state,
                category: action.payload.data.Result,
                message: action.payload.statusText,
                status: action.payload.data.StatusCode,
            }
        case `${CATEGORY_DETAIL}_REJECTED`:
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.StatusCode,
                statusClass: 'error'
            }
            break;
        case `${CATEGORY_CREATE}_PENDING`:
            return { ...state };
            break;
        case `${CATEGORY_CREATE}_FULFILLED`:
            return {
                ...state,
                message: action.payload.data.StatusCode,
                status: action.payload.data.StatusCode,
                statusClass: 'ok'
            }
            break;
        case `${CATEGORY_CREATE}_REJECTED`:
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.StatusCode,
                statusClass: 'error'
            }
            break;
        case `${CATEGORY_UPDATE}_PENDING`:
            return { ...state };
            break;
        case `${CATEGORY_UPDATE}_FULFILLED`:
            return {
                ...state,
                message: action.payload.statusText,
                status: action.payload.status,
                statusClass: 'ok'
            }
            break;
        case `${CATEGORY_UPDATE}_REJECTED`:
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.StatusCode,
                statusClass: 'error'
            }
            break;
        case `${CATEGORY_DELETE}_PENDING`:
            return { ...state };
        case `${CATEGORY_DELETE}_FULFILLED`:
            return {
                ...state,
                message: action.payload.statusText,
                status: action.payload.status,
                statusClass: 'ok'
            }
        case `${CATEGORY_DELETE}_REJECTED`:
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.StatusCode,
                statusClass: 'error'
            }
            break;
        default:
            return state;
    }

}