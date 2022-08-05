import { combineReducers } from 'redux'
import storage from 'redux-persist/lib/storage' // defaults to localStorage for web and AsyncStorage for react-native
import { GlobalReducer } from './global.state'
import { AuthenticationReducer } from './authentication.state'

const rootReducer = combineReducers({
  global: GlobalReducer,
  authentication: AuthenticationReducer,
})

export const persistConfig = {
  key: 'root',
  storage,
  blacklist: ['navigation'], // blacklist will not be persisted
  whitelist: ['global', 'authentication'], // only whitelist will be persisted
}

export default rootReducer
