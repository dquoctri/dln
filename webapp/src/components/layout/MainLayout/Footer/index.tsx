import React from 'react'
import { Link } from 'react-router-dom'
import { Typography } from '@mui/material'
import { Favorite } from '@mui/icons-material'
import { red } from '@mui/material/colors'

const Footer = () => {
  return (
    <footer className="footer">
      <Typography variant="h6" align="center" gutterBottom>
        Footer
      </Typography>
      <Typography variant="subtitle1" align="center" color="secondary" component="p">
        Something here to give the footer a purpose!
      </Typography>
      <Typography variant="subtitle2" align="center" color="secondary" component="p">
        Made with <Favorite sx={{ color: red[500] }} /> by <Link to="/">Deadl!ne</Link>
      </Typography>
    </footer>
  )
}

export default Footer
