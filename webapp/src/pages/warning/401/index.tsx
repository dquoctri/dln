import React, { Fragment } from 'react'
import { Link } from 'react-router-dom'
import AppTitle from '../../../components/app-title'

const Unauthorized = () => {
  return (
    <Fragment>
      <AppTitle title='Unauthorized-Deadline' />
      <p>Full authentication is required to access this resource: <Link to="/login">/login</Link></p>
    </Fragment>
  )
}

export default Unauthorized