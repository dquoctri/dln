import React, { ReactNode } from 'react'
import { Link } from 'react-router-dom'
// define interface to represent component props
interface Props {
  children: ReactNode
}

const AdminLayout = ({ children }: Props) => {
  return (
    <>
      <div>Start Admin Layout</div>
      <Link to="/">home </Link>
      <Link to="/dashboard">dashboard </Link>
      <Link to="/profile">Profile </Link>
      <Link to="/setting">setting </Link>
      <Link to="/admin">admin </Link>
      <Link to="/admin/management">management </Link>
      {children}
      <div>End Admin Layout</div>
    </>
  )
}

export default AdminLayout