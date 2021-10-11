import React from 'react'
import { useSelector } from 'react-redux'
import { BrowserRouter as Router } from 'react-router-dom'
import { ThemeProvider, createTheme } from '@mui/material/styles'
import CssBaseline from '@mui/material/CssBaseline'

import { themeSelector } from './stote/selectors'
import AppRoutes from './routes'

function App() {
  const themeMode = useSelector(themeSelector)
  const theme = React.useMemo(() => createTheme(themeMode), [themeMode])
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <AppRoutes />
      </Router>
    </ThemeProvider>
  )
}

export default App
