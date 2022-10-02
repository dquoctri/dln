import { WebService } from './abstract-web.service'
import Ping from 'models/apis/ping'

export const PING_ENDPOINTS = {
  ping: '/api/ping',
}

export class PingService extends WebService {
  constructor() {
    super()
  }

  public ping(): Promise<Ping[]> {
    return this.get<Ping[]>(PING_ENDPOINTS.ping)
  }
}
