import React, { Fragment } from 'react'
import { Typography, Container, Box, Grid, Button } from '@mui/material'
import PageTitle from '../../components/PageTitle'

const Home = () => {
  return (
    <Fragment>
      <PageTitle title="HHHHH-Deadline" />
      <Container maxWidth="xl">
        <Box sx={{ pb: 5 }}>
          <Typography variant="h4">Hi, Welcome back</Typography>
        </Box>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={6} md={3}>
            {/* <AppWeeklySales /> */}
            <Typography variant="body2" color="primary">
              primary Hello primary Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et
              doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid
              ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian
              bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim
              id est labours.
            </Typography>
          </Grid>
          <Grid item xs={12} sm={6} md={3}>
            {/* <AppNewUsers /> */}
            <Typography variant="body2" color="secondary">
              secondary Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers
              et doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut
              liquid ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu
              fujian bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent
              molls anim id est labours.
            </Typography>
          </Grid>
          <Grid item xs={12} sm={6} md={3}>
            {/* <AppItemOrders /> */}
            <Typography variant="body2" color="error">
              error Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et
              doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid
              ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian
              bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim
              id est labours.
            </Typography>
          </Grid>

          <Grid item xs={12} sm={6} md={3}>
            {/* <AppBugReports /> */}
            <Button color="secondary">Secondary</Button>
            <Button color="warning">warning</Button>
            <Button color="info">info</Button>
            <Button color="success">success</Button>
            <Button color="primary">primary</Button>
            <Typography variant="body2" color="warning">
              warning Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et
              doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid
              ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian
              bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim
              id est labours.
            </Typography>
          </Grid>

          <Grid item xs={12} md={6} lg={8}>
            {/* <AppWebsiteVisits /> */}
            <Typography variant="body2" color="info.main">
              info Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et
              doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid
              ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian
              bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim
              id est labours.
            </Typography>
          </Grid>

          <Grid item xs={12} md={6} lg={4}>
            {/* <AppCurrentVisits /> */}
            <Typography variant="body2" color="success.main">
              success Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et
              doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid
              ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian
              bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim
              id est labours.
            </Typography>
          </Grid>

          <Grid item xs={12} md={6} lg={8}>
            {/* <AppConversionRates /> */}
            <Typography variant="body2" color="success.light">
              success Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et
              doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid
              ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian
              bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim
              id est labours.
            </Typography>
          </Grid>

          <Grid item xs={12} md={6} lg={4}>
            {/* <AppCurrentSubject /> */}
            <Typography variant="body2" color="info.light">
              info Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et
              doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid
              ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian
              bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim
              id est labours.
            </Typography>
          </Grid>

          <Grid item xs={12} md={6} lg={8}>
            {/* <AppNewsUpdate /> */}
            <Typography variant="body2" color="success.dark">
              success Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et
              doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid
              ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian
              bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim
              id est labours.
            </Typography>
          </Grid>

          <Grid item xs={12} md={6} lg={4}>
            {/* <AppOrderTimeline /> */}
            <Typography variant="body2" color="info.dark">
              info.dark Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers
              et doolie magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut
              liquid ex ea commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu
              fujian bulla parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent
              molls anim id est labours.
            </Typography>
          </Grid>

          <Grid item xs={12} md={6} lg={4}>
            {/* <AppTrafficBySite /> */}
            <Typography variant="body2">
              Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et doolie
              magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid ex ea
              commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian bulla
              parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim id est
              labours.
            </Typography>
          </Grid>

          <Grid item xs={12} md={6} lg={8}>
            {/* <AppTasks /> */}
            <Typography variant="body2">
              Lorem ipsum dolor sit amen, consenter nipissing eli, sed do elusion tempos incident ut laborers et doolie
              magna alissa. Ut enif ad minim venice, quin nostrum exercitation illampu laborings nisi ut liquid ex ea
              commons construal. Duos aube grue dolor in reprehended in voltage veil esse colum doolie eu fujian bulla
              parian. Exceptive sin ocean cuspidate non president, sunk in culpa qui officiate descent molls anim id est
              labours.
            </Typography>
          </Grid>
        </Grid>
      </Container>
    </Fragment>
  )
}

export default Home
