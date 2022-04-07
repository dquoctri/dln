import React from 'react'
import { Link } from 'react-router-dom'
import { useTheme, alpha } from '@mui/material/styles'
import { Avatar, Box, ButtonBase } from '@mui/material'
import { Menu } from '@mui/icons-material'
import LogoSection from './LogoSection'

const Header = () => {
  const theme = useTheme()
  return (
    <>
      <Box
        sx={{
          width: 228,
          display: 'flex',
          backgroundColor: `${alpha(theme.palette.background.default, 0.72)}`,
        }}
      >
        <Box
          component="span"
          sx={{ display: { xs: 'none', md: 'block' }, flexGrow: 1 }}
        >
          <LogoSection />
        </Box>
        <ButtonBase
          sx={{
            borderRadius: '12px',
            overflow: 'hidden',
            backgroundColor: `${alpha(theme.palette.background.default, 0.72)}`,
          }}
        >
          <Avatar
            variant="rounded"
            sx={{
              background: theme.palette.secondary.light,
              color: theme.palette.secondary.dark,
              '&:hover': {
                background: theme.palette.secondary.dark,
                color: theme.palette.secondary.light,
              },
            }}
            // onClick={handleLeftDrawerToggle}
            color="inherit"
          >
            <Menu />
          </Avatar>
        </ButtonBase>
        <Link to="/">home </Link>
        <Link to="/dashboard">dashboard </Link>
        <Link to="/profile">Profile </Link>
        <Link to="/setting">setting </Link>
        <Link to="/admin">admin </Link>
        <Link to="/admin/management">management </Link>
      </Box>
    </>
  )
}

export default Header
