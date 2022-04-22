import { PaletteMode } from '@mui/material'
// import { createTheme } from '@mui/material/styles'
import { common, amber, grey, blue, green, deepOrange } from '@mui/material/colors'
import { ThemeOptions } from '@mui/material/styles'

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
}

const lightDefaultTheme: ThemeOptions = {
  palette: {
    mode: 'light',
    // palette values for light mode
    common: common,
    primary: amber,
    secondary: deepOrange,
    error: {
      main: '#d32f2f',
      // light: '#ef5350',
      // dark: '#c52828',
      contrastText: '#fff',
    },
    warning: amber,
    info: blue,
    success: green,
    divider: amber[200],
    text: {
      primary: grey[900],
      secondary: grey[800],
    },
  },
}

const getDesignTokens = (mode: PaletteMode) => (): ThemeOptions => {
  return mode === 'light' ? lightDefaultTheme : darkDefaultTheme
}

// A custom theme for this app
export const lightTheme: ThemeOptions = {
  palette: {
    mode: 'light',
    // palette values for light mode
    // common: common,
    // primary: amber,
    // secondary: deepOrange,
    // error: {
    //   main: '#d32f2f',
    //   // light: '#ef5350',
    //   // dark: '#c52828',
    //   // contrastText: '#fff'
    // },
    // warning: amber,
    // info: blue,
    // success: green,
    divider: amber[200],
  },
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
}

export default getDesignTokens
