import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export enum AuthenticationType {
  LOGIN = 'authentication/login',
  LOGOUT = 'authentication/logout',
}

export interface AuthenticationState {
  loggedIn: boolean
  token?: string
  userId?: number
  email?: string
  issuedAt?: number
  expiresAt?: number
  roles: Array<string>
  isAdmin: boolean
  //more infor
}

const initialState: AuthenticationState = {
  loggedIn: false,
  isAdmin: false,
  roles: [],
}

function loginReducer(state: AuthenticationState, action: PayloadAction<AuthenticationState>) {
  state = { ...action.payload, loggedIn: true }
  return state
}

function logoutReducer(state: any, _: PayloadAction<void>) {
  state = initialState
  return state
}

const { reducer } = createSlice({
  name: 'authentication',
  initialState,
  reducers: {
    login: loginReducer,
    logout: logoutReducer,
  },
})

export { reducer as AuthenticationReducer }
