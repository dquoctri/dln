import thunk from 'redux-thunk'
import logger from 'redux-logger'
import { configureStore } from '@reduxjs/toolkit'
import { persistStore, persistReducer } from 'redux-persist'
import storage from 'redux-persist/lib/storage' // defaults to localStorage for web and AsyncStorage for react-native
import rootReducer from './reducers'

const middlewares = process.env.NODE_ENV === 'production' ? [thunk] : [thunk, logger]

const persistConfig = {
  key: 'root',
  storage,
  whitelist: ['global', 'authentication'],
}

const persistedReducer = persistReducer(persistConfig, rootReducer)

export const store = configureStore({
  reducer: persistedReducer,
  middleware: middlewares,
})

export const persistor = persistStore(store)
export type RootState = ReturnType<typeof store.getState>
