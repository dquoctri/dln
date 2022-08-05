import { RootState } from '..'

export const themeSelector = (state: RootState) => state.global.theme
export const languageSelector = (state: RootState) => state.global.lng
export const authenticationSelector = (state: RootState) => state.authentication
