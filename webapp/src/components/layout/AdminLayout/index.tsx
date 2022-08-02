import React, { Fragment } from 'react'
import { Link, Outlet } from 'react-router-dom'

// import Header from '../MainLayout/Header'

const AdminLayout = () => {
  return (
    <Fragment>
      <header>
        {/* <Header /> */}
        <Link to="/">home </Link>
        <Link to="/dashboard">dashboard </Link>
        <Link to="/profile">Profile </Link>
        <Link to="/setting">setting </Link>
        <Link to="/admin">admin </Link>
        <Link to="/admin/management">management </Link>
      </header>

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
