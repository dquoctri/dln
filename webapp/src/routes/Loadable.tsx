import React, { Suspense, LazyExoticComponent } from 'react'
import Loader from '../components/loader'
import ProtectedRoute from './ProtectedRoute'

interface ProtectedProps {
  roles?: Array<string>
}

const Loadable = (
  Component: LazyExoticComponent<() => JSX.Element> | (() => JSX.Element),
  protectedProps?: ProtectedProps
) => {
  const render = (props: any) => {
    return (
      <ProtectedRoute roles={protectedProps?.roles}>
        <Suspense fallback={<Loader />}>
          <Component {...props} />
        </Suspense>
      </ProtectedRoute>
    )
  }
  return render
}

export default Loadable
