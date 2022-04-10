import React from 'react'
import { BrowserRouter } from 'react-router-dom'
import CssBaseline from '@mui/material/CssBaseline'
import Routes from './routes'

function App() {
  return (
    <BrowserRouter>
      <CssBaseline />
      <Routes />
    </BrowserRouter>
  )
}

export default App
