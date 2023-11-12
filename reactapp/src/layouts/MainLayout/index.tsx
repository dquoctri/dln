import React, { Fragment } from "react"
import { Outlet } from "react-router-dom"

import Navigation from "./Navigation"
import Header from "./Header"
import Footer from "./Footer"

const MainLayout = () => {
  return (
    <Fragment>
      <div className="min-h-full">
        <Navigation />
        <Header />
        <main>
          <Outlet />
        </main>
        <Footer />
      </div>
    </Fragment>
  )
}

export default MainLayout
