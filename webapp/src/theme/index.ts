import { PaletteMode } from '@mui/material'
import { createTheme } from '@mui/material/styles'
import { amber, red, grey, deepOrange } from '@mui/material/colors'

export enum ThemeMode {
  LIGHT = 'light',
  DARK = 'dark',
}

const getDesignTokens = (mode: PaletteMode) => ({
  palette: {
    mode,
    ...(mode === 'light'
      ? {
          // palette values for light mode
          primary: amber,
          divider: amber[200],
          text: {
            primary: grey[900],
            secondary: grey[800],
          },
        }
      : {
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
        }),
  },
})

// A custom theme for this app
export const lightTheme = {
  palette: {
    mode: 'light',
    // primary: amber,
    // divider: amber[200],
    // text: {
    //   primary: grey[900],
    //   secondary: grey[800],
    // },
  },
}

// A custom theme for this app
export const darkTheme = {
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
