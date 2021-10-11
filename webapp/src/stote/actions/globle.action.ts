import { GlobalType } from '../reducers/global.state'
import { LanguageKeys } from '../../locales'
import { ThemeMode } from '../../theme'

export const setLanguage = (data: LanguageKeys) => {
  return {
    type: GlobalType.SET_LANGUAGE,
    payload: data,
  }
}

export const setTheme = (data: ThemeMode) => {
  return {
    type: GlobalType.SET_THEME,
    payload: data,
  }
}
