import React, { Fragment, ReactNode } from 'react'
import { Link } from 'react-router-dom'
import { Typography } from '@mui/material'
import { Favorite } from '@mui/icons-material'
import { red } from '@mui/material/colors'

// define interface to represent component props
interface Props {
  children: ReactNode
}

const Footer = ({ children }: Props) => {
  return (
    <Fragment>
      <Typography variant="h6" align="center" gutterBottom>
        Footer
      </Typography>
      <Typography variant="subtitle1" align="center" color="textSecondary" component="p">
        Something here to give the footer a purpose!
      </Typography>
      <Typography variant="subtitle2" align="center" color="textSecondary" component="p">
        Made with <Favorite sx={{ color: red[500] }} /> by <Link to="/">Deadl!ne</Link>
      </Typography>
      {children}
    </Fragment>
  )
}

export default Footer
