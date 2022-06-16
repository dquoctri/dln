import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { LanguageKeys } from '../../locales'
import { ThemeMode } from '../../theme'

export enum GlobalType {
  SET_LANGUAGE = 'global/language',
  SET_THEME = 'global/theme',
}

export interface GlobalState {
  lng: LanguageKeys
  theme: any
}

const initialState: GlobalState = { lng: LanguageKeys.EN, theme: 'lightTheme' }

function LanguageReducer(state: GlobalState, action: PayloadAction<LanguageKeys>) {
  state.lng = action.payload
}

function ThemeReducer(state: GlobalState, action: PayloadAction<ThemeMode>) {
  switch (action.payload) {
    case ThemeMode.LIGHT:
      state.theme = 'lightTheme'
      break
    case ThemeMode.DARK:
      state.theme = 'darkTheme'
      break
    default:
      state.theme = 'lightTheme'
  }
}

const { reducer } = createSlice({
  name: 'global',
  initialState,
  reducers: {
    language: LanguageReducer,
    theme: ThemeReducer,
  },
})

export { reducer as GlobalReducer }
