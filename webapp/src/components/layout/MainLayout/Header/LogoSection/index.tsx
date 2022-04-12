import React from 'react'
import { Link } from 'react-router-dom'

import { useTheme } from '@mui/material/styles'
import ButtonBase from '@mui/material/ButtonBase'
import Typography from '@mui/material/Typography'
import Logo from '../../../common/Logo'

const LogoSection = () => {
  const theme = useTheme()
  return (
    <Link to="/" style={{ textDecoration: 'none' }}>
      <ButtonBase disableRipple>
        <Logo />
        <Typography
          variant="h5"
          color={theme.palette.grey[900]}
          sx={{ display: { xs: 'none', md: 'block' }, flexGrow: 1 }}
        >
          Deadl
        </Typography>
        <Typography
          variant="h5"
          color={theme.palette.primary.main}
          sx={{ display: { xs: 'none', md: 'block' }, flexGrow: 1 }}
        >
          !
        </Typography>
        <Typography
          variant="h5"
          color={theme.palette.grey[900]}
          sx={{ display: { xs: 'none', md: 'block' }, flexGrow: 1 }}
        >
          ne
        </Typography>
      </ButtonBase>
    </Link>
  )
}

export default LogoSection
