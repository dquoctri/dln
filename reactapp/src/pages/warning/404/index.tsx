import React, { Fragment } from "react"
import PageTitle from "components/PageTitle"

const NotFound = () => {
  return (
    <Fragment>
      <PageTitle title="NotFound-Deadline" />
      <p>Oops! page not found </p>
      <p>Sorry, we can&apos;t find that page! It might be an old link or maybe it moved</p>
      <p>/Search function</p>
    </Fragment>
  )
}

export default NotFound
