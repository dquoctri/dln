import React, { Fragment } from 'react'
import { Link } from 'react-router-dom'
import AppTitle from '../../../components/app-title'

const Forbidden = () => {
  return (
    <Fragment>
      <AppTitle title='Forbidden-Deadline' />
      <p>Forbidden</p>
    </Fragment>
  )
}

export default Forbidden