import React, { ReactNode } from 'react'
import { Link } from 'react-router-dom'
// define interface to represent component props
interface Props {
  children: ReactNode
}

const Layout = ({ children }: Props) => {
  return (
    <>

      <div>Start Layout</div>
      <Link to="/">home </Link>
      <Link to="/dashboard">dashboard </Link>
      <Link to="/profile">Profile </Link>
      <Link to="/setting">setting </Link>
      <Link to="/admin">admin </Link>
      {children}
      <div>End Layout</div>
    </>
  )
}

export default Layout
