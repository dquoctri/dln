import React, { lazy } from 'react'
import { Navigate, RouteObject } from 'react-router-dom'
import MainLayout from '../components/layout/MainLayout'
import AdminLayout from '../components/layout/AdminLayout'
import Loadable from './Loadable'

const NotFound = Loadable(lazy(() => import('../pages/warning/404')))
const Unauthorized = Loadable(lazy(() => import('../pages/warning/401')))
const Forbidden = Loadable(lazy(() => import('../pages/warning/403')))
const Login = Loadable(lazy(() => import('../pages/login')))
const Home = Loadable(lazy(() => import('../pages/home')))
const Dashboard = Loadable(lazy(() => import('../pages/dashboard')))
const Profile = Loadable(lazy(() => import('../pages/profile')))
const Settings = Loadable(lazy(() => import('../pages/setting')))
const AdminDashboard = Loadable(
  lazy(() => import('../pages/admin/dashboard')),
  { roles: [], features: ['Hello'] }
)
const Management = Loadable(
  lazy(() => import('../pages/admin/management')),
  { roles: ['VIEW'], features: ['Hello'] }
)

export const MainRoutes: RouteObject = {
  path: '/',
  element: <MainLayout />,
  children: [
    { index: true, element: <Home /> },
    { path: 'dashboard', element: <Dashboard title="dashnono" description="dashboard" abc="Jkkk" /> },
    { path: 'profile', element: <Profile /> },
    { path: 'setting', element: <Settings /> },
    { path: 'login', element: <Login /> },
    { path: '403', element: <Forbidden /> },
    { path: '401', element: <Unauthorized /> },
    { path: '404', element: <NotFound /> },
    { path: '*', element: <Navigate to="/404" /> },
  ],
}

export const AdminRoutes: RouteObject = {
  path: '/admin',
  element: <AdminLayout />,
  children: [
    { index: true, element: <AdminDashboard /> },
    { path: 'management', element: <Management /> },
  ],
}
