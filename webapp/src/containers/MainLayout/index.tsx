import React from 'react'
import { Link, Outlet } from 'react-router-dom'

import { useTheme } from '@mui/material/styles'
import { AppBar, Container, Toolbar } from '@mui/material'

import Footer from './Footer'
import Header from './Header'

const MainLayout = () => {
  const theme = useTheme()
  const leftDrawerOpened = true

  return (
    <Container>
      <Container>
        <AppBar
          position="fixed"
          enableColorOnDark
          color="inherit"
          elevation={30}
          sx={{
            bgcolor: theme.palette.background.default,
            transition: leftDrawerOpened ? theme.transitions.create('width') : 'none',
          }}
        >
          <Toolbar>
            <Header />
          </Toolbar>
        </AppBar>
      </Container>
      <Container>
        <Outlet />
      </Container>
      <Footer>
        <Link to="/setting">setting </Link>
      </Footer>
    </Container>
  )
}

export default MainLayout
