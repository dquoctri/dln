import React, { Fragment } from "react"
import PageTitle from "components/PageTitle"
import { CustomPageProps } from "../page.type"

const Home = ({ title, description }: CustomPageProps) => {
  return (
    <Fragment>
      <PageTitle title={title ? title : "Home"} />
      <div>{description}</div>
    </Fragment>
  )
}

export default Home
