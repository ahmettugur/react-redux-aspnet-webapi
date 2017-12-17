import {
    FETCH_PRODUCT_LIST,
    FETCH_ADMIN_PRODUCT_LIST,
    PRODUCT_DETAIL,
    PRODUCT_CREATE,
    PRODUCT_UPDATE,
    PRODUCT_DELETE,
} from "../actions/index"

const INITIAL_STATE = {
    products: [],
    adminProducts: [],
    PageCount: 0,
    PageSize: 0,
    product: null,
    message: '',
    status: 0,
    statusClass: ''
}

export default function (state = INITIAL_STATE, action) {
    switch (action.type) {
        case `${FETCH_PRODUCT_LIST}_PENDING`:
            return { ...state }
            break;
        case `${FETCH_PRODUCT_LIST}_FULFILLED`:
            return {
                ...state, products: action.payload.data.Result.Products,
                PageSize: action.payload.data.Result.PageSize,
                PageCount: action.payload.data.Result.PageCount,
                product: null,
                message: action.payload.statusText,
                status: action.payload.data.StatusCode,
                statusClass: 'ok'
            }
            break;
        case `${FETCH_PRODUCT_LIST}_REJECTED`:
        
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.status,
                statusClass: 'error'
            }
            break;
        case `${FETCH_ADMIN_PRODUCT_LIST}_PENDING`:
            return { ...state }
            break;
        case `${FETCH_ADMIN_PRODUCT_LIST}_FULFILLED`:
            return {
                ...state, products: action.payload.data.Result.Products,
                PageCount: action.payload.data.Result.PageCount,
                PageSize: action.payload.data.Result.PageSize,
                product: null,
                message: action.payload.statusText,
                status: action.payload.data.StatusCode,
                statusClass: 'ok'
            }
        case `${FETCH_ADMIN_PRODUCT_LIST}_REJECTED`:
        console.log(action.payload.response)
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.StatusCode,
                statusClass: 'error'
            }
            break;
        case `${PRODUCT_DETAIL}_PENDING`:
            return { ...state }
            break;
        case `${PRODUCT_DETAIL}_FULFILLED`:
            console.log(action.payload.data);
            return {
                ...state,
                product: action.payload.data.Result,
                message: action.payload.statusText,
                status: action.payload.data.StatusCode,
                statusClass: 'ok'
            }
            break;
        case `${PRODUCT_DETAIL}_REJECTED`:
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.StatusCode,
                statusClass: 'error'
            }
            break;

        case `${PRODUCT_CREATE}_PENDING`:
            return { ...state };
            break;
        case `${PRODUCT_CREATE}_FULFILLED`:
            return {
                ...state,
                message: action.payload.data.StatusCode,
                status: action.payload.data.StatusCode,
                statusClass: 'ok'
            }
            break;
        case `${PRODUCT_CREATE}_REJECTED`:
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.StatusCode,
                statusClass: 'error'
            }
            break;
        case `${PRODUCT_UPDATE}_PENDING`:
            return { ...state };
            break;
        case `${PRODUCT_UPDATE}_FULFILLED`:
        console.log("action.payload.response")
        console.log(action.payload)
        console.log("action.payload.response")
            return {
                ...state,
                message: action.payload.statusText,
                status: action.payload.status,
                statusClass: 'ok'
            }
            break;
        case `${PRODUCT_UPDATE}_REJECTED`:
            return {
                ...state,
                message: action.payload.response.data.ErrorMessage,
                status: action.payload.response.data.StatusCode,
                statusClass: 'error'
            }
            break;
        case `${PRODUCT_DELETE}_PENDING`:
            return { ...state };
        case `${PRODUCT_DELETE}_FULFILLED`:
            return {
                ...state,
                message: action.payload.statusText,
                status: action.payload.status,
                statusClass: 'ok'
            }
        case `${PRODUCT_DELETE}_REJECTED`:
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
