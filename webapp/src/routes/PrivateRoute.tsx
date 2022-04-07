import React from 'react'
import { useSelector } from 'react-redux'
import { Navigate, useLocation } from 'react-router-dom'
import { authenticationSelector } from '../stote/selectors'

// define interface to represent component props
interface Props {
  roles?: Array<string>
  children: JSX.Element
}

const PrivateRoute = ({ children, roles }: Props) => {
  const auth = useSelector(authenticationSelector)
  const location = useLocation()

  if (!roles) return children

  if (!auth.loggedIn) {
    return <Navigate to="/login" state={{ from: location }} replace />
  }

  if (
    (roles.length === 0 && auth.loggedIn) ||
    roles.some((role) => auth.roles && auth.roles.includes(role))
  ) {
    return children
  }

  return <Navigate to="/403" />
}

export default PrivateRoute
