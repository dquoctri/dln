import thunk from 'redux-thunk'
import { configureStore } from '@reduxjs/toolkit'
import { persistStore, persistReducer } from 'redux-persist'
import rootReducer, { persistConfig } from './reducers'
import logger from './logger'

const middlewares = process.env.NODE_ENV === 'development' ? [thunk, logger] : [thunk]
const persistedReducer = persistReducer(persistConfig, rootReducer)

export const store = configureStore({
  reducer: persistedReducer,
  middleware: middlewares,
})

export const persistor = persistStore(store)
export type RootState = ReturnType<typeof store.getState>
