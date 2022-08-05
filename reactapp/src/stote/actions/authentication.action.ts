import { AuthenticationState, AuthenticationType } from '../reducers/authentication.state'

export const login = (data: AuthenticationState) => {
  return {
    type: AuthenticationType.LOGIN,
    payload: data,
  }
}

export const logout = () => {
  return {
    type: AuthenticationType.LOGOUT,
    payload: null,
  }
}
