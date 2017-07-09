import { FETCH_ADMIN_PRODUCT_LIST, PRODUCT_DETAIL, FETCH_PRODUCT_LIST } from "../actions/index"

const INITIAL_STATE = { products: [], adminProducts: [], PageCount: 0, PageSize: 0, product: null }

export default function (state = INITIAL_STATE, action) {
    switch (action.type) {
        case FETCH_PRODUCT_LIST:
            return {
                ...state, products: action.payload.data.Products,
                PageSize: action.payload.data.PageSize,
                PageCount: action.payload.data.PageCount,
            }
        case FETCH_ADMIN_PRODUCT_LIST:
            return {
                ...state, products: action.payload.data.Products,
                PageCount: action.payload.data.PageCount,
                PageSize: action.payload.data.PageSize
            }
        case PRODUCT_DETAIL:
            return { ...state, product: action.payload.data }
        default:
            return state;
    }
}
