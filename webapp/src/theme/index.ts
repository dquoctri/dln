import { PaletteMode } from '@mui/material'
// import { createTheme } from '@mui/material/styles'
import { amber, grey, deepOrange } from '@mui/material/colors'
import { ThemeOptions } from '@mui/material/styles'
import { Shadows } from '@mui/material/styles/shadows'


export enum ThemeMode {
  LIGHT = 'light',
  DARK = 'dark',
}

const darkDefaultTheme: ThemeOptions = {
  palette: {
    mode: 'dark',
    // palette values for dark mode
    primary: deepOrange,
    divider: deepOrange[700],
    background: {
      default: deepOrange[900],
      paper: deepOrange[900],
    },
    text: {
      primary: '#fff',
      secondary: grey[500],
    },
  },
  shadows: Array(25).fill('none') as Shadows
}

const lightDefaultTheme: ThemeOptions = {
  palette: {
    mode: 'light',
    // palette values for light mode
    primary: amber,
    divider: amber[200],
    text: {
      primary: grey[900],
      secondary: grey[800],
    },
  },
  shadows: Array(25).fill('none') as Shadows
}

const getDesignTokens = (mode: PaletteMode) => () => {
  return mode === 'light' ? lightDefaultTheme : darkDefaultTheme
}

// A custom theme for this app
export const lightTheme: ThemeOptions = {
  palette: {
    mode: 'light',
    // primary: amber,
    // divider: amber[200],
    // text: {
    //   primary: grey[900],
    //   secondary: grey[800],
    // },
  },
  shadows: Array(25).fill('none') as Shadows
}

// A custom theme for this app
export const darkTheme: ThemeOptions = {
  palette: {
    mode: 'dark',
    // primary: deepOrange,
    // divider: deepOrange[700],
    // background: {
    //   default: deepOrange[900],
    //   paper: deepOrange[900],
    // },
    // text: {
    //   primary: '#fff',
    //   secondary: grey[500],
    // },
  },
  shadows: Array(25).fill('none') as Shadows
}

export default getDesignTokens
