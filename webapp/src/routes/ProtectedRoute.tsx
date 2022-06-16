import React from 'react'
import { useSelector } from 'react-redux'
import { Navigate, useLocation } from 'react-router-dom'
import { authenticationSelector } from '../stote/selectors'
import ProtectedProps from './protected.type'

// define interface to represent component props
export interface Props extends ProtectedProps {
  children: JSX.Element
}

const ProtectedRoute = ({ children, roles, features }: Props) => {
  const auth = useSelector(authenticationSelector)
  const location = useLocation()

  function warnFeatures() {
    if (!features || auth.roles.every((role) => features && features.includes(role))) {
      return children
    }
    return (
      <>
        {children}
      </>
    )
  }

  if (!roles) return warnFeatures()

  if (!auth.loggedIn) {
    return <Navigate to="/login" state={{ from: location }} replace />
  }

  if ((roles.length === 0 && auth.loggedIn) || roles.some((role) => auth.roles && auth.roles.includes(role))) {
    return warnFeatures()
  }

  return <Navigate to="/403" />
}

export default ProtectedRoute
