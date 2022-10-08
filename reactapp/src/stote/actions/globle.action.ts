import { GlobalType } from "../reducers/global.state"
import { LanguageKeys } from "../../locales"
import Mode from "models/ui/mode"

export const setLanguage = (data: LanguageKeys) => {
  return {
    type: GlobalType.SET_LANGUAGE,
    payload: data,
  }
}

export const setTheme = (data: Mode) => {
  return {
    type: GlobalType.SET_THEME,
    payload: data,
  }
}
