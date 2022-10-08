import { createSlice, PayloadAction } from "@reduxjs/toolkit"
import Mode from "models/ui/mode"
import { LanguageKeys } from "../../locales"

export enum GlobalType {
  SET_LANGUAGE = "global/language",
  SET_THEME = "global/theme",
}

export interface GlobalState {
  lng: LanguageKeys
  theme: Mode
}

const initialState: GlobalState = { lng: LanguageKeys.EN, theme: Mode.LIGHT }

function LanguageReducer(state: GlobalState, action: PayloadAction<LanguageKeys>) {
  state.lng = action.payload
}

function ThemeReducer(state: GlobalState, action: PayloadAction<Mode>) {
  state.theme = action.payload
}

const { reducer } = createSlice({
  name: "global",
  initialState,
  reducers: {
    language: LanguageReducer,
    theme: ThemeReducer,
  },
})

export { reducer as GlobalReducer }
