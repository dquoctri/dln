import React, { Suspense, useMemo } from 'react'
import { Route, Switch, Redirect, useLocation } from 'react-router-dom'
import { useSelector } from 'react-redux'
import { RouteItem, adminRoutes, privateRoutes, publicRoutes } from './configuration.d'
import { useCreatePaths } from './useRouteCreater.hook'
import { authenticationSelector } from '../stote/selectors'
import Loader from '../components/loader'
import Layout from '../containers/layout'
import AdminLayout from '../containers/admin-layout'
const NotFound = React.lazy(() => import('../pages/warning/404'))

const AppRoutes = () => {
  const location = useLocation()
  const auth = useSelector(authenticationSelector)

  const adminPaths = useCreatePaths(adminRoutes)
  const privatePaths = useCreatePaths(privateRoutes)
  const publicPaths = useCreatePaths(publicRoutes)

  const createRoute = (route: RouteItem) => {
    return <Route
      path={route.path}
      component={route.component || NotFound}
      exact={route.exact}
    />
  }

  const createRedirectRoute = (route: RouteItem, path: string) => {
    return <Route
      path={route.path}
      render={() => <Redirect to={{ pathname: path, state: { from: location } }} />}
      exact={route.exact}
    />
  }

  const renderAdminRoutes = useMemo(() => {
    if (!auth.loggedIn) {
      return adminRoutes.map(route => { return createRedirectRoute(route, '/401') })
    }
    if (!auth.isAdmin) {
      return adminRoutes.map(route => { return createRedirectRoute(route, '/403') })
    }
    return adminRoutes.map(route => { return createRoute(route) })
  }, [auth])

  const renderPrivateRoutes = useMemo(() => {
    if (!auth.loggedIn) {
      return privateRoutes.map(route => { return createRedirectRoute(route, '/login') })
    }
    return privateRoutes.map(route => { return createRoute(route) })
  }, [auth])

  const renderPublicRoutes = useMemo(() => {
    return publicRoutes.map(route => { return createRoute(route) })
  }, [])

  return (
    <Suspense fallback={<Loader />}>
      <Switch>
        <Route path={adminPaths} exact>
          <AdminLayout>
            <Switch>
              <Suspense fallback={<Loader />}>
                {renderAdminRoutes}
              </Suspense>
            </Switch>
          </AdminLayout>
        </Route>
        <Route path={privatePaths} exact>
          <Layout>
            <Switch>
              <Suspense fallback={<Loader />}>
                {renderPrivateRoutes}
              </Suspense>
            </Switch>
          </Layout>
        </Route>
        <Route path={publicPaths} exact>
          <Layout>
            <Switch>
              <Suspense fallback={<Loader />}>
                {renderPublicRoutes}
              </Suspense>
            </Switch>
          </Layout>
        </Route>
        <Route path='*'>
          <Layout>
            <NotFound />
          </Layout>
        </Route>
      </Switch>
    </Suspense>
  )
}

export default AppRoutes
