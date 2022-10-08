import { WebService } from "./abstract-web.service"
import { useSelector } from "react-redux"
import { authenticationSelector } from "stote/selectors"

export abstract class SecuredService extends WebService {
  constructor() {
    super()
    const isExpired = (expiresAt: number): boolean => {
      return expiresAt * 1000 < Date.now()
    }

    const getToken = () => {
      const authentication = useSelector(authenticationSelector)
      if (!authentication || !authentication.token || isExpired(authentication.expiresAt || 0)) {
        return null
      }
      return "Bearer " + authentication.token
    }
    this.addDynamicHeader("Authorization", getToken)
  }
}
