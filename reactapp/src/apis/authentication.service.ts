import { WebService } from "./abstract-web.service"

export const AUTH_ENDPOINTS = {
  login: "/api/auth/login",
  logout: "/api/auth/logout",
}

export class AuthenticationService extends WebService {
  private static token: string | undefined
  private static LS_TOKEN_KEY = "TOKEN"

  constructor() {
    super()
  }

  public login(username: string, password: string): Promise<string> {
    return this.post(AUTH_ENDPOINTS.login, { username, password })
  }

  public logout(): Promise<void> {
    return this.post(AUTH_ENDPOINTS.logout, {})
  }
}
