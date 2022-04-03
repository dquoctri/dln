import React, { Suspense, LazyExoticComponent } from 'react'
import Loader from '../components/loader'
import PrivateRoute from './PrivateRoute'

const Loadable = (Component: LazyExoticComponent<() => JSX.Element>, roles?: Array<string>) => (props: any) => {
  if (!roles) {
    return (
      <Suspense fallback={<Loader />}><Component {...props} /></Suspense>
    )
  }
  return (
    <PrivateRoute roles={roles}>
      <Suspense fallback={<Loader />}><Component {...props} /></Suspense>
    </PrivateRoute>
  )
}

export default Loadable