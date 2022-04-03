import React from 'react'
import { Link, Outlet } from 'react-router-dom'

import { useTheme } from '@mui/material/styles';
import { AppBar, Box, Toolbar } from '@mui/material';

import Footer from './Footer'
import Header from './Header'


const MainLayout = () => {

  const theme = useTheme();
  const leftDrawerOpened = true;

  return (
    <>
      <Box sx={{ display: 'flex' }}>
        <AppBar
          enableColorOnDark
          position="fixed"
          color="inherit"
          elevation={0}
          sx={{
            bgcolor: theme.palette.background.default,
            transition: leftDrawerOpened ? theme.transitions.create('width') : 'none'
          }}
        >
          <Toolbar>
            <Header />
          </Toolbar>
        </AppBar>
        <Box style={{ marginTop: '100px' }}>
          <Outlet />
        </Box>
      </Box>
      <Footer>
        <Link to="/setting">setting </Link>
      </Footer>
    </>
  )
}

export default MainLayout
