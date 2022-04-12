import React, { Fragment } from 'react'
import { Link, Outlet } from 'react-router-dom'

import { AppBar, Toolbar } from '@mui/material'
import { useTheme } from '@mui/material/styles'
import useScrollTrigger from '@mui/material/useScrollTrigger'
// import Footer from '../../containers/layout/footer'
// import Header from '../MainLayout/Header'

const AdminLayout = () => {
  const theme = useTheme()
  const trigger = useScrollTrigger({
    disableHysteresis: true,
    threshold: 0,
  })
  return (
    <Fragment>
      <AppBar
        enableColorOnDark
        color="primary"
        elevation={trigger ? 4 : 0}
        sx={{
          bgcolor: theme.palette.background.default,
        }}
      >
        <Toolbar>
          <header>
            {/* <Header /> */}
            <Link to="/">home </Link>
            <Link to="/dashboard">dashboard </Link>
            <Link to="/profile">Profile </Link>
            <Link to="/setting">setting </Link>
            <Link to="/admin">admin </Link>
            <Link to="/admin/management">management </Link>
          </header>
        </Toolbar>
      </AppBar>
      <Toolbar />

      <Outlet />
      <footer>
        {/* <Footer>
          <Link to="/setting">setting</Link>
        </Footer> */}
      </footer>
    </Fragment>
  )
}

export default AdminLayout
