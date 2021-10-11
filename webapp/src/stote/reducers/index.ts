import { combineReducers } from 'redux'
import { GlobalReducer } from './global.state'
import { AuthenticationReducer } from './authentication.state'

const rootReducer = combineReducers({
  global: GlobalReducer,
  authentication: AuthenticationReducer,
})

export default rootReducer
