import React, { Fragment } from 'react'
import PageTitle from '../../components/PageTitle'
import { CustomPageProps } from '../page.type'

const Dashboard = ({ title, description }: CustomPageProps) => {
  return (
    <Fragment>
      <PageTitle title={title ? title : 'Dashboard'} />
      {description}
    </Fragment>
  )
}

export default Dashboard
