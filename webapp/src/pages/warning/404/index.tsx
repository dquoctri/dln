import React, { Fragment } from 'react'
// import { Link } from 'react-router-dom'
import AppTitle from '../../../components/app-title'

const NotFound = () => {
  return (
    <Fragment>
      <AppTitle title='NotFound-Deadline' />
      <p>Oops! page not found </p>
      <p>Sorry, we can't find that page! It might be an old link or maybe it moved</p>
      <p>/Search function</p>
    </Fragment>
  )
}

export default NotFound