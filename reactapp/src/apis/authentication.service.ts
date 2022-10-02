import { WebService } from './abstract-web.service'

export const AUTH_ENDPOINTS = {
  login: '/api/auth/login',
  logout: '/api/auth/logout',
}

export class AuthenticationService extends WebService {
  private static token: string | undefined
  private static LS_TOKEN_KEY = 'TOKEN'

  constructor() {
    super()
  }

  public login(username: string, password: string): Promise<string> {
    return this.post(AUTH_ENDPOINTS.login, { username, password })
  }

  public logout(): Promise<void> {
    return this.post(AUTH_ENDPOINTS.logout, {})
  }

  //   public static saveToken(token: TokenLike | null): void {
  //     if (token) {
  //       window.localStorage.setItem(AuthenticationService.LS_TOKEN_KEY, token.token)
  //       AuthenticationService.token = token.token
  //     } else {
  //       window.localStorage.removeItem(AuthenticationService.LS_TOKEN_KEY)
  //       AuthenticationService.token = undefined
  //     }
  //   }

  //   public static getToken(): Token | null {
  //     if (!AuthenticationService.token) {
  //       AuthenticationService.token = window.localStorage.getItem(AuthenticationService.LS_TOKEN_KEY) || undefined
  //       if (!AuthenticationService.token) {
  //         return null
  //       }
  //     }

  //     return Token.fromJwt(AuthenticationService.token)
  //   }
}
