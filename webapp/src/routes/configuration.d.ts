import React, { lazy } from 'react'

const NotFound = lazy(() => import('../pages/warning/404'))
const Unauthorized = lazy(() => import('../pages/warning/401'))
const Forbidden = lazy(() => import('../pages/warning/403'))
const Login = lazy(() => import('../pages/login'))
const Home = lazy(() => import('../pages/home'))
const Dashboard = lazy(() => import('../pages/dashboard'))
const Profile = lazy(() => import('../pages/profile'))
const Settings = lazy(() => import('../pages/setting'))
const AdminDashboard = lazy(() => import('../pages/admin/dashboard'))
const Management = lazy(() => import('../pages/admin/management'))


export interface RouteItem {
  path: string,
  component?: React.LazyExoticComponent<() => JSX.Element>,
  exact?: boolean,
  subRoutes?: Array<RouteItem>
}

export const publicRoutes: Array<RouteItem> = [
  {
    path: "/home",
    component: Home,
    exact: true,
    subRoutes: [],
  },
  {
    path: "/dashboard",
    component: Dashboard,
    exact: true,
    subRoutes: [],
  },
  {
    path: "/login",
    component: Login,
    exact: true,
    subRoutes: [],
  },
  {
    path: "/404",
    component: NotFound,
    exact: true,
    subRoutes: [],
  },
  {
    path: "/401",
    component: Unauthorized,
    exact: true,
    subRoutes: [],
  },
]

export const privateRoutes: Array<RouteItem> = [
  {
    path: "/profile",
    component: Profile,
    exact: true,
    subRoutes: [],
  },
  {
    path: "/setting",
    component: Settings,
    exact: true,
    subRoutes: [],
  },
  {
    path: "/403",
    component: Forbidden,
    exact: true,
    subRoutes: [],
  },
]


export const adminRoutes: Array<RouteItem> = [
  {
    path: "/admin",
    component: AdminDashboard,
    exact: true,
    subRoutes: [],
  },
  {
    path: "/admin/management",
    component: Management,
    exact: true,
    subRoutes: [],
  },
]

publicRoutes.push({
  path: "/",
  component: Home,
  exact: true,
  subRoutes: [],
})



