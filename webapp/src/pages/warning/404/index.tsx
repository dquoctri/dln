import React, { Fragment } from 'react'
import { Link } from 'react-router-dom'
import PageTitle from '../../../components/PageTitle'
import { Box, Typography, Container } from '@mui/material'

const NotFound = () => {
  return (
    <Fragment>
      <PageTitle title="NotFound-Deadline" />
      <Container>
        <div>
          <Box sx={{ maxWidth: 480, margin: 'auto', textAlign: 'center' }}>
            <div>
              <Typography variant="h3" paragraph>
                Sorry, page not found!
              </Typography>
            </div>
            <Typography sx={{ color: 'text.secondary' }}>
              Sorry, we couldn&apos;t find the page you&apos;re looking for.
              Perhaps you&apos;ve mistyped the URL? Be sure to check your
              spelling.
            </Typography>

            <div>
              <Box
                component="img"
                src="/static/illustrations/illustration_404.svg"
                sx={{ height: 260, mx: 'auto', my: { xs: 5, sm: 10 } }}
              />
            </div>
            <Link to="/">Go to Home </Link>
          </Box>
        </div>
      </Container>
      <p>Oops! page not found </p>
      <p>
        Sorry, we can&apos;t find that page! It might be an old link or maybe it
        moved
      </p>
      <p>/Search function</p>
    </Fragment>
  )
}

export default NotFound
